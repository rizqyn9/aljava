using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MachineProcess : MonoBehaviour
    {
        [Header("Properties")]
        public Image bar;

        [Header("Debug")]
        public Machine machine;

        private void Start() => bar.fillAmount = 0;

        public void init(Machine _machine)
        {
            machine = _machine;
            transform.position = Camera.main.WorldToScreenPoint(_machine.processPos.position);
        }
    }
}
