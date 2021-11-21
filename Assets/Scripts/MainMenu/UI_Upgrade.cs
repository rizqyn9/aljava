using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.MainMenu
{
    public class UI_Upgrade : MonoBehaviour
    {
        public List<MachineBase> machineBases;

        public void init()
        {
            machineBases = ResourceManager.ListMachines.FindAll(val => val.isUpgradeable);
        }
    }
}