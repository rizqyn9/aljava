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
        public int capacity = 0;

        public void init(Machine _machine)
        {
            machine = _machine;
            transform.position = Camera.main.WorldToScreenPoint(machine.capacityPos.position);
        }

        public void filled()
        {
            
        }
    }
}
