using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    abstract class Mind : IMind, IUpdatable
    {
        // Entity texture:
        protected Texture2D _texture;
        // Entity texture FacingDirection
        protected float _facingDirectionX;
        // Entity location:
        protected Vector2 _location;
        // reference to the mind ID
        protected String _mindID;
        // list of the colliders in the mind
        protected List<ICreateCollider> _colliders;
        // this entities collider that is colliding
        protected String _collidedThis;
        // enitity that collided with this entities collider
        protected String _collidedWith;
        // this entities collider that is collidings UID
        protected int _collidedThisUID;
        // enitity that collided with this entities collider UID
        protected int _collidedWithUID;

        /// <summary>
        /// Updates entity location
        /// </summary>
        /// <param name="pLocation"></param>
        public void UpdateLocation(Vector2 pLocation)
        {
            // set _location
            _location = pLocation;
        }

        /// <summary>
        /// Updates entity texture
        /// </summary>
        /// <param name="pTexture"></param>
        public float UpdateTexture(Texture2D pTexture)
        {
            // set texture
            _texture = pTexture;

            return _facingDirectionX;
        }

        /// <summary>
        /// Updates entity texture
        /// </summary>
        /// <param name="pColliders"></param>
        public void SetCollider(List<ICreateCollider> pColliders)
        {
            // set _colliders
            _colliders = pColliders;
        }

        /// <summary>
        /// Translates x position
        /// </summary>
        public virtual float TranslateX()
        {
            return 0;
        }

        /// <summary>
        /// Translates y position
        /// </summary>
        public virtual float TranslateY()
        {
            return 0;
        }

        /// <summary>
        /// called when a collision event occures
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual bool OnNewCollision(ICollisionInput args)
        {
            // check if the entity has any colliders
            if (_colliders != null)
            {
                // find which collider in the _colliders is colliding
                foreach (ICreateCollider i in _colliders)
                {
                    // set _colliderThis and _collided with
                    if (i.GetTag() == args.GetCollided()[0])
                    {
                        _collidedThis = args.GetCollided()[0];
                        _collidedWith = args.GetCollided()[1];
                        _collidedThisUID = args.GetUID()[0];
                        _collidedWithUID = args.GetUID()[1];
                    }
                    else if(i.GetTag() == args.GetCollided()[1])
                    {
                        _collidedThis = args.GetCollided()[1];
                        _collidedWith = args.GetCollided()[0];
                        _collidedThisUID = args.GetUID()[1];
                        _collidedWithUID = args.GetUID()[0];
                    }
                }
            } 
            //return whether the entity should kill itself
            return false;
        }

        /// <summary>
        /// Updates mind, called by AIComponentManager
        /// </summary>
        public virtual void Update()
        {
        }
    }
}