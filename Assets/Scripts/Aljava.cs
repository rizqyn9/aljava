using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Game;

public class AljavaWindow : EditorWindow
{
    UIMachineManager machineManager;
    Transform machinePosProcess;
    Transform machinePosCapacity;

    Machine machine;
    ResourceManager resourceManager;

    GameObject basePrefab;

    [MenuItem("Aljava/Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AljavaWindow));
    }

    void OnGUI()
    {
        machineManager = EditorGUILayout.ObjectField(machineManager, typeof(UIMachineManager), true) as UIMachineManager;
        machinePosProcess = EditorGUILayout.ObjectField(machinePosProcess, typeof(Transform), true) as Transform;
        machinePosCapacity = EditorGUILayout.ObjectField(machinePosCapacity, typeof(Transform), true) as Transform;
        resourceManager = EditorGUILayout.ObjectField(resourceManager, typeof(ResourceManager), true) as ResourceManager;
        basePrefab = EditorGUILayout.ObjectField(basePrefab, typeof(GameObject), true) as GameObject;
        

        if (GUILayout.Button("Generate Process"))
        {
            //machineManager.processesTransform = new List<MachineCanvas>();

            resourceManager.listMachines.ForEach(val =>
            {
                GameObject go = Instantiate(basePrefab, machinePosProcess);
                go.transform.position = Camera.main.WorldToScreenPoint(val.basePrefab.GetComponent<Machine>().processPos.position);
                go.name = $"Machine-{val.machineType}";

                MachineCanvas machineCanvas = go.AddComponent<MachineCanvas>();
                machineCanvas.machineType = val.machineType;
                machineManager.processesTransform.Add(machineCanvas);
            });
        }

        if(GUILayout.Button("Generate Capacity"))
        {
            //foreach (Transform to in machinePosCapacity.GetComponentsInChildren<Transform>()) DestroyImmediate(to.gameObject);
            //machineManager.capacitiesTransform = new List<MachineCanvas>();

            resourceManager.listMachines.ForEach(val =>
            {
                GameObject go = Instantiate(basePrefab, machinePosCapacity);
                go.transform.position = Camera.main.WorldToScreenPoint(val.basePrefab.GetComponent<Machine>().capacityPos.position);
                go.name = $"Machine-{val.machineType}";

                MachineCanvas machineCanvas = go.AddComponent<MachineCanvas>();
                machineCanvas.machineType = val.machineType;
                machineManager.capacitiesTransform.Add(machineCanvas);
            });
        }
    }
}
