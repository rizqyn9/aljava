using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class UIMachineManager : MonoBehaviour
    {
        [Header("Properties")]
        public Transform machineProcessPlace;
        public Transform machineCapacityPlace;
        public Transform machineOverlayPlace;
        public Transform fixedPos;
        public GameObject baseMachineProcess, baseMachineCapacity;
        public List<MachineCanvas> processesTransform = new List<MachineCanvas>();
        public List<MachineCanvas> capacitiesTransform = new List<MachineCanvas>();
        public List<UI_MachineOverlay> listMachineOverlay = new List<UI_MachineOverlay>();

        [Header("Debug")]
        public List<MachineProcess> machineProcesses = new List<MachineProcess>();
        public List<MachineCapacity> machineCapacities = new List<MachineCapacity>();
        public UI_MachineOverlay activeMachineOverlay = null;

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

        public void instanceMachineOverlay(Machine _machine, out UI_MachineOverlay machineOverlay)
        {
            machineOverlay = Instantiate(_machine.machineBase.prefabUIOverlay, machineOverlayPlace).GetComponent<UI_MachineOverlay>();
            listMachineOverlay.Add(machineOverlay);
            machineOverlay.init(_machine, machineOverlayPlace.gameObject);
        }

        public static Transform MachineProcessPlace => UIGameManager.MachineManager.machineProcessPlace;
    }
}
