using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aljava;

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
    [SerializeField] internal MenuBase notValidMenu;

    public static List<LevelBase> ListLevels => Instance.listLevels;
    public static List<MachineBase> ListMachines => Instance.listMachines;
    public static List<BuyerBase> ListBuyers => Instance.listBuyers;
    public static List<MenuBase> ListMenus => Instance.listMenus;
    public static MenuBase NotValidMenu => Instance.notValidMenu;

    public static int LevelCount;
    public static int MachineCount;
    public static int BuyerCount;
    public static int MenuCount;

    private void Start()
    {
        GameManager.Instance.isResourceManagerReady = true;
        print($"<color=green> Resource Instance </color>");
        LevelCount = listLevels.Count;
        MachineCount = ListMachines.Count;
        BuyerCount = ListBuyers.Count;
        MenuCount = ListMenus.Count;
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

