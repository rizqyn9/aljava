using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MachineCapacity : MonoBehaviour
    {
        [Header("Properties")]
        public Image bar;

        [Header("Debug")]
        public Machine machine;
        public int maxCapacity = 0;
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
            transform.position = Camera.main.WorldToScreenPoint(machine.capacityPos.position);
        }

        void resetCapacity()
        {
            LeanTween.alpha(rectTransform, 0, 0);
        }

        public void setFull()
        {
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
            machine.OnCapacityGetOne();
        }

        private void handleEmpty()
        {
            LeanTween.alpha(rectTransform, 0, 1f);
            machine.OnCapacityEmpty();
        }
    }
}
