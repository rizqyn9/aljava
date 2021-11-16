using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject basePrefab;
        public Color colorDeactive;
        public Vector2 offset;

        [Header("Debug")]
        [SerializeField] int instance;
        public List<Image> healths;

        public void init(int _total)
        {
            instance = _total;
            for (int i = 0; i < _total; i++)
            {
                Image image = Instantiate(basePrefab, transform).GetComponent<Image>();
                healths.Add(image);
                image.transform.position = new Vector2((offset.x * i) + 20, image.transform.position.y);
            }
        }

        public void decrement()
        {
            healths[instance - 1].color = colorDeactive;
            animate(instance - 1);
            instance -= 1;
            validateHealth();
        }

        private void validateHealth()
        {
            if (instance < 1) GameController.RulesController.handleHealthRunOut();
        }

        public void increment()
        {
            healths[instance - 1].color = Color.white;
            animate(instance - 1);
            instance += 1;
        }

        public void animate(int target) => LeanTween.scale(healths[target].gameObject, new Vector2(1.2f, 1.2f), .2f).setLoopPingPong(2);
    }
}
