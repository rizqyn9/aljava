using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

public class ResourceManager : Singleton<ResourceManager>
{
    public List<LevelBase> ListLevels;
    public List<MachineBase> ListMachines;
    public List<BuyerBase> ListBuyers;
    public List<MenuBase> ListMenus;
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
        resourceManager.ListBuyers = Resources.LoadAll<BuyerBase>("Buyer").ToList();
        resourceManager.ListMenus = Resources.LoadAll<MenuBase>("Menu").ToList();
        resourceManager.ListLevels = Resources.LoadAll<LevelBase>("Level").ToList();
        resourceManager.ListMachines = Resources.LoadAll<MachineBase>("Machine").ToList();
    }
}
#endif

