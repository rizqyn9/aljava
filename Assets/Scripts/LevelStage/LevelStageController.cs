using System.Collections.Generic;
using UnityEngine;
using Aljava.MainMenu;

namespace Aljava.Level
{
    public class LevelStageController : Singleton<LevelStageController>
    {
        [Header("Properties")]
        public GameObject levelStageContainer;
        public GameObject levelPrefab;
        public int minShowLevel = 10;

        [Header("Debug")]
        public int resCount = 0;
        public List<UI_Level> levels = new List<UI_Level>();
        public bool isAcceptable = true; // Prevent Brute Force
        public List<LevelModel> listLevelUser = new List<LevelModel>();

        public void init()
        {
            resCount = ResourceManager.ListLevels.Count;
            int render = resCount > minShowLevel ? resCount : minShowLevel;

            listLevelUser = GameManager.Instance.userData.listLevels;

            for(int i = 0; i < render; i++)
            {
                instanceLevelChild(i);
            }
        }

        void instanceLevelChild(int _index)
        {
            UI_Level _level = Instantiate(levelPrefab, levelStageContainer.transform).GetComponent<UI_Level>();
            levels.Add(_level);

            _level.init(
                _index,
                getLevelBase(_index)
            );
        }

        LevelBase getLevelBase(int _index)
        {
            if (_index >= resCount) return null;
            return ResourceManager.ListLevels[_index];
        }


        public void reqFromChild(LevelBase _levelBase)
        {
            isAcceptable = false;
            print($"req from child {_levelBase.level}");
            GameManager.LoadLevel(_levelBase);
        }

        public void Btn_Close()
        {
            MainMenuController.Instance.Btn_Level();
        }
    }
}
