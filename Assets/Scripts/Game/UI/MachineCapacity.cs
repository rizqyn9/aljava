using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public enum MachineCapacityState
    {
        ON_EMPTY,
        ON_CAPACITY,
        ON_FULL
    }

    public class MachineCapacity : MonoBehaviour
    {
        [Header("Properties")]
        public Image bar;

        [Header("Debug")]
        public Machine machine;
        public int maxCapacity = 0;
        public MachineCapacityState machineCapacityState = MachineCapacityState.ON_EMPTY;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] int _stateCapacity = 0;
        public int stateCapacity
        {
            get => _stateCapacity;
            set
            {
                {
                    updateCapacity(_stateCapacity, value);
                    _stateCapacity = value;
                }
            }
        }

        void updateCapacity(int _old, int _new)
        {
            bar.fillAmount = (float)_new / maxCapacity;
        }

        private void Start() => resetCapacity();
        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        public void init(Machine _machine)
        {
            machine = _machine;
            maxCapacity = machine.properties.maxCapacity;
            //transform.position = Camera.main.WorldToScreenPoint(machine.capacityPos.position);


            transform.position = machine.capacityPos.position;


            //print(Camera.main.WorldToScreenPoint(machine.capacityPos.position));
            //print(Camera.main.WorldToViewportPoint(machine.capacityPos.position));
            //print(Camera.main.ScreenPointToRay(machine.capacityPos.position));
            //print(Camera.main.ScreenToViewportPoint(machine.capacityPos.position));
            //print(Camera.main.ScreenToWorldPoint(machine.capacityPos.position));
            //print(Camera.main.ViewportPointToRay(machine.capacityPos.position));
            //print(Camera.main.ViewportToScreenPoint(machine.capacityPos.position));
            //print(Camera.main.ViewportToWorldPoint(machine.capacityPos.position));
        }

        void resetCapacity()
        {
            LeanTween.alpha(rectTransform, 0, 0);
        }

        public void setFull()
        {
            machineCapacityState = MachineCapacityState.ON_FULL;
            stateCapacity = maxCapacity;
            LeanTween.alpha(rectTransform, 1, 1f);
            machine.OnCapacityFull();
        }

        public void getOne()
        {
            stateCapacity -= 1;
            if (_stateCapacity <= 0)
            {
                handleEmpty();
            }
            machineCapacityState = MachineCapacityState.ON_CAPACITY;
            machine.OnCapacityGetOne();
        }

        private void handleEmpty()
        {
            machineCapacityState = MachineCapacityState.ON_EMPTY;
            LeanTween.alpha(rectTransform, 0, 1f);
            machine.OnCapacityEmpty();
        }
    }
}
