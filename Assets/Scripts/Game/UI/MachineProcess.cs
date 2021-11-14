using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public enum BarType
    {
        PROCESS,
        OVERCOOK,
        REPAIR,
        NOT_SET
    }

    public class MachineProcess : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] Image barProcess;
        [SerializeField] Image barOvercook, barRepair;
        [SerializeField] GameObject checkList, checkRepair;

        [Header("Debug")]
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Machine machine;
        [SerializeField] float duration;
        [SerializeField] bool isUseProcessUI, isUseOverCook, isUseCapacity;
        [SerializeField] int leanTweenId = 0;
        [SerializeField] BarType barType;
        [SerializeField] List<Image> bars;                      // Bars image container
        [SerializeField] List<GameObject> checks;                      // Checks GO container


        private void Start() => resetProcess(true);
        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        public void init(Machine _machine)
        {
            machine = _machine;
            transform.position = Camera.main.WorldToScreenPoint(_machine.processPos.position);

            bars = new List<Image> { barRepair, barProcess, barOvercook };
            checks = new List<GameObject> { checkList, checkRepair };

            isUseProcessUI = machine.machineBase.isUseProcessUI;
            isUseOverCook = _machine.machineBase.isUseOverCook;
            isUseCapacity = _machine.machineBase.isUseBarCapacity;
            duration = _machine.properties.processDuration;
        }

        public void runProcess()
        {
            barType = BarType.PROCESS;
            runDefault(barProcess, duration);
        }

        public void runOverCook()
        {
            barType = BarType.OVERCOOK;
            runDefault(barOvercook, GameController.GameProperties.overcookDuration);
        }

        public void runRepair()
        {
            barType = BarType.REPAIR;
            float dur = GameController.GameProperties.repairDuration;
            simulateRepair(dur);
            runDefault(barOvercook, dur);
        }

        void runDefault(Image _bar, float _duration)
        {
            _bar.fillAmount = 0;
            _bar.enabled = true;

            LeanTween.alpha(rectTransform, 1, .2f);
            gameObject.LeanScale(new Vector2(1, 1), .2f).setEaseInOutBounce();

            leanTweenId = LeanTween.value(0, 100, _duration).setOnUpdate((val) =>
             {
                 _bar.fillAmount = val / 100;
             }).setOnComplete(handleComplete).id;
        }

        void handleComplete()
        {
            if(barType == BarType.PROCESS)
            {
                if (machine.machineBase.isUseBarCapacity) resetProcess();
                spawnCheck(checkList);
                machine.OnProcessCompleted();
            } else if(barType == BarType.OVERCOOK)
            {
                print("Overcook");
                spawnCheck(checkRepair);
                machine.OnProcessOvercookCompleted();
            } else if(barType == BarType.REPAIR)
            {
                resetProcess();
                machine.OnProcessRepairCompleted();
            }
        }

        void spawnCheck(GameObject _checkGO)
        {
            checks.ForEach(val => val.SetActive(false));
            _checkGO.SetActive(true);
        }

        #region Dependencies
        void resetChecks() => checks.ForEach(val => val.SetActive(false));
        public void resetProcess(bool isInit = false)
        {
            if (leanTweenId != 0) LeanTween.cancel(leanTweenId);

            resetChecks();
            bars.ForEach(val =>
            {
                val.enabled = false;
                val.fillAmount = 0;
            });

            LeanTween.alpha(rectTransform, 0, isInit ? 0 : .3f);
            gameObject.LeanScale(Vector2.zero, isInit ? 0 : .5f).setOnComplete(resetChecks);

            barType = BarType.NOT_SET;
        }

        void simulateRepair(float _dur)
        {
            LeanTween.rotateZ(checkRepair, 30f, _dur / 5).setLoopPingPong(5);
        }
        #endregion
    }
}
