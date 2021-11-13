using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MachineManager : MonoBehaviour
    {
        [Header("Debug")]
        public EnvManager envManager;
        public List<Machine> machines = new List<Machine>();
        public List<MachineBase> machineBases = new List<MachineBase>();

        public void init()
        {
            envManager = gameObject.GetComponentInParent<EnvManager>();

        }

        public void validateMachineWillInstance()
        {
            // Get all menu
            // Get all machine
            // get different machine
            //GameController.Instance.levelBase.MenuTypeUnlock.ForEach(val =>
            //{
            //    ResourceManager.Instance.ListMenus.Find(val => )
            //})

        }
    }
}
