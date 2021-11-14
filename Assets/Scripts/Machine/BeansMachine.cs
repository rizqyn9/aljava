using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BeansMachine : Machine
    {
        public override void OnValidateMachineDone()
        {
            if (MachineManager.IsMachineTargetAvaible(machineBase.targetMachine, out machineTarget))
            {
                machineTarget.reqInput(machineType);
                machineCapacity.getOne();
            }
        }
    }
}
