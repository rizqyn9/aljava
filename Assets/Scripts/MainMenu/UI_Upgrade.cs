using System.Collections.Generic;
using UnityEngine;

namespace Aljava.MainMenu
{
    public class UI_Upgrade : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject baseUpgradeItem;
        public Transform placeInstance;
        public TMPro.TMP_Text coins;

        [Header("Debug")]
        public int userCoin;
        public List<MachineBase> machineBases;
        public List<UI_UpgradeItem> listUpgradeItems = new List<UI_UpgradeItem>();

        public void init()
        {
            machineBases = ResourceManager.ListMachines.FindAll(val => val.isUpgradeable);
            machineBases.ForEach(val =>
            {
                UI_UpgradeItem item = Instantiate(baseUpgradeItem, placeInstance).GetComponent<UI_UpgradeItem>();
                listUpgradeItems.Add(item);
                item.init(val);
            });
        }

        private void OnEnable()
        {
            userCoin = GameManager.Instance.userData.point;
            coins.text = userCoin.ToString();
            listUpgradeItems.ForEach(val => val.updateData());
        }
    }
}