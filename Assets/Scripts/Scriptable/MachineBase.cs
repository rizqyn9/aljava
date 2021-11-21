using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using System;
using UnityEditor;
using Game;
#endif

[CreateAssetMenu(fileName = "MachineBase", menuName = "ScriptableObject/MachineBase")]
public class MachineBase : ScriptableObject
{
    public MachineClass machineClass;
    public MachineIgrendient machineType;
    public MachineIgrendient targetMachine;
    public GameObject basePrefab;
    public GameObject PrefabResult;

    [Header("Glass State")]
    public List<MachineIgrendient> listTargetGlassState;
    public List<SpriteGlassState> spriteGlassStates;

    [Tooltip("Instance UI overlay when machine touched on first time")]
    public bool isUseMachineOverlay = false;
    public GameObject prefabUIOverlay;

    [Header("Component")]
    public bool isUseProcessUI = true;
    public bool isUseOverCook = false;
    public bool isUseBarCapacity = false;

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
    public string msg = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        machineData = (MachineBase)target;

        EditorGUILayout.LabelField("Validate");
        if (GUILayout.Button("Validate " + target.name))
        {
            renameFile();
            validateData();
        }
        GUILayout.TextArea(msg);
    }

    public void renameFile()
    {
        string name = machineData.machineClass.ToString() + "_"+ machineData.machineType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(target.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, name);
    }

    private bool validateData()
    {
        // Validate use case component
        try
        {
            msg = $"Validating {target.name}";

            //machine = machineData.basePrefab.GetComponent<Machine>();

            //if (machineData.properties.Count <= 0) throw new Exception("Properties cant be empty");
            //if (machineData.isUseMachineOverlay && machineData.prefabUIOverlay == null) throw new Exception("Will use overlay, but dont have overlay prefab");
            //if (!machineData.isUpgradeable && machineData.properties.Count > 1) throw new Exception("Not Upgradeable have multiple Properties");
            //if (machineData.isUpgradeable && machineData.properties.Count == 1) throw new Exception("Upgradeable but only one Properties");

            for (int i = 0; i < machineData.properties.Count; i++)
            {
                MachineProperties props = machineData.properties[i];

                GameObject prefab = props.prefab;
                if (!prefab)
                    throw new Exception("Props dont have prefab");

                Animator animator = prefab.GetComponent<Animator>();
                if(!animator)
                    throw new Exception("Prefab dont have Animator");
                
                if (machineData.properties[i].level != i + 1)
                    throw new Exception("Wrong format level");

                //if (
                //    machineData.isUseBarCapacity
                //    && machineData.properties[i].maxCapacity == 0
                //    || !machine.capacityBarPos
                //    )
                //    throw new Exception("Max Capacity");
                //if (
                //    machineData.isUseRadiusBar
                //    && machineData.properties[i].processDuration == 0
                //    || !machine.radiusBarPos
                //    )
                //    throw new Exception("Processing time error Capacity");
            }

            //Debug.Log($"<color=green> Validate success {target.name} </color>");

            msg = $"Validate success {target.name}";
            return true;

        }
        catch (Exception e)
        {
            //Debug.LogError(e);
            msg = e.Message.ToString();
            return false;
        }
    }

    public Transform pos;

    public void generatePosCanvas()
    {
        Debug.Log("adsadad");

        Debug.Log(UIMachineManager.MachineProcessPlace);
    }
}
#endif