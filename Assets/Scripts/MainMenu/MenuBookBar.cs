using UnityEngine;
using UnityEngine.UI;

namespace Aljava.MainMenu
{
    public class MenuBookBar : MonoBehaviour
    {
        public Transform instanceBarPlace;
        public GameObject baseBar;
        public TMPro.TMP_Text title;
        public Sprite active, deactive;

        public void init(int _idTitle, int totalBar)
        {
            title.text = _idTitle == 1 ? "Bitterness" : _idTitle == 2 ? "Sweetness" : "Acidity";
            for (int i =0; i < 3; i++)
                Instantiate(baseBar, instanceBarPlace).GetComponent<Image>().sprite = i > totalBar ? deactive : active;
        }
    }
}
