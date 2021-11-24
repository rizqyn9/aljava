using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public abstract class Tutorial : MonoBehaviour
    {
        public LevelBase levelBase;
        public List<Action<bool>> act;

        /**
         * Customer come and order menu 
         */
        public virtual void init()
        {

        }
    }
}
