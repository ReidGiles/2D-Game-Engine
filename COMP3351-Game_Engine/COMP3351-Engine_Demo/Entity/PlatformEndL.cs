﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
    class PlatformEndL : Entity, ICollisionListener
    {
        public PlatformEndL()
        {

        }

        /// <summary>
        /// Initialisation logic
        /// </summary>
        public override void Initialise()
        {
            // Set initial entity mind:
            _mind = _aiComponentManager.RequestMind<PlatformMind>();
        }

        public void OnNewCollision(object sender, ICollisionInput args)
        {

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

            // SET Left collider to keep an hostile entities on the platform when moving
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height, "Boundary"));

            // Set Collider for the Boundary for patrolling Hostiles collision Box
            ColliderOrigin.X = location.X + 1;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            // SET Left collider to keep an hostile entities on the platform when moving
            _colliders.Add(new RectCollider(ColliderOrigin, 2, texture.Height, "HBoundary"));

            // // Set Collider for the floor
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height, "Floor"));



            // Add the collider list to the mind
            _mind.SetCollider(_colliders.Cast<ICreateCollider>().ToList());

            // SET has collider bool to true
            hasCollider = true;
        }

        /// <summary>
        /// Overides Update() with unique entity behaviour.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // if there are no colliders then set them using SetCollider method
            if (!hasCollider)
            {
                SetCollider();
            }
        }
    }
}
