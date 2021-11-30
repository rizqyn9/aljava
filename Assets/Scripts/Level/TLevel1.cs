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
            listAction = new List<Action>()
            {
                dialogFirst,
                dialogSecond,
                dialogThird,
                dialogFourth
            };
            StartCoroutine(IStartGame());
        }

        IEnumerator IStartGame()
        {
            GameController.GameState = GameState.INIT;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart / 2);

            GameController.GameState = GameState.BEFORE_START;

            //foreach(Action action in listAction)
            //{
            //    StartCoroutine(IBaseListener(action));
            //    yield return new WaitUntil(() => canNext);
            //}

            Time.timeScale = 1;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart);

            GameController.GameState = GameState.START;

            //StartCoroutine(taskMachineOverlay());

            miniGameTask1();
            yield break;
        }

        void miniGameTask1() =>
            TaskCoroutine(
                    dialogFifth,
                    () => UIGameManager.Converse.isNextClicked,
                    miniGameTask2
                );

        void miniGameTask2() =>
            TaskCoroutine(
                    dialogSixth,
                    () => UIGameManager.Converse.isNextClicked,
                    miniGameTask3
                );

        void miniGameTask3() =>
            TaskCoroutine(
                    dialogSeventh,
                    () => machineListen.machineOverlay.isInteractable,
                    () => { print("Mantap"); }
                );

        void dialogFirst() =>
            UIGameManager.Converse
                .showDialog("Disini kamu akan melayani pelanggan yang datang ke Cafe ini", false);

        void dialogSecond() =>
            UIGameManager.Converse
                .showDialog("Jumlah pelanggan yang harus kamu layani dapat kamu lihat pada ikon diatas ini", false);

        void dialogThird() =>
            UIGameManager.Converse
                .showDialog("Kamu dapat melihat sisa jam kerja mu disini", false);

        void dialogFourth() =>
            UIGameManager.Converse
                .showDialog("pastikan kamu melayani semua pelanggan sebelum waktu kerjamu habis", true);

        void dialogFifth() =>
            UIGameManager.Converse
                .showDialog("Sekarang aku akan memberitahumu bagaimana untuk mengoperasikan mesin yang ada disini", false);

        void dialogSixth() =>
            UIGameManager.Converse
                .showDialog("Setiap harinya kamu harus mengatur MESIN GRINDER dan juga MESIN COFFEE MAKER", false);

        void dialogSeventh() =>
            UIGameManager.Converse
                .showDialog
                    (
                        "cobalah tekan mesin grinder",
                        true,
                        _showNextBtn: false,
                        _cbOnStart: () => {
                            listenMachine(MachineIgrendient.BEANS_ROBUSTA);
                            Time.timeScale = 1;
                            },
                        _cbOnDone: () => machineListen.canBypassAuthorize = true
                    );
    }
}
