using UnityEngine;

namespace Game
{
    public class UI_Lose : MonoBehaviour
    {

        [ContextMenu("Test")]
        public void Test()
        {
            init();
        }

        public void init()
        {
            LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 0, 2f).setEaseInBounce()
                .setOnStart(() => UIGameManager.Instance.noClickSetActive(true));
        }
    }
}
