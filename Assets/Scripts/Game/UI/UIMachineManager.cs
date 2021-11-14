using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIMachineManager : MonoBehaviour
    {
        [Header("Properties")]
        public Transform machineProcessPlace;
        public Transform machineCapacityPlace;
        public GameObject baseMachineProcess, baseMachineCapacity;

        [Header("Debug")]
        public List<MachineProcess> machineProcesses = new List<MachineProcess>();
        public List<MachineCapacity> machineCapacities = new List<MachineCapacity>();

        public void instanceMachineProcess(Machine _machine, out MachineProcess machineProcess)
        {
            machineProcess = Instantiate(baseMachineProcess, machineProcessPlace).GetComponent<MachineProcess>();
            machineProcesses.Add(machineProcess);
            machineProcess.init(_machine);
        }

        public void instanceMachineCapacity(Machine _machine, out MachineCapacity machineCapacity)
        {
            machineCapacity = Instantiate(baseMachineCapacity, machineCapacityPlace).GetComponent<MachineCapacity>();
            machineCapacities.Add(machineCapacity);
            machineCapacity.init(_machine);
        }
    }
}