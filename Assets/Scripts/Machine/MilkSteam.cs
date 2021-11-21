using System.Collections;
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
                listIgrendients.Add(machineType);
                glassTarget.glass.addIgrendients(
                    listIgrendients,
                    machineBase.spriteGlassStates.Find(val => glassTarget.glass.igrendients.Contains(val.igrendient)).color
                    );
                machineCapacity.getOne();
            }
        }
    }
}
