using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageController : Singleton<LevelStageController>
{
    public void init()
    {
        
        Debug.LogWarning("Level Stage");
    }

    public void Btn_Close()
    {
        GameManager.UnLoadScene(SceneValid.LEVEL_STAGE);
    }
}
