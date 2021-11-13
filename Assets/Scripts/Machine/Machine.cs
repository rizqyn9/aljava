using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Machine : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject prefabPlace;

        [Header("Debug")]
        public MachineIgrendient machineType;
        public MachineBase machineBase;
        public GameObject machinePrefab;
        public Animator animator;
        public MachineProperties properties;


        public void init(MachineBase _machineBase, int _machineLevel)
        {
            machineBase = _machineBase;
            machineType = machineBase.machineType;

            if(_machineLevel >= machineBase.properties.Count)
            {
                _machineLevel = machineBase.properties.Count;
                Debug.LogWarning($"{gameObject.name} Too much level");
            }

            _machineLevel = _machineLevel == 0 ? 1 : _machineLevel;
            properties = machineBase.properties[_machineLevel - 1];

            instancePrefabs();
            machinePrefab = Instantiate(properties.prefab, prefabPlace.transform);
            animator = machinePrefab.GetComponent<Animator>();
        }

        void instancePrefabs()
        {
            if(prefabPlace.transform.childCount > 0)
                foreach(SpriteRenderer go in prefabPlace.GetComponentsInChildren<SpriteRenderer>())
                    Destroy(go.gameObject);
        }
    }
}
