using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class MachineManager : MonoBehaviour
    {
        public List<Transform> lattePos;

        [Header("Debug")]
        public EnvManager envManager;
        public List<Machine> machines = new List<Machine>();

        public void init(EnvManager _envManager)
        {
            envManager = _envManager;
            instanceMachine();
        }

        public void OnGameBeforeStart() => machines.ForEach(val => val.OnGameBeforeStart());
        public void OnGameStart()
        {
            machines.ForEach(val => val.OnGameStart());
        }

        public void setInteractableMachine(bool _isInteract) => machines.ForEach(val => val.isInteractable = _isInteract);

        public static bool IsMachineTargetAvaible(MachineIgrendient _machineTarget, out Machine _machine)
        {
            _machine = EnvManager.Instance.machineManager.machines.Find(val => val.machineType == _machineTarget && val.machineState == MachineState.ON_IDDLE);
            return _machine;
        }

        public void OnGameFinished() => setInteractableMachine(false);

        #region SetUp Machine
        void instanceMachine()
        {
            foreach(MachineBase _machineBase in envManager.listMachines)
            {
                if (!_machineBase.basePrefab) return;
                Machine _machine = Instantiate(_machineBase.basePrefab, getTransform(_machineBase)).GetComponent<Machine>();
                _machine.init(_machineBase, getLevelMachine(_machineBase));
                machines.Add(_machine);
            }
        }

        Transform getTransform(MachineBase _machineBase)
        {
            if (_machineBase.machineClass == MachineClass.LATTEE)
                return lattePos.Find(val => val.childCount == 0).transform;
            else
                return transform;
        }

        int getLevelMachine(MachineBase _machineBase)
        {
            try
            {
                int res = GameManager.Instance.userData.userEnvDatas.Find(val => val.machineType == _machineBase.machineType).level;
                if(res > _machineBase.properties.Count || res == 0)
                {
                    Debug.LogWarning($"{_machineBase.machineType.ToString()} to much level value || value == 0");
                    return 1;
                } 
                return res;
            } catch
            {
                return 1;
            }
        }
        #endregion
    }
}
