using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class CustomerManager : Singleton<CustomerManager>, IGameState
    {
        [Header("Properties")]
        public List<TransformSeatData> transformSeatDatas = new List<TransformSeatData>();
        public List<Transform> spawnPos = new List<Transform>();
        public GameObject baseCustomer;


        [Header("Debug")]
        public SpawnerState spawnerState = SpawnerState.IDDLE;
        public int seatCount, buyerNow = 0;
        public bool gameTimeOut = false;


        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameInit() { }

        public void OnGameBeforeStart()
        {
            seatCount = transformSeatDatas.Count;
        }

        public void OnGameStart()
        {
            spawnerState = SpawnerState.CAN_CREATE;
        }

        public void OnGameClearance()
        {
            spawnerState = SpawnerState.MAX_ORDER;
        }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGamePause() { }
        #endregion

        [SerializeField] List<TransformSeatData> avaibleSeat;
        void Update()
        {
            if(spawnerState == SpawnerState.CAN_CREATE && buyerNow <= seatCount )
            {
                avaibleSeat = findAvaibleSeat();
                if(avaibleSeat.Count > 0)
                {
                    createCustomer();
                }
            }
        }

        [SerializeField] int customerCounter = 0;
        void createCustomer()
        {
            TransformSeatData seatData = avaibleSeat[Random.Range(0, avaibleSeat.Count)];

            buyerNow += 1;
            GameController.RulesController.buyerInstance += 1;
            TransformSeatData tsd = transformSeatDatas[seatData.index];         // To modify the struct, first assign it to a local variable, modify the variable, then assign the variable back to the item in the collection
            tsd.isSeatAvaible = false;
            transformSeatDatas[seatData.index] = tsd;

            BuyerPrototype buyerPrototype = new BuyerPrototype()
            {
                buyerBase       = EnvManager.ListBuyers[Random.Range(0, EnvManager.ListBuyers.Count)],
                customerCode    = $"Customer-{customerCounter++}",
                seatData        = seatData,
                menuListNames   = getMenuTypes(Random.Range(1, 2)),
                spawnPos        = spawnPos[Random.Range(0, spawnPos.Count)].position,
                seatPos         = seatData.transform.position
            };

            CustomerHandler customer = Instantiate(baseCustomer, buyerPrototype.spawnPos, Quaternion.identity, transform).GetComponent<CustomerHandler>();
            customer.init(buyerPrototype);

            StartCoroutine(IReactiveSpawner());
        }

        public void onLeave(int _seatIndex)
        {
            TransformSeatData tsd = transformSeatDatas[_seatIndex];
            tsd.isSeatAvaible = true;
            transformSeatDatas[_seatIndex] = tsd;

            if (gameTimeOut)
            {
                if (findAvaibleSeat().Count == 3)
                    GameController.RulesController.handleCustomerGameManagerTimeOut();
            }
        }


        #region Depends
        public List<TransformSeatData> findAvaibleSeat() => transformSeatDatas.FindAll(val => val.isSeatAvaible);

        IEnumerator IReactiveSpawner()
        {
            spawnerState = SpawnerState.REACTIVE;
            yield return new WaitForSeconds(GameController.Instance.levelBase.delayPerCustomer);
            if (spawnerState != SpawnerState.REACTIVE) yield break;
            spawnerState = SpawnerState.CAN_CREATE;
            yield break;
        }

        List<MenuBase> getMenuTypes(int _total)
        {
            List<MenuBase> res = new List<MenuBase>();
            for (int i = 0; i < _total; i++) res.Add(EnvManager.ListMenus[Random.Range(0, EnvManager.ListMenus.Count)]);
            return res;
        }
        #endregion

    }
}
