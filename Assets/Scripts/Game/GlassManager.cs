using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public struct GlassRegistered
    {
        public string glassCode;
        public Glass glass;
        public int seatIndex;
    }

    [System.Serializable]
    public struct GlassPosStruct
    {
        public Transform pos;
        public bool isSpawned;
        public Glass glass;
    }

    public class GlassManager : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject glassPrefab;
        public List<Transform> listPosSpawn;

        [Header("Debug")]
        public int glassCodeCache = 0;
        [SerializeField] List<GlassRegistered> glassRegistereds = new List<GlassRegistered>();

        public void init() // Hook from Env Manager
        {
            for (int i = 0; i < listPosSpawn.Count; i++) reqGlassSpawn(i);
        }

        /// <summary>
        /// Find from last Igrendients 
        /// </summary>
        /// <param name="_igrendient"></param>
        /// <returns>Glass Registered Struct</returns>
        private static GlassRegistered FindGlass(MachineIgrendient _igrendient) =>
            EnvManager.GlassManager.glassRegistereds.Find(val => val.glass.lastIgrendients == _igrendient && val.glass.glassState != GlassState.PROCESS);

        public static bool IsGlassTargetAvaible(MachineIgrendient _lastIgrendient, out GlassRegistered _glassRegistered)
        {
            _glassRegistered = FindGlass(_lastIgrendient);
            return _glassRegistered.glassCode != null;
        }

        #region Glass Spawn
        public void reqGlassSpawn(int _seat)
        {
            Glass _spawn = Instantiate(glassPrefab, listPosSpawn[_seat]).GetComponent<Glass>();

            GlassRegistered _registGlass = new GlassRegistered()
            {
                glass = _spawn,
                glassCode = generateUniqueCode,
                seatIndex = _seat
            };

            StartCoroutine(spawnGlass(_spawn));
            glassRegistereds.Add(_registGlass);
            _spawn.init(_registGlass);
        }

        string generateUniqueCode => $"Glass-{glassCodeCache++}";

        IEnumerator spawnGlass(Glass _glass)
        {
            _glass.gameObject.transform.localScale = Vector2.zero;
            _glass.gameObject.LeanScale(new Vector2(.2f, .2f), .5f).setEaseInBounce().setOnComplete(() =>
            {
                _glass.glassState = GlassState.EMPTY; // Open glass for filled
            });
            yield break;
        }
        #endregion

        public static bool MenuChecker(List<MachineIgrendient> _igrendients, out MenuBase _menubase)
        {
            bool res = false;
            _menubase = ResourceManager.NotValidMenu;
            for (int i = 0; i < ResourceManager.MenuCount; i++)
            {
                if (res = ResourceManager.ListMenus[i].Igrendients.SequenceEqual(_igrendients))
                {
                    _menubase = ResourceManager.ListMenus[i];
                    break;
                }
            }
            if (!res) { }
            return res;
        }
    }
}
