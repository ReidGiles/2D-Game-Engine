using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP2351_Game_Engine
{
    class HostileMind : Mind
    {
        // the change in location
        private Vector2 _dLocation;
        // the movement speed of the entity
        private float _speed;
        // in air flag
        private bool _inAir;
        // on floor status flag
        private bool _onFloor;

        public HostileMind()
        {
            // set _dLocation
            _dLocation = new Vector2(0, 0);
            // set speed
            _speed = 3;
            // set facing direction of the sprite
            _facingDirectionX = 1;
            // set mind ID
            _mindID = "Hostile";
            // set in Air flag to true
            _inAir = true;
            // set onFoor status to false
            _onFloor = false;
        }

        public override Vector2 Translate()
        {
            // move the _location by the difference to the physicsComponent location
            _dLocation = _physicsComponent.GetPosition() - _location;
            _location += _dLocation;
            return _dLocation;
        }

        public override bool OnNewCollision(ICollisionInput args)
        {
            bool rtnValue = base.OnNewCollision(args);

            // on collision with HBoundary change facing direction in order to move the opposite direction
            if (_collidedWith == "HBoundary" && _collidedThis == "HostileB" ||  _collidedWith == "HBoundary" && _collidedThis == "HostileT")
            {
                _facingDirectionX *= -1;
            }

            // on collision with another entity change facing direction in order to move the opposite direction
            if (_collidedWith == "HostileB" && _collidedThis == "HostileB")
            {
                _facingDirectionX *= -1;
            }

            // on collision with Floor change floor Collide flag to true
            if (!_onFloor)
            {
                if (_collidedWith == "Floor" && _collidedThis == "PlayerB")
                {
                    _inAir = false;
                    _location.Y -= _overlap.Y;
                    _physicsComponent.RemoveOverlapY(-_overlap.Y);
                    _onFloor = true;
                }
            }

            // on collisio with the base of the player and the top of this entity remove this entity from the scene
            if (_collidedWith == "PlayerB" && _collidedThis == "HostileT")
            {
                rtnValue = true;
            }

            // Reset Collided with and this to null
            _collidedWith = null;
            _collidedThis = null;

            // return rtnValue
            return rtnValue;
        }

        private void Move()
        {
            //Declare a vector to store the force needed to move
            Vector2 force = new Vector2(0, 0);
            //Set the x value of force to speed x direction
            force.X = _speed * _facingDirectionX;

            // update facing direction in entity
            eInvertTexture(_facingDirectionX);
            // apply force to the physics component to move entity
            _physicsComponent.ApplyForce(force);
        }

        public override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;

            // update location
            UpdateLocation(eGetLocation());
            // Call the Move method
            Move();
            // Update PhysicsComponent
            _physicsComponent.UpdatePhysics();
            // update location of mind and entity
            eTranslate(Translate());


            _onFloor = false;
        }
    }
}