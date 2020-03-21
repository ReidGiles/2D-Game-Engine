using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
    class HostileMind : Mind
    {
        // the change in location
        private Vector2 _dLocation;
        // the movement speed of the entity on x axis
        private float _xSpeed;
        // in air flag
        private bool _inAir;
        // on floor status flag
        private bool _onFloor;
        bool statesDeclared;

        public HostileMind()
        {
            // set _dLocation
            _dLocation = new Vector2(0, 0);
            // set speed
            _xSpeed = 3;
            // set facing direction of the sprite
            _facingDirectionX = 1;
            // set mind ID
            _mindID = "Hostile";
            // set in Air flag to true
            _inAir = true;
            // set onFoor status to false
            _onFloor = false;
        }

        /// <summary>
        /// Required states are declared here to be added to game code.
        /// </summary>
        private void DeclareStates()
        {
            _stateDictionary.Add("Run", new HostileRunState(_entityUID, _animator, _audioPlayer));

            _currentState = _stateDictionary["Run"];
        }

        /// <summary>
        /// method to move the mind and entiry location to match the location of the physics component
        /// </summary>
        /// <returns></returns>
        public override Vector2 Translate()
        {
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

            // Run floor collision logic
            FloorCollision();

            // on collisio with the base of the player and the top of this entity remove this entity from the scene
            if (_collidedWith == "PlayerB" && _collidedThis == "HostileT")
            {
                rtnValue = true;
            }

            // Reset Collided with and this to null
            _collidedWith = null;
            _collidedThis = null;
            _overlap.X = 0;
            _overlap.Y = 0;
            _cNormal.X = 0;
            _cNormal.Y = 0;

            // return rtnValue
            return rtnValue;
        }

        private void FloorCollision()
        {
            // on collision with Floor change floorCollide flag to true
            if (!_onFloor)
            {
                if (_collidedWith == "Floor" && _collidedThis == "HostileB")
                {
                    _inAir = false;
                    _location.Y -= _overlap.Y;
                    _physicsComponent.RemoveOverlapY(-_overlap.Y);
                    _onFloor = true;
                }
            }
        }

        /// <summary>
        /// Move behavior for the hostile
        /// </summary>
        private void Move()
        {
            //Declare a vector to store the force needed to move
            Vector2 force = new Vector2(0, 0);

            force.X = _xSpeed * _facingDirectionX;

            // update facing direction in entity to update texture orientation
            eInvertTexture(_facingDirectionX);
            // apply force to the physics component to move entity
            _physicsComponent.ApplyForce(force);
        }

        public override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;

            // update location
            UpdateLocation(eGetLocation());
            // Move the entity
            Move();
            // Update PhysicsComponent
            _physicsComponent.UpdatePhysics();

            eTranslate(Translate());

            // Declare required states
            if (!statesDeclared)
            {
                DeclareStates();
                statesDeclared = true;
            }

            // Run state machine
            StateMachine();
            _onFloor = false;
        }
    }
}