using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class CollisionHandler : EventArgs, ICollisionInput
    {
        // String containing the collider tags of entities that collided
        private String[] _collided;
        // int containing the UIDs of the entities that collided
        private int[] _UID;

        /// <summary>
        /// constrcutor for the collision handler
        /// </summary>
        /// <param name="pCollided"></param>
        /// <param name="pUID"></param>
        public CollisionHandler(String[] pCollided, int[] pUID)
        {
            //Initialise _collided
            _collided = pCollided;
            //Initialise _UID
            _UID = pUID;
        }

        /// <summary>
        /// Method to return _collided
        /// </summary>
        /// <returns></returns>
        public String[] GetCollided()
        {
            return _collided;
        }

        /// <summary>
        /// Method to return _UID
        /// </summary>
        /// <returns></returns>
        public int[] GetUID()
        {
            return _UID;
        }
    }
}
