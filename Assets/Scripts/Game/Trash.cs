using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Trash : MonoBehaviour
    {
        public int countTrashed = 0;
        public void throwTrash()
        {
            countTrashed += 1;
            LeanTween.scale(gameObject, new Vector2(1.1f, 1.1f), .1f).setLoopPingPong(2);
        }
    }
}
