using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MachineManager : MonoBehaviour
    {
        [Header("Debug")]
        public EnvManager envManager;
        public List<Machine> machines = new List<Machine>();

        public void init(EnvManager _envManager)
        {
            envManager = _envManager;
            instanceMachine();
        }

        public void OnGameBeforeStart() => machines.ForEach(val => val.OnGameBeforeStart());

        void instanceMachine()
        {
            foreach(MachineBase _machineBase in envManager.listMachines)
            {
                if (!_machineBase.basePrefab) return;
                Machine _machine = Instantiate(_machineBase.basePrefab, transform).GetComponent<Machine>();
                _machine.init(_machineBase, getLevelMachine(_machineBase));
                machines.Add(_machine);
            }
        }

        int getLevelMachine(MachineBase _machineBase)
        {
            int res = GameManager.Instance.userData.userEnvDatas.Find(val => val.machineType == _machineBase.machineType).level;
            if(res > _machineBase.properties.Count || res == 0)
            {
                Debug.LogWarning($"{_machineBase.machineType.ToString()} to much level value || value == 0");
                return 1;
            } 
            return res;
        }
    }
}
