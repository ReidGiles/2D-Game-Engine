using COMP3351_Game_Engine.Interface.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine.Component.Engine
{
    public class PhysicsComponent : IPhysicsComponent
    {
        // Declare a variable type:Vector2, called _gravity
        private Vector2 _gravity;
        // Declare a variable type:Vector2, called _position
        private Vector2 _position;
        // Declare a variable type:Vector2, called _velocity
        private Vector2 _velocity;
        // Declare a variable type:Vector2, called _acceleration
        private Vector2 _acceleration;
        // Declare a variable type:float, called _inverseMass
        private float _inverseMass;
        // Declare a variable type:Vector2, called _restitution
        private Vector2 _restitution;
        // Declare a variable type:Vector2, called _damping
        private Vector2 _damping;

        /// <summary>
        /// Constructor for the PhysicsComponent
        /// </summary>
        /// <param name="position"></param>
        /// <param name="mass"></param>
        /// <param name="restitution"></param>
        /// <param name="damping"></param>
        public PhysicsComponent(Vector2 position,float mass, Vector2 restitution,Vector2 damping)
        {
            // Set gravity
            _gravity = new Vector2(0,2f);

            // Set _position
            _position = position;

            // Set _inverseMass
            _inverseMass = 1 / mass;

            // Set _restitution
            _restitution = restitution;

            // Set _damping
            _damping = damping;

            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
        }

        /// <summary>
        /// Method to update the accelleration to take an inputted force
        /// </summary>
        /// <param name="force"></param>
        public void ApplyForce(Vector2 force)
        {
            _acceleration += force * _inverseMass;
        }

        /// <summary>
        /// Method to update the class to apply phyics
        /// </summary>
        public void UpdatePhysics()
        {
            _velocity *= _damping;
            _velocity += _acceleration;
            _position += _velocity;
            _acceleration = _gravity;
        }

        public void ApplyImpulse(Vector2 closingVelocity)
        {
            closingVelocity *= _restitution;
            _velocity += closingVelocity;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void ResetPosition(Vector2 pPos)
        {
            _position = pPos;
        }

        public void RemoveOverlapY(float pOverlapY)
        {
            _position.Y += pOverlapY;
        }
    }
}
