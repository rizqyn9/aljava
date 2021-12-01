using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.MainMenu
{
    public class UI_UpgradeItem : MonoBehaviour
    {
        [Header("Properties")]
        public Image icon;
        public TMPro.TMP_Text machineName;
        public List<Image> listIndicator;

        public TMPro.TMP_Text priceText;
        public Button priceBtn;

        public MachineBase machineBase;
        public int level;

        public void init(MachineBase _machine)
        {
            machineBase = _machine;
            machineName.text = _machine.machineName;
            if (machineBase.iconUpgradeable) icon.sprite = machineBase.iconUpgradeable;

            listIndicator.ForEach(val => val.enabled = false);

            updateData();
        }

        public void updateData()
        {
            int index = GameManager.Instance.userData.userEnvDatas.FindIndex(val => val.machineType == machineBase.machineType);
            if(index >= 0)
            {
                level = GameManager.Instance.userData.userEnvDatas[index].level;
            } else
            {
                level = 1;
            }
            if(level >= 3)
            {
                priceBtn.interactable = false;
                priceText.text = "MAX";
            } else if(MainMenuController.Instance.UI_Upgrade.userCoin < machineBase.properties[level - 1].amout)
            {
                priceBtn.interactable = false;
                priceText.text = machineBase.properties[level - 1].amout.ToString();
            } else
            {
                priceBtn.interactable = true;
                priceText.text = machineBase.properties[level - 1].amout.ToString();
            }
            for (int i = 0; i < level; i++) listIndicator[i].enabled = true;
        }

        public void Btn_Upgrade()
        {

        }
    }
}
