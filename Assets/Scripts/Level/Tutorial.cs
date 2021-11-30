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
        public Machine machineListen = null;

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

        public void TaskCoroutine(Action _task, Func<bool> waitUntil, Action _success) => StartCoroutine(ITask(_task, waitUntil, _success));

        public IEnumerator ITask(Action _task, Func<bool> waitUntil, Action _success)
        {
            _task();
            yield return new WaitUntil(waitUntil);
            _success();
        }

        public virtual void handleTaskClear()
        {
            Debug.Log("Task Succeded");
        }

        public void listenMachine(MachineIgrendient _machineType)
        {
            machineListen = null;
            machineListen = EnvManager.MachineManager.machines.Find(val => val.machineType == _machineType);
        }
    }
}
