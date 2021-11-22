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

        public void init(string _title, int totalBar)
        {
            title.text = _title;
            for(int i =0; i < 3; i++)
                Instantiate(baseBar).GetComponent<Image>().sprite = i > totalBar ? deactive : active;
        }
    }
}
