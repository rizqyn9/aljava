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
        public Transform fixedPos;
        public GameObject baseMachineProcess, baseMachineCapacity;
        public List<MachineCanvas> processesTransform = new List<MachineCanvas>();
        public List<MachineCanvas> capacitiesTransform = new List<MachineCanvas>();


        [Header("Debug")]
        public List<MachineProcess> machineProcesses = new List<MachineProcess>();
        public List<MachineCapacity> machineCapacities = new List<MachineCapacity>();

        public void instanceMachineProcess(Machine _machine, out MachineProcess machineProcess)
        {
            machineProcess = Instantiate(baseMachineProcess, GetTransform(true, _machine)).GetComponent<MachineProcess>();
            machineProcesses.Add(machineProcess);
            machineProcess.init(_machine);
        }

        public void instanceMachineCapacity(Machine _machine, out MachineCapacity machineCapacity)
        {
            machineCapacity = Instantiate(baseMachineCapacity, GetTransform(false, _machine)).GetComponent<MachineCapacity>();
            machineCapacities.Add(machineCapacity);
            machineCapacity.init(_machine);
        }

        Transform GetTransform(bool isProcess, Machine _machine)
        {
            if (isProcess)
                return processesTransform.Find(val => val.machineType == _machine.machineType).transform;
            else
                return capacitiesTransform.Find(val => val.machineType == _machine.machineType).transform;
        }

        public static Transform MachineProcessPlace => UIGameManager.MachineManager.machineProcessPlace;
    }
}
