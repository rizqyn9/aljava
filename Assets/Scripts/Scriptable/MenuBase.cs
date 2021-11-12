using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuBase", menuName = "ScriptableObject/MenuBase")]
public class MenuBase : ScriptableObject
{
    public string menuName;
    public MenuListName menuListName;
    public GameObject menuPrefab;
    public List<MachineIgrendient> Igrendients;
    public string menuDesc;
    public Sprite menuSprite;
    public int pointInGame;
    public int price;
}
