using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.MainMenu
{
    public class UI_UpgradeItem : MonoBehaviour
    {
        public MachineBase machineBase;

        public void init(MachineBase _machine)
        {
            machineBase = _machine;
        }
    }
}
