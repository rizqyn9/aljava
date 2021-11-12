using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        public void init()
        {
            print("Game Initialize");
        }

        public void Btn_Home()
        {
            GameManager.LoadScene(SceneValid.MAIN_MENU);
        }
    }
}
