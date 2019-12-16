using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class RectCollider : ICollider,ICreateCollider
    {
        //origin point of the rectangle collider
        private Vector2 _origin;

        //width of the collider
        private float _width;

        //height of the collider
        private float _height;

        //Tag used to identigy type of collider
        private String _colliderTag;

        /// <summary>
        /// class contructor
        /// </summary>
        /// <param name="pOrigin"></param>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <param name="pColliderTag"></param>
        public RectCollider(Vector2 pOrigin, float pWidth, float pHeight, String pColliderTag)
        {
            //set origin
            _origin = pOrigin;

            //set width
            _width = pWidth;

            //set height
            _height = pHeight;

            //set collider tag
            _colliderTag = pColliderTag;
        }

        /// <summary>
        /// Translate the position of the Collider
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Translate(float dx, float dy)
        {
            //Set x
            _origin.X += dx;
            //set y
            _origin.Y += dy;
        }


        /// <summary>
        /// Method to return the collider centre point, width, and height
        /// </summary>
        /// <returns></returns>
        public float[] CreateCollider()
        {
            float[] retVal = { _origin.X, _origin.Y, _width, _height };

            return retVal;
        }

        /// <summary>
        /// Method to return the _colliderTag
        /// </summary>
        /// <returns></returns>
        public String GetTag()
        {
            return _colliderTag;
        }
    }
}
