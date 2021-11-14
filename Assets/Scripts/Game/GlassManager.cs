using System;
using System.Collections;
using System.Collections.Generic;
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

        public void init()
        {
            for (int i = 0; i < listPosSpawn.Count; i++) reqGlassSpawn(i);
        }

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
    }
}
