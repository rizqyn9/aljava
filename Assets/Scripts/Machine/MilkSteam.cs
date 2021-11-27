using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class MilkSteam : Machine
    {
        public override void OnValidateMachineDone()
        {
            if (GlassManager.IsGlassTargetAvaible(machineBase.listTargetGlassState, out glassTarget))
            {
                machineProcess.resetProcess();

                SpriteGlassState spriteGlassState = machineBase.spriteGlassStates.Find(val => glassTarget.glass.igrendients.Contains(val.igrendient));

                glassTarget.glass.addIgrendients(
                    new List<MachineIgrendient>() { machineType},
                    spriteGlassState.color,
                    spriteGlassState.sprite
                    );
                machineCapacity.getOne();
            }
        }
    }
}
