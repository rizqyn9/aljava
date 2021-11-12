using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

public class ResourceManager : Singleton<ResourceManager>
{
    public List<LevelBase> ListLevel = new List<LevelBase>();
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

        if (GUILayout.Button("Get all reources"))
        {

        }
        if (GUILayout.Button("Get all Level"))
        {
            resourceManager.ListLevel = Resources.LoadAll<LevelBase>("Level").ToList();
        }
    }
}
#endif

