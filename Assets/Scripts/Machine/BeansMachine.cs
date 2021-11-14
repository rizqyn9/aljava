using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BeansMachine : Machine
    {
        private void OnMouseDown()
        {
            machineState = MachineState.ON_PROCESS;
        }
    }
}
