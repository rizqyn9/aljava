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
        public Converse converse => UIGameManager.Converse;
        public List<Action> listAction;

        public bool canNext = false;
        public Machine machineListen = null;
        public MachineIgrendient machineAuthorized;

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

        //public 

        public void TaskCoroutine(Action _task, Func<bool> waitUntil, Action _success = null, bool _usePause = false) => StartCoroutine(ITask(_task, waitUntil, _success, _usePause));

        public IEnumerator ITask(Action _task, Func<bool> waitUntil, Action _success = null, bool _usePause = false)
        {
            if (_usePause) Time.timeScale = 0;
            _task();
            yield return new WaitUntil(waitUntil);
            _success?.Invoke();
            if (_usePause) Time.timeScale = 1;
        }

        public virtual void handleTaskClear()
        {
            Debug.Log("Task Succeded");
        }

        public void getListenMachine(MachineIgrendient _machineType)
        {
            machineListen = null;
            machineListen = EnvManager.MachineManager.machines.Find(val => val.machineType == _machineType);
        }
    }
}
