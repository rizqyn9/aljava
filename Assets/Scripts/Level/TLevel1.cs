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

            foreach(Action action in listAction)
            {
                StartCoroutine(IBaseListener(action));
                yield return new WaitUntil(() => canNext);
            }

            Time.timeScale = 1;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart);

            GameController.GameState = GameState.START;
            yield break;
        }

        public void dialogFirst() =>
            UIGameManager.Converse
                .showDialog("Disini kamu akan melayani pelanggan yang datang ke Cafe ini", false);

        public void dialogSecond() =>
            UIGameManager.Converse
                .showDialog("Jumlah pelanggan yang harus kamu layani dapat kamu lihat pada ikon diatas ini", false);

        public void dialogThird() =>
            UIGameManager.Converse
                .showDialog("Kamu dapat melihat sisa jam kerja mu disini", false);

        public void dialogFourth() =>
            UIGameManager.Converse
                .showDialog("pastikan kamu melayani semua pelanggan sebelum waktu kerjamu habis", true);
    }
}
