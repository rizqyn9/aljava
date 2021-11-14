using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CoffeeMaker : Machine
    {
        public override void Start()
        {
            base.Start();
            isMachineReceiver = true;
        }

        public override void OnMachineIddle()
        {
            base.OnMachineIddle();
            machineProcess.resetProcess();
            listIgrendients = new List<MachineIgrendient>();
        }

        public override void OnValidateMachineDone()
        {
            if (GlassManager.IsGlassTargetAvaible(MachineIgrendient.NULL, out glassTarget))
            {
                machineProcess.resetProcess();
                glassTarget.glass.addIgrendients(
                    listIgrendients,
                    spriteGlassStates.Find(val => val.igrendient == listIgrendients[0]).sprite
                    );
                machineState = MachineState.ON_IDDLE;
            }
        }

    }
}
