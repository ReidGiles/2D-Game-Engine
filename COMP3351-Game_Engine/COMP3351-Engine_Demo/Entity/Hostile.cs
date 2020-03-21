using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3351_Game_Engine;
using COMP3351_Game_Engine.Component.Engine;

namespace COMP3351_Engine_Demo
{
    class Hostile : Entity, ICollisionListener
    {
        public Hostile()
        {
        }

        /// <summary>
        /// Initialis mind and physics component
        /// </summary>
        public override void Initialise()
        {
            // Set initial entity mind:
            SetMind(_aiComponentManager.RequestMind<HostileMind>());
            _physicsComponent = new PhysicsComponent(location, 1, new Vector2(0f, 0.9f), new Vector2(0f, 0.9f));
            _mind.SetPhysicsComponent(_physicsComponent);
            _mind.SetEntityUID(GetUID());
        }

        /// <summary>
        /// On collision call collision method on mind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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

        /// <summary>
        /// Set the colliders for the hosile
        /// </summary>
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
            ColliderOrigin.Y = location.Y + 0.25f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height / 2, "HostileT"));

            // Set Collider for the Bottom of the Player
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 1f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height / 2, "HostileB"));

            // Add the collider list to the mind
            _mind.SetCollider(_colliders.Cast<ICreateCollider>().ToList());

            // SET has collider bool to true
            hasCollider = true;
        }
    }
}
