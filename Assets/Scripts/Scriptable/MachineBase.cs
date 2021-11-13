using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
using Game;
#endif

/// <summary>
/// TODO
/// Create levelling Machine
/// Dynamic value for duration long time
/// </summary>
[CreateAssetMenu(fileName = "MachineBase", menuName = "ScriptableObject/MachineBase")]
public class MachineBase : ScriptableObject
{
    public MachineClass machineClass;
    public MachineIgrendient machineType;
    public MachineIgrendient targetMachine;
    public GameObject basePrefab;
    public GameObject PrefabResult;

    [Tooltip("Instance UI overlay when machine touched on first time")]
    public bool isUseMachineOverlay = false;
    public GameObject prefabUIOverlay;

    [Header("Component")]
    public bool isUseRadiusBar = false;
    public bool isUseBarCapacity = false;
    public bool isUseOverCook = false;

    [Header("Machine Properties")]
    public bool isUpgradeable = true;
    public bool isAutoRun = false;
    public List<MachineProperties> properties = new List<MachineProperties>();
}

#if UNITY_EDITOR
[CustomEditor(typeof(MachineBase))]
public class editScript : Editor
{
    public MachineBase machineData;
    public Machine machine;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        machineData = (MachineBase)target;

        EditorGUILayout.LabelField("Validate");
        if (GUILayout.Button("Validate " + target.name))
        {
            validateData();
        }
    }

    private bool validateData()
    {
        // Validate use case component
        try
        {
            Debug.Log($"<color=yellow> Validating {target.name} </color>");

            machine = machineData.basePrefab.GetComponent<Machine>();

            if (machineData.properties.Count <= 0) throw new Exception("Properties cant be empty");
            if (machineData.isUseMachineOverlay && machineData.prefabUIOverlay == null) throw new Exception("Will use overlay, but dont have overlay prefab");
            if (!machineData.isUpgradeable && machineData.properties.Count > 1) throw new Exception("Not Upgradeable have multiple Properties");
            if (machineData.isUpgradeable && machineData.properties.Count == 1) throw new Exception("Upgradeable but only one Properties");

            //for (int i = 0; i < machineData.properties.Count; i++)
            //{
            //    if (machineData.properties[i].level != i + 1)
            //        throw new Exception("Wrong format level");
            //    if (
            //        machineData.isUseBarCapacity
            //        && machineData.properties[i].maxCapacity == 0
            //        || !machine.capacityBarPos
            //        )
            //        throw new Exception("Max Capacity");
            //    if (
            //        machineData.isUseRadiusBar
            //        && machineData.properties[i].processDuration == 0
            //        || !machine.radiusBarPos
            //        )
            //        throw new Exception("Processing time error Capacity");
            //}

            Debug.Log($"<color=green> Validate success {target.name} </color>");
            return true;

        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }

    }
}
#endif