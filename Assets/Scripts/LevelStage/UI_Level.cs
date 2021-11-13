using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Level : MonoBehaviour
{
    [Header("Properties")]
    public Image image;
    public TMPro.TMP_Text text;
    public Button btn;
    public Sprite levelUnlock;
    public Sprite levelLocked;

    [Header("Debug")]
    public int index;
    public int level;
    public LevelBase levelBase;

    public void init(int _index, LevelBase _levelBase = null)
    {
        index = _index;
        level = index + 1;
        levelBase = _levelBase;
        gameObject.name = $"Level-{level}";
        text.text = level.ToString();

        if (!_levelBase)
        {
            image.sprite = levelLocked;
            text.color = new Color(0, 1, 0, .5f);
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
            LevelStageController.Instance.reqFromChild(levelBase);
        }
    }
}
