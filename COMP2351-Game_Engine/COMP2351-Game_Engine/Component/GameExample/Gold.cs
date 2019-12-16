using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class Gold : RelicHunterEntity, ICollisionListener
    {
        public Gold()
        { }

        public override void Initialise()
        {
            // Set initial entity mind:
            _mind = _aiComponentManager.RequestMind<GoldMind>();
        }

        public void OnNewCollision(object sender, ICollisionInput args)
        {
            // Check if this entity is the one colliding
            if (_uid == args.GetUID()[0] || _uid == args.GetUID()[1])
            {
                // If entity is not flagged for the removal from the scene using _killSelf
                if (!_killSelf)
                {
                    //set _killSelf to the result of the collision method in the mind
                    this._killSelf = _mind.OnNewCollision(args);
                }
            }
        }

        public override void SetCollider()
        {
            // Create a list of colliders
            _colliders = new List<ICollider>();
            // create an origin point for the collider
            Vector2 ColliderOrigin;

            // Set Collider for the overall collision Box (encompasses all other colliders within its area)
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height, "Overall"));

            // Set Collider for the Top of the Player
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height, "CoinGold"));

            // Add the collider list to the mind
            _mind.SetCollider(_colliders.Cast<ICreateCollider>().ToList());

            // SET has collider bool to true
            hasCollider = true;
        }

        /// <summary>
        /// Overides Update() with unique entity behaviour.
        /// </summary>
        public override void Update()
        {
            // if there are no colliders then set them using SetCollider method
            if (!hasCollider)
            {
                SetCollider();
            }
        }
    }
}
