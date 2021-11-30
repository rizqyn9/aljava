using UnityEngine;
using UnityEngine.UI;

namespace Aljava.Level
{
    public class UI_Level : MonoBehaviour
    {
        [Header("Properties")]
        public Image image;
        public TMPro.TMP_Text text;
        public Button btn;
        public Sprite levelUnlock;
        public Sprite levelLocked;
        public Color textLockedColor;

        [Header("Debug")]
        public int index;
        public int level;
        public LevelBase levelBase;
        public LevelModel levelModel;

        public void init(int _index, LevelBase _levelBase = null)
        {
            index = _index;
            level = index + 1;
            levelBase = _levelBase;
            gameObject.name = $"Level-{level}";
            text.text = level.ToString();

            levelModel = LevelStageController.Instance.listLevelUser.Find(val => val.level == level);

            if (!_levelBase || levelModel.Equals(default(LevelModel)) || levelModel.levelState == LevelState.LOCK)
            {
                image.sprite = levelLocked;
                text.color = textLockedColor;
                btn.interactable = false;
                return;
            } else
            {
                image.sprite = levelUnlock;
                btn.interactable = true;
            }

            btn.onClick.AddListener(ClickHandle);       // Add Listener
        }

        private void ClickHandle()
        {
            if (LevelStageController.Instance.isAcceptable)              // Prevent brute force req
            {
                SoundManager.PlayButtonSFX();
                LevelStageController.Instance.reqFromChild(levelBase);
            }
        }
    }
}
