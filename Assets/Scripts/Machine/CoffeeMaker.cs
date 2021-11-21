using System.Collections.Generic;

namespace Aljava.Game
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
                listIgrendients.Add(machineType);
                glassTarget.glass.addIgrendients(
                    listIgrendients,
                    machineBase.spriteGlassStates.Find(val => val.igrendient == listIgrendients[0]).color
                    );
                machineState = MachineState.ON_IDDLE;
            }
        }

    }
}
