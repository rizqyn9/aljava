using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public enum TutorialState
    {
        PENDING,
        RESOLVE,
        REJECT
    }

    public abstract class Tutorial : MonoBehaviour
    {
        public LevelBase levelBase;
        public List<Action> listAction;

        public bool canNext = false;

        /**
         * Customer come and order menu 
         */
        public virtual void init()
        {

        }

        public TutorialState tutorialState = TutorialState.PENDING;
        public IEnumerator IBaseListener(Action _action)
        {
            tutorialState = TutorialState.PENDING;
            canNext = false;

            _action();

            yield return new WaitUntil(() => tutorialState != TutorialState.PENDING);

            canNext = true;
            handleTaskClear();

            yield break;
        }

        public virtual void handleTaskClear()
        {
            Debug.Log("Task Succeded");
        }
    }
}
