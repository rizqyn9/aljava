using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public abstract class Tutorial : MonoBehaviour
    {
        public LevelBase levelBase;
        public List<Action<bool>> act;

        /**
         * Customer come and order menu 
         */
        public virtual void init()
        {

        }

        public bool requestSuccess = false;
        public IEnumerator IBaseListener()
        {
            yield return new WaitUntil(() => requestSuccess);
            handleTaskClear();
            yield break;
        }

        public virtual void handleTaskClear()
        {
            Debug.Log("Task Succeded");
        }
    }
}
