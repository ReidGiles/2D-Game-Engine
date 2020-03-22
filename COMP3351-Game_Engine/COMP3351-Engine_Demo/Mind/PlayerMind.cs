﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
    class PlayerMind : Mind, IKeyboardListener
    {
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // the change in location
        private Vector2 _dLocation;
        // the movement speed of the entity on the x axis
        private float _xSpeed;
        // the movement speed of the entity on the y axis
        private float _ySpeed;
        // in air flag
        private bool _inAir;
        // on the floor
        private bool _onFloor;
        private bool _rightCollide;
        // left side of entity collision flag
        private bool _leftCollide;
        // score for the level
        private int _score;
        // collision return value
        bool rtnValue;
        bool statesDeclared;

        public PlayerMind()
        {
            // set args
            _args = new KeyboardHandler();
            // set _dLocation
            _dLocation = new Vector2(0, 0);
            // set _xSpeed
            _xSpeed = 7;
            // set _ySpeed
            _ySpeed = 35;
            // set facing direction
            _facingDirectionX = 1;
            // set player mind ID
            _mindID = "Player";
            // set collision flags
            _inAir = true;
            _onFloor = false;
            _rightCollide = false;
            _leftCollide = false;
            // set starting score
            _score = 0;
        }

        /// <summary>
        /// Required states are declared here to be added to game code.
        /// </summary>
        private void DeclareStates()
        {
            _stateDictionary.Add("Idle", new PlayerIdleState(_entityUID, _animator, _audioPlayer, _args));
            _stateDictionary.Add("Jump", new PlayerJumpState(_entityUID, _animator, _audioPlayer, _args, _physicsComponent));
            _stateDictionary.Add("Run", new PlayerRunState(_entityUID, _animator, _audioPlayer, _args, eInvertTexture, _physicsComponent));

            _currentState = _stateDictionary["Idle"];
        }

        /// <summary>
        /// Required for Input management of the keyboard
        /// </summary>
        public void OnNewKeyboardInput(object sender, IKeyboardInput args)
        {
            // on keyboard event set the keyboard input to args
            _args = args;
            KeyboardInput();
        }

        // Handle Key input
        private void KeyboardInput()
        {
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

        // Handles new collisions args passed by entity
        public override bool OnNewCollision(ICollisionInput args)
        {
            // Run super
            rtnValue = base.OnNewCollision(args);

            // Run floor collision logic
            FloorCollision();

            // On collision with Hostile Bottom collider(HosileB), or Saw collider
            if (_collidedWith == "HostileB" && _collidedThis == "PlayerT" || _collidedWith == "HostileB" && _collidedThis == "PlayerB" || _collidedWith == "Saw" && _collidedThis == "PlayerT" || _collidedWith == "Saw" && _collidedThis == "PlayerB")
            {
                // Run hostile collision logic
                HostileCollision();
            }               

            // Run ceiling collision logic
            CeilingCollision();

            // Run boundary collision logic
            BoundaryCollision();

            // Run coin collision logic
            CoinCollision();

            // Run relic saw collision
            RelicSawCollision();

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
                if (_collidedWith == "Floor" && _collidedThis == "PlayerB")
                {
                    _inAir = false;
                    _location.Y -= _overlap.Y;
                    _physicsComponent.RemoveOverlapY(-_overlap.Y);
                    _onFloor = true;
                }
            }  
        }

        /// <summary>
        /// Hostile collision logic
        /// </summary>
        private void HostileCollision()
        {
            // set return value to true to remove the player from the scene
            rtnValue = true;

            // reset floor collision flag statuses
            _inAir = true;

            // lower players score value when having to respawn, cannot go below 0
            if (_score > 0)
            {
                _score -= 200;
            }
            if (_score < 0)
            {
                _score = 0;
            }
        }

        private void BoundaryCollision()
        {
            // on collision with the Bounday collider and the player while moving right
            if (_collidedWith == "Boundary" && _collidedThis == "PlayerM" && _facingDirectionX == 1)
            {
                // set rightCollide flag to true
                _rightCollide = true;
            }

            // on collision with the Bounday collider and the player while moving left
            if (_collidedWith == "Boundary" && _collidedThis == "PlayerM" && _facingDirectionX == -1)
            {
                // set leftCollide flag to true
                _leftCollide = true;
            }
        }

        private void CeilingCollision()
        {
            // on collision between the ceiling and the player top collider
            if (_collidedWith == "Ceiling" && _collidedThis == "PlayerT")
            {
                // ensure player doesnt pass through ceiling
                _location.Y -= _overlap.Y;
                _physicsComponent.RemoveOverlapY(-_overlap.Y);
            }
        }

        private void CoinCollision()
        {
            // on Collision with a CoinGold collider and the player
            if (_collidedWith == "CoinGold" && _collidedThis == "PlayerB" || _collidedWith == "CoinGold" && _collidedThis == "PlayerT")
            {
                // add points to the player score and out put score to the console
                _score += 100;
                Console.WriteLine("Player Score: " + _score);
            }
        }

        private void RelicSawCollision()
        {
            // on Collision with a RelicSaw collider and the player
            if (_collidedWith == "RelicSaw" && _collidedThis == "PlayerB" || _collidedWith == "RelicSaw" && _collidedThis == "PlayerT")
            {
                // add points to the player score and out put score to the console
                _score += 500;
                Console.WriteLine("Player Score: " + _score);
                Console.WriteLine("Bone Saw Relic Aquired");
            }
        }

        public override void Update(GameTime gameTime)
        {
            _gameTime = gameTime;

            // update location
            UpdateLocation(eGetLocation());
            // Update PhysicsComponent
            _physicsComponent.UpdatePhysics();
            // update the location of the mind and the entity
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