using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class TemporaryBlockBehaviour : BlockBehaviour
    {
<<<<<<< HEAD
        private float DeactivateDelay = 2;
=======
        public float DeactivateDelay = 2;
>>>>>>> 998f472a287c08ff0e4d3c1dbf1ad94cb3b663cc
        private float nextTime;

        protected override void Activate()
        {
            base.Activate();
            nextTime = UnityEngine.Time.time + DeactivateDelay;
        }

        protected override void Deactivate()
        {
            base.Deactivate();
        }

        void Update()
        {
            base.UpdateBlock();
            if (Time.time > nextTime && IsActivated)
                IsActivated = false;
        }   
    }
}
