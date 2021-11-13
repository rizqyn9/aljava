using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] internal List<LevelBase> listLevels;
    [SerializeField] internal List<MachineBase> listMachines;
    [SerializeField] internal List<BuyerBase> listBuyers;
    [SerializeField] internal List<MenuBase> listMenus;

    public static List<LevelBase> ListLevels => Instance.listLevels;
    public static List<MachineBase> ListMachines => Instance.listMachines;
    public static List<BuyerBase> ListBuyers => Instance.listBuyers;
    public static List<MenuBase> ListMenus => Instance.listMenus;

    private void Start()
    {
        GameManager.Instance.isResourceManagerReady = true;
        print($"<color=green> Resource Instance </color>");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ResourceManager))]
public class ResourceManagerEditor: Editor
{
    public ResourceManager resourceManager;

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        resourceManager = (ResourceManager)target;

        getAllResources();
        if (GUILayout.Button("Get all reources"))
        {
        }
    }

    public void getAllResources()
    {
        resourceManager.listBuyers = Resources.LoadAll<BuyerBase>("Buyer").ToList();
        resourceManager.listMenus = Resources.LoadAll<MenuBase>("Menu").ToList();
        resourceManager.listLevels = Resources.LoadAll<LevelBase>("Level").ToList();
        resourceManager.listMachines = Resources.LoadAll<MachineBase>("Machine").ToList();
    }
}
#endif

