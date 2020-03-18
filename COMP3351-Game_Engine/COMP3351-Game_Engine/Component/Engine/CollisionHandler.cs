using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    class CollisionHandler : EventArgs, ICollisionInput
    {
        // String containing the collider tags of entities that collided
        private String[] _collided;
        // int containing the UIDs of the entities that collided
        private int[] _UID;

        private Vector2 _overlap;

        private Vector2 _cNormal;

        /// <summary>
        /// constrcutor for the collision handler
        /// </summary>
        /// <param name="pCollided"></param>
        /// <param name="pUID"></param>
        public CollisionHandler(String[] pCollided, int[] pUID,Vector2 pOverlap, Vector2 pCNormal)
        {
            //Initialise _collided
            _collided = pCollided;
            //Initialise _UID
            _UID = pUID;

            _overlap = pOverlap;

            _cNormal = pCNormal;
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

        public Vector2 GetOverlap()
        {
            return _overlap;
        }

        public Vector2 GetCNormal()
        {
            return _cNormal;
        }
    }
}
