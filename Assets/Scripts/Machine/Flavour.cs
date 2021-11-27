using System.Collections.Generic;

namespace Aljava.Game
{
    public class Flavour : Machine
    {
        public override void initLatte()
        {
            machinePrefab = prefabPlace.GetChild(0).gameObject;
        }

        public override void OnValidateMachineIddle()
        {
            setDisableCollider();
            machineState = MachineState.ON_PROCESS;

            if (GlassManager.IsGlassTargetAvaible(MachineIgrendient.NULL, out glassTarget))
            {
                glassTarget.glass.addIgrendients(
                    new List<MachineIgrendient>() { machineType },
                    machineBase.spriteGlassStates[0].color,
                    machineBase.spriteGlassStates[0].sprite
                    );
            }

            machineState = MachineState.ON_IDDLE;
        }

        public override void OnMachineProcess() { }
        public override void OnMachineIddle()
        {
            setEnableCollider();
        }
    }
}
