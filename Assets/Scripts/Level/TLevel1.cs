using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class TLevel1 : Tutorial
    {
        public override void init()
        {
            base.init();
            Debug.LogWarning("Tutorial mode");
            StartCoroutine(IStartGame());
        }

        IEnumerator IStartGame()
        {
            GameController.GameState = GameState.INIT;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart / 2);

            GameController.GameState = GameState.BEFORE_START;

            introGameTask1();
            yield return new WaitUntil(() => canNext);

            Time.timeScale = 1;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart);

            GameController.GameState = GameState.START;

            Time.timeScale = 0;
            baseMechanic1();
            yield return new WaitUntil(() => canNext);

            miniGameTask1();
            yield break;
        }

        #region Intro
        void introGameTask1() =>
            TaskCoroutine(
                () => converse.showDialog("Hi, Welcome to Aljava Cafe, I am the manager of this cafe and I will be your guide", false),
                () => converse.isNextClicked,
                introGameTask2
                );

        void introGameTask2() =>
            TaskCoroutine(
                () => converse.showDialog("And I will tell you what work you have to do", true),
                () => converse.isNextClicked,
                () => canNext = true
                );
        #endregion

        #region baseMechanics
        void baseMechanic1() =>
            TaskCoroutine(
                () => converse.showDialog("In here you will serve customers who come to this cafe", false),
                () => converse.isNextClicked,
                baseMechanic2
                );

        // HightLight Customer Target
        void baseMechanic2() =>
            TaskCoroutine(
                () => converse.showDialog("The number of customers you have to serve you can see on the icon at the top of your ", false),
                () => converse.isNextClicked,
                baseMechanic3
                );

        void baseMechanic3() =>
            TaskCoroutine(
                () => converse.showDialog("Everyday you will work according to the specified time", false),
                () => converse.isNextClicked,
                baseMechanic4
                );

        // HightLight Timer
        void baseMechanic4() =>
            TaskCoroutine(
                () => converse.showDialog("You can see your remaining working time here", false),
                () => converse.isNextClicked,
                baseMechanic5
                );

        void baseMechanic5() =>
            TaskCoroutine(
                () => converse.showDialog("Make sure you serve all customers before your working time runs out", false),
                () => converse.isNextClicked,
                () => canNext = true
                );
        #endregion

        #region beansMachine
        void miniGameTask1() =>
            TaskCoroutine(
                    () => converse.showDialog("Now I will tell you how to operate the machines here", false),
                    () => converse.isNextClicked,
                    miniGameTask2
                );

        void miniGameTask2() =>
            TaskCoroutine(
                    () => converse.showDialog("Every day you have to set up the GRINDER MACHINE and also the COFFEE MACHINE ", false),
                    () => converse.isNextClicked,
                    miniGameTask3
                );

        // Hightlight beans Machine
        void miniGameTask3() =>
            TaskCoroutine(
                    () => converse.showDialog(
                            "Try pressing the grinder machine",
                            true,
                            _showNextBtn: true,
                            _cbOnStart: () => getListenMachine(MachineIgrendient.BEANS_ROBUSTA),
                            _cbOnDone: () => machineListen.canBypassAuthorize = true
                        ),
                    () => converse.isNextClicked,
                    miniGameTask4
                );

        // Listen Machine open overlay beans
        void miniGameTask4() =>
            TaskCoroutine(
                    () => machineListen.showHightLight(),
                    () => machineListen.machineOverlay.isInteractable,
                    miniGameTask5
                );

        void miniGameTask5() =>
            TaskCoroutine(
                    () => converse.showDialog("Press the button 'fill beans' to fill the coffee beans on to the grinder", false),
                    () => machineListen.machineOverlay.isOverlayResolve,
                    miniGameTask6
                    );

        void miniGameTask6() =>
            TaskCoroutine(
                    () => converse.showDialog("Then press on done button so the grinder machine is ready to use", true),
                    () => machineListen.machineOverlay.isMiniGameSuccess,
                    miniGameTask7
                );

        // Highlight Coffee Maker
        void miniGameTask7() =>
            TaskCoroutine(
                    () => converse.showDialog("Now is the time to set the coffee machine, press the coffee machine", true),
                    () => machineListen.machineOverlay.isMiniGameSuccess,
                    miniGameTask8
                );

        void miniGameTask8() =>
            TaskCoroutine(
                    () => converse.showDialog("Set the coffee maker temperature to '75'", false),
                    () => machineListen.machineOverlay.isMiniGameSuccess,
                    miniGameTask9
                );
        
        void miniGameTask9() =>
            TaskCoroutine(
                    () => converse.showDialog("If it's set up correctly then you can press the done button", true),
                    () => machineListen.machineOverlay.isMiniGameSuccess,
                    miniGameTask10
                );
        
        void miniGameTask10() =>
            TaskCoroutine(
                    () => converse.showDialog("Now all the machines are ready. it's time to serve your first customer", true),
                    () => machineListen.machineOverlay.isMiniGameSuccess,
                    () => { }
                );

        #endregion

    }
}
