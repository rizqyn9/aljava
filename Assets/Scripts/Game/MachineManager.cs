using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MachineManager : MonoBehaviour
    {
        [Header("Debug")]
        public EnvManager envManager;
        public List<Machine> machines = new List<Machine>();
        public List<MachineBase> machineBases = new List<MachineBase>();

        public void init(EnvManager _envManager)
        {
            envManager = _envManager;
            validateMachineWillInstance();
            envManager.listMachines = machineBases;

        }

        [SerializeField] List<MachineIgrendient> machineTypes;
        public void validateMachineWillInstance()
        {
            machineTypes = new List<MachineIgrendient>();
            // Get all menu
            // Get all machine
            // get different machine
            foreach (MenuBase _menu in envManager.listMenus)
            {
                foreach(MachineIgrendient _machineType in _menu.Igrendients)
                {
                    if (!machineTypes.Contains(_machineType))
                    {
                        machineTypes.Add(_machineType);
                        machineBases.Add(ResourceManager.ListMachines.Find(val => val.machineType == _machineType));
                    }
                }
            }
        }
    }
}
