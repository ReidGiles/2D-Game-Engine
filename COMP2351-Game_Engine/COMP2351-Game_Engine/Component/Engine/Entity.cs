using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    class Entity : IEntity, IUpdatable
    {
        // Entity texture:
        protected Texture2D texture;
        // Entity location:
        protected Vector2 location;
        // Entity texture rotation
        protected float rotation;
        // Entity Origin point for a texture
        protected Vector2 origin;
        // Entity Texture effect used to flip texture
        protected SpriteEffects textureEffect;
        // Entity unique identification number:
        protected int _uid;
        // Entity unique name:
        protected String _uName;
        // Entity AI Component Manager:
        protected IAIComponentManager _aiComponentManager;
        // Entity collider:
        protected List<ICollider> _colliders;
        // Entity mind:
        protected IMind _mind;
        // bool to flag if the enitity has a collider
        protected bool hasCollider = false;
        // bool to flag for the entity to be terminated
        protected bool _killSelf = false;
        
        /// <summary>
        /// Called by scene manager, updates entities on the scene.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Sets entity texture.
        /// </summary>
        /// <param name="pTexture"></param>
        public virtual void SetTexture(Texture2D pTexture)
        {
            texture = pTexture;
            textureEffect = SpriteEffects.None;
            rotation = 0;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        protected void InvertTexture(float pfacingDirection)
        {
            if (pfacingDirection == 1)
            {
                textureEffect = SpriteEffects.None;
            }
            else
            {
                textureEffect = SpriteEffects.FlipHorizontally;
            }
        }

        /// <summary>
        /// Sets entity location
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public virtual void SetLocation(float pX, float pY)
        {
            location.X = pX;
            location.Y = pY;
            if (hasCollider)
            {
                SetCollider();
            }
        }

        /// <summary>
        /// Returns entity unique name.
        /// </summary>
        public Vector2 GetLocation()
        {
            return location;
        }

        /// <summary>
        /// Method to check the the entity needs to be removed
        /// </summary>
        public virtual bool KillSelf()
        {
            bool rtnVal = _killSelf;
            _killSelf = false;
            return rtnVal;
        }

        /// <summary>
        /// Method to set the collider
        /// </summary>
        public virtual void SetCollider()
        {
            
        }

        /// <summary>
        /// Sets entity AI Component Manager
        /// </summary>
        /// <param name="pAIComponentManger"></param>
        public virtual void SetAIComponentManager(IAIComponentManager pAIComponentManger)
        {
            _aiComponentManager = pAIComponentManger;
        }

        /// <summary>
        /// Sets entity mind
        /// </summary>
        /// <param name="pMind"></param>
        public virtual void SetMind(IMind pMind)
        {
            _mind = pMind;
        }

        /// <summary>
        /// Get the _mind
        /// </summary>
        /// <returns></returns>
        public virtual IMind GetMind()
        {
            return _mind;
        }

        /// <summary>
        /// Sets the unique identification number and unique name of the entity.
        /// </summary>
        /// <param name="pUID"></param>
        /// <param name="pUName"></param>
        public virtual void SetUp(int pUID, String pUName)
        {
            _uid = pUID;
            _uName = pUName;
        }

        /// <summary>
        /// Runs on entity creation
        /// </summary>
        public virtual void Initialise()
        {
        }

        /// <summary>
        /// Updates entity location.
        /// </summary>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        public virtual void Translate(float dX, float dY)
        {
            location.X += dX;
            location.Y += dY;
        }

        /// <summary>
        /// Returns entity unique identification.
        /// </summary>
        public int GetUID()
        {
            return _uid;
        }

        /// <summary>
        /// Returns entity unique name.
        /// </summary>
        public String GetUname()
        {
            return _uName;
        }

        /// <summary>
        /// Draws entity on the scene.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location+origin, null, Color.AntiqueWhite, rotation, origin,1, textureEffect, 0);
        }

        /// <summary>
        /// Method to check if the entity has a collider set
        /// <summary>
        public bool CheckCollider()
        {
            return hasCollider;
        }

        /// <summary>
        /// Method to retrieve the list of colliders and store in a list if type ICreateCollider
        /// </summary>
        /// <returns></returns>
        public List<ICreateCollider> GetCollider()
        {
            List<ICreateCollider> L = _colliders.Cast<ICreateCollider>().ToList();
            return L;
        }
    }
}