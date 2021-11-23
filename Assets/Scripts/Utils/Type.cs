using UnityEngine;
using System.Collections.Generic;
using Aljava.Game;


/// <summary>
/// Default handler game state, handle from GameController
/// </summary>
public interface IGameState
{
    /// <summary>
    /// This script has default flow.
    /// <list type="bullet">
    /// <item>Shit for native macos cant support override in interface</item>
    /// <item>Copy this</item>
    /// </list>
    /// <code>GameStateController.UpdateGameState(this)</code>
    /// </summary>
    public void GameStateHandler();
    GameObject GetGameObject();
    public void OnGameInit();
    public void OnGameIddle();
    public void OnGameBeforeStart();
    public void OnGameStart();
    public void OnGamePause();
    public void OnGameClearance();
    public void OnGameFinish();
}

/// <summary>
/// Behaviour for class manager
/// <list type="bullet">
/// Coffee Manager
/// </list>
/// </summary>
public interface IMenuClassManager
{
    MachineClass GetMachineClass();
    //void InstanceMachine(List<MachineData> _machineDatas);
}

[System.Serializable]
public struct InLevelUserData
{
    public List<MachineLevel> machineLevels;
}

[System.Serializable]
public struct MachineLevel
{
    public MachineIgrendient machineIgrendient;
    public int level;
}

[System.Serializable]
public struct MachineProperties
{
    public int level;
    public int maxCapacity;
    public float processDuration;
    public GameObject prefab;
}

[System.Serializable]
public struct SpriteGlassState
{
    public string name;
    public MachineIgrendient igrendient;
    public Sprite sprite;
    public Color color;
}

public enum MachineClass
{
    COFFEE,
    LATTE,
    ADDITIONAL
}

public enum MenuClassification
{
    COFFEE,
    LATTE,
    MILKSHAKE,
    SQUASH,
    ADDITIONAL
}

public enum GameState
{
    NULL,
    INIT,          // STATE BEFORE INITIALIZE GAME
    BEFORE_START,
    START,           // GAME ALREADY TO PLAYING
    PAUSE,          // GAME ON PAUSE TRIGGER
    CLEARANCE,           // GAME STOPPED, CRASH, FINISH 
    FINISH          // GAME FINISHED
}

public enum GameMode
{
    POINT,
    TIME,
    ORDER
}

[System.Serializable]
public struct TransformSeatData
{
    public int index;
    public bool isSeatAvaible;
    public Transform transform;
}

public enum SpawnerState
{
    IDDLE,
    REACTIVE,
    CAN_CREATE,
    MAX_SEAT,
    MAX_ORDER
}

[System.Serializable]
public struct BuyerPrototype
{
    public string customerCode;
    public BuyerBase buyerBase;
    public BuyerState buyerState;
    public TransformSeatData seatData;
    public Vector2 spawnPos;
    public Vector2 seatPos;
    public List<MenuBase> menuListNames;
    public CustomerHandler customerHandler;
}

[System.Serializable]
public struct buyerOrderItemHandler
{
    //public MenuType menu;
    public GameObject itemGO;
}

public enum GlassState : byte
{
    EMPTY,              // glass empty no igrendients
    FILLED,             // glass have once/some igrendients
    PROCESS,            // glass on process, any request will be reject
    VALIDMENU           // glass have igrendients and return a valid Menu Type
}

public enum MachineState : byte
{
    OFF,            // OFF, CANT HANDLING ANY REQUEST
    INIT,
    ON_IDDLE,       // OUTPUT EMPTY
    ON_OVERCOOK,
    ON_REPAIR,
    ON_NEEDREPAIR,
    ON_PROCESS,     // MACHINE GOING PROCESS
    ON_CLEARANCE,
    ON_DONE,        // PROCESSING DONE, BUT OUTPUT NOT EQUALS NULL
}

/// <summary>
/// Machine Type will reference to igrendients
/// </summary>
public enum MachineIgrendient : byte
{
    // Other
    GLASS,
    NULL,

    // Coffee
    BEANS_ARABICA,
    BEANS_ROBUSTA,
    COFEE_MAKER,

    //Latte
    LATTE_CHOCO,
    LATTE_RED_VELVET,
    LATTE_MATCHA,
    LATTE_TARO,

    //Squash
    SQUASH_ORANGE,
    SQUASH_PEACH,
    SQUASH_FRUIT,
    SQUASH_CHIA_SEED,

    //Milkshake
    MILK_MELON,
    MILK_STRAWBERRY,
    MILK_CHOCO,

    //Additional
    FRESHMILK,
    MILK_STEAMMED,
    SODA,
    ICE
}

/// <summary>
/// List menu registered
/// </summary>
public enum MenuListName : byte
{
    NOT_VALID,

    // COFFEE
    ARABICA_COFFEE_LATTE,
    ROBUSTA_COFFEE_LATTE,
    ROBUSTA_ORIGINAL,

    // LATTE
    LATTE_MATCHA,
    LATTE_RED_VELVET,
    LATTE_TARO,
    LATTE_CHOCOLATE,

    // SQUASH
    SQUASH_ORANGE,
    SQUASH_PEACH,
    SQUASH_GRENADINE,

    // MILKSHAKE
    MIILKSHAKE_MELON,
    MIILKSHAKE_STRAWBERRY,
    MIILKSHAKE_CHOCO,
}

public enum enumBuyerType
{
    COWOK,
    CEWEK,
    INDIE,
    OJOL_KANTORAN,
    OJOL
}

public enum BuyerState
{
    ON_IDDLE,
    ON_WAITING,
    ON_DONE
}

public struct MachineDependsType
{
    public MachineIgrendient machineType;
    public Transform gameObjectPosition;
}