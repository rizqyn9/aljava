using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

//#if UNITY_EDITOR
//#endif

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Machine : MonoBehaviour
    {
        [Header("Animation State")]
        public string ANIM_EMPTY = "ANIM_EMPTY";
        public string ANIM_FIRST_INIT = "ANIM_FIRST_INIT",
            ANIM_PROCESS = "ANIM_PROCESS",
            ANIM_FULL = "ANIM_FULL",
            ANIM_GET_ONE = "ANIM_GET_ONE",
            ANIM_LAST_INIT = "ANIM_LAST_INIT";

        [Header("Debug")]
        public MachineIgrendient machineType;
        public Transform capacityPos, processPos, prefabPlace;
        public MachineBase machineBase;
        public GameObject machinePrefab;
        public BoxCollider2D boxCollider2D;
        public Animator animator;
        public MachineProperties properties;

        [Header("Component")]
        public MachineCapacity machineCapacity;
        public MachineProcess machineProcess;

#region Machine State
        [SerializeField] MachineState _machineState = MachineState.OFF;
        public MachineState machineState
        {
            get => _machineState;
            set
            {
                if (_machineState == value) return;
                //print($"{value} : {_machineState}");
                _machineState = value;
                OnMachineStateChanged(_machineState, value);
            }
        }

        public virtual void OnMachineStateChanged(MachineState _old, MachineState _new)
        {
            switch (_machineState)
            {
                case MachineState.INIT:
                    OnMachineInit();
                    break;
                case MachineState.OFF:
                    OnMachineOff();
                    break;
                case MachineState.ON_IDDLE:
                    OnMachineIddle();
                    break;
                case MachineState.ON_PROCESS:
                    OnMachineProcess();
                    break;
                case MachineState.ON_DONE:
                    OnMachineDone();
                    break;
                case MachineState.ON_CLEARANCE:
                    OnMachineClearance();
                    break;
                case MachineState.ON_OVERCOOK:
                    OnMachineOverCook();
                    break;
                case MachineState.ON_REPAIR:
                    OnMachineRepair();
                    break;
                case MachineState.ON_NEEDREPAIR:
                    OnMachineNeedRepair();
                    break;
                default:
                    break;
            }
        }
#endregion

#region Initial
        private void Awake() => boxCollider2D = GetComponent<BoxCollider2D>();
        public void setEnableCollider() => boxCollider2D.enabled = true;
        public void setDisableCollider() => boxCollider2D.enabled = false;

        public virtual void Start()
        {
            machineState = MachineState.INIT;

            gameObject.LeanAlpha(0, 0);
            setDisableCollider();

            machineState = MachineState.OFF;
        }

        private void OnValidate()
        {
            getDependencies();
        }

        public void init(MachineBase _machineBase, int _machineLevel)
        {
            machineBase = _machineBase;
            machineType = machineBase.machineType;

            properties = machineBase.properties[_machineLevel - 1];

            checkPrefabSpawn();
            machinePrefab = Instantiate(properties.prefab, prefabPlace.transform);
            animator = machinePrefab.GetComponent<Animator>();
            animator.enabled = false;
            setUpComponent();
        }
#endregion

#region Handle from Machine Manager
        public virtual void OnGameBeforeStart() => StartCoroutine(beforeStart());
        public virtual void OnGameStart()
        {
            //print($"Start : {machineState}");
            machineState = MachineState.ON_IDDLE;
            isInteractable = true;
        }
#endregion

#region Trigger Default handle
        public GlassRegistered glassTarget;
        public Machine machineTarget;
        public bool isInteractable = false;
        //BUG code:1
        [SerializeField] int bruteForce;
        public virtual void OnMouseDown()
        {
            if (!isInteractable) return;
            bruteForce += 1;
            if (machineState == MachineState.ON_IDDLE) OnValidateMachineIddle();
            if (machineState == MachineState.ON_DONE || machineState == MachineState.ON_OVERCOOK)
                OnValidateMachineDone();
            if (machineState == MachineState.ON_NEEDREPAIR) machineState = MachineState.ON_REPAIR;
        }

        public virtual void OnValidateMachineDone() { }
        public virtual void OnValidateMachineIddle()
        {
            if (isMachineReceiver) return;
            machineState = MachineState.ON_PROCESS;
        }
        public virtual void OnMachineServe() { }
#endregion

#region Hook Machine Receiver
        public bool isMachineReceiver = false;
        public List<MachineIgrendient> listIgrendients = new List<MachineIgrendient>();
        public void reqInput(MachineIgrendient _igrendient)
        {
            listIgrendients.Add(_igrendient);
            OnMachineReceive();
        }

        public virtual void OnMachineReceive()
        {
            if (isMachineReceiver)
                machineState = MachineState.ON_PROCESS;
        }
#endregion

#region Handle On State
        public virtual void OnMachineInit()
        {
            animator.enabled = true;
            setDisableCollider();
        }

        public virtual void OnMachineOff() => setDisableCollider();

        public virtual void OnMachineIddle()
        {
            //print($"{bruteForce} ");
            machineProcess.resetProcess();
            setEnableCollider();
            changeAnimation(ANIM_EMPTY);
        }

        public virtual void OnMachineProcess()
        {
            changeAnimation(ANIM_PROCESS);

            setDisableCollider();
            if (!isInteractable) return;
            machineProcess.runProcess();
        }

        public virtual void OnMachineOverCook()
        {

        }

        public virtual void OnMachineNeedRepair()
        {
            setEnableCollider();
        }

        public virtual void OnMachineRepair()
        {
            setDisableCollider();
            machineProcess.runRepair();
        }

        public virtual void OnMachineClearance() { }

        public virtual void OnMachineDone()
        {
            setEnableCollider();
        }
#endregion

#region Hook Machine Process
        public void OnProcessCompleted()
        {
            machineState = MachineState.ON_DONE;
            if(machineBase.isUseOverCook)
                StartCoroutine(IStartOverCook());
            if (machineBase.isUseBarCapacity)
            {
                machineCapacity.setFull();
                changeAnimation(ANIM_FULL);
            }
        }
        public void OnProcessOvercookCompleted()
        {
            machineState = MachineState.ON_NEEDREPAIR;
        }
        public void OnProcessRepairCompleted()
        {
            machineState = MachineState.ON_IDDLE;
        }

        IEnumerator IStartOverCook()
        {
            yield return new WaitForSeconds(GameController.GameProperties.delayToOverCook);
            machineProcess.runOverCook();
            yield break;
        }
#endregion

#region Animator
        [SerializeField] string currentAnimState = "";
        public void changeAnimation(string newAnimState)
        {
            if (currentAnimState == newAnimState) return;
            animator.Play(newAnimState);
            currentAnimState = newAnimState;
        }
#endregion

#region Hook machine capacity
        public void OnCapacityGetOne()
        {
            if (machineCapacity.stateCapacity == 0)
                changeAnimation(ANIM_EMPTY);
            else
                changeAnimation(ANIM_GET_ONE);
        }

        public void OnCapacityFull()
        {

        }

        public void OnCapacityEmpty()
        {
            machineState = MachineState.ON_IDDLE;
        }
#endregion

#region IEnumurator
        IEnumerator beforeStart()
        {
            LeanTween.alpha(gameObject, 1, GameController.GameProperties.animMachineBeforeStart).setEaseInBounce();
            yield return 1;
        }
#endregion

#region Dependencies
        void setUpComponent()
        {
            UIGameManager.MachineManager.instanceMachineProcess(this, out machineProcess);
            if (machineBase.isUseBarCapacity)
            {
                UIGameManager.MachineManager.instanceMachineCapacity(this, out machineCapacity);
            }
            if (machineBase.isUseMachineOverlay) { }
        }

        void getDependencies()
        {
            foreach(Transform to in gameObject.GetComponent<Transform>())
            {
                if (to.gameObject.name == "--prefab") prefabPlace = to; 
                if (to.gameObject.name == "-capacity") capacityPos = to; 
                if (to.gameObject.name == "-process") processPos = to; 
            }
        }

        void checkPrefabSpawn()
        {
            if(prefabPlace.childCount > 0)
                foreach(SpriteRenderer go in prefabPlace.GetComponentsInChildren<SpriteRenderer>())
                    Destroy(go.gameObject);
        }
#endregion
    }
}


