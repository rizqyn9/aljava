using UnityEngine;
using System.Collections;

namespace Game
{
    public abstract class Machine : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject prefabPlace;
        public Transform capacityPos, processPos;

        [Header("Debug")]
        public MachineIgrendient machineType;
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

        private void Start()
        {
            machineState = MachineState.INIT;
            gameObject.LeanAlpha(0, 0);
            setDisableCollider();
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
        public virtual void OnGameStart() { }
        #endregion

        #region Handle On State
        public virtual void OnMachineNeedRepair() { }

        public virtual void OnMachineRepair() { }

        public virtual void OnMachineOverCook() { }

        public virtual void OnMachineClearance() { }

        public virtual void OnMachineDone() { }

        public virtual void OnMachineProcess() { }

        public virtual void OnMachineIddle() { }

        public virtual void OnMachineOff() { }

        public virtual void OnMachineInit() { }
        #endregion

        #region Hook Machine Process
        public void OnProcessCompleted()
        {
            machineState = MachineState.ON_DONE;
            StartCoroutine(IStartOverCook());
        }
        public void OnProcessOvercookCompleted()
        {

        }
        public void OnProcessRepairCompleted()
        {

        }

        IEnumerator IStartOverCook()
        {
            yield return new WaitForSeconds(GameController.GameProperties.delayToOverCook);
            machineProcess.runOverCook();
            yield break;
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

        void checkPrefabSpawn()
        {
            if(prefabPlace.transform.childCount > 0)
                foreach(SpriteRenderer go in prefabPlace.GetComponentsInChildren<SpriteRenderer>())
                    Destroy(go.gameObject);
        }
        #endregion
    }
}
