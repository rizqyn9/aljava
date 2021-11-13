using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        public LevelBase levelBase;

        public void init(LevelBase _levelbase)
        {
            print("Game Initialize");
            levelBase = _levelbase;
        }

        public void Btn_Home()
        {
            print("TOuched");
            GameManager.LoadScene(SceneValid.MAIN_MENU);
        }
    }
}
