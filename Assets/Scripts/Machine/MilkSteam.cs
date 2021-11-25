using System.Collections.Generic;

namespace Aljava.Game
{
    public class MilkSteam : Machine
    {
        public override void OnValidateMachineDone()
        {
            if (GlassManager.IsGlassTargetAvaible(machineBase.listTargetGlassState, out glassTarget))
            {
                machineProcess.resetProcess();
                glassTarget.glass.addIgrendients(
                    new List<MachineIgrendient>() { machineType},
                    machineBase.spriteGlassStates.Find(val => glassTarget.glass.igrendients.Contains(val.igrendient)).color
                    );
                machineCapacity.getOne();
            }
        }
    }
}
