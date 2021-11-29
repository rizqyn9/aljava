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
            StartCoroutine(IBaseListener());
            StartCoroutine(IStartGame());
        }

        IEnumerator IStartGame()
        {
            GameController.GameState = GameState.INIT;

            yield return new WaitForSeconds(GameController.GameProperties.delayStart / 2);

            GameController.GameState = GameState.BEFORE_START;
            UIGameManager.Converse.setDialog("Disini kamu akan melayani pelanggan yang datang ke Cafe ini");
            yield return new WaitForSeconds(GameController.GameProperties.delayStart);

            GameController.GameState = GameState.START;
            requestSuccess = true;
            yield break;
        }
    }
}
