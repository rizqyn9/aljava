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
        public MachineBase machineBase;
        public Animator animator;
        public MachineProperties properties;
    }
}
