using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace COMP2351_Game_Engine
{
    class PlayerMind : Mind, IKeyboardListener
    {
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // the effect of gravity on the entity
        private float _gravity;
        // the movement speed of the entity
        private float _speed;
        // the valuse used to make the entity jump vertically
        private float _jump;
        // used to push the ntity out of the floor when colliding
        private float _counterForce;
        // the velocity of the entity
        private Vector2 _velocity;
        // floor collision flag
        private bool _floorCollide;
        // in air flag
        private bool _inAir;
        // on floor status flag
        private bool _onFloor;
        // right side of entity collision flag
        private bool _rightCollide;
        // left side of entity collision flag
        private bool _leftCollide;
        // score for the level
        private int _score;

        public PlayerMind()
        {
            // set args
            _args = new KeyboardHandler();
            // set gravity
            _gravity = 10;
            // set speed
            _speed = 12;
            // set jump
            _jump = 0;
            // set counterforce
            _counterForce = 0;
            // set velocity
            _velocity.X = 1;
            _velocity.Y = -1;
            // set facing direction
            _facingDirectionX = 1;
            // set player mind ID
            _mindID = "Player";
            // set collision flags
            _floorCollide = false;
            _inAir = true;
            _onFloor = false;
            _rightCollide = false;
            _leftCollide = false;
            // set starting score
            _score = 0;
        }

        /// <summary>
        /// Required for Input management of the keyboard
        /// </summary>
        public void OnNewKeyboardInput(object sender, IKeyboardInput args)
        {
            // on keyboard event set the keyboard input to args
            _args = args;
        }

        // TranslateX override for player specific movement
        public override float TranslateX()
        {
            // Player input controlling movement, only active on key down
            foreach (Keys k in _args.GetInputKey())
            {
                // if player presses right arrow or D
                if (k == Keys.Right || k == Keys.D)
                {
                    // check for collision on the right side of the player
                    if (_rightCollide)
                    {
                        // if colliding move the opposite direction
                        return (_speed * _facingDirectionX*-1) * _velocity.X;
                    }
                    // set facing direction to right(1)
                    _facingDirectionX = 1;
                    // move the player
                    return (_speed * _facingDirectionX) * _velocity.X;
                }

                // if player presses left arrow or A
                if (k == Keys.Left || k == Keys.A)
                {
                    // check for collision on the left side of the player
                    if (_leftCollide)
                    {
                        // if colliding move the opposite direction
                        return (_speed * _facingDirectionX * -1) * _velocity.X;
                    }
                    // set facing direction to left(-1)
                    _facingDirectionX = -1;
                    // move the player
                    return (_speed * _facingDirectionX) * _velocity.X;
                }
            }
            return 0;
        }

        // TranslateY override for player specific movement
        public override float TranslateY()
        {
            // if player is not inAir
            if (!_inAir)
            {
                // Player input controlling movement, only active on key down
                foreach (Keys k in _args.GetInputKey())
                {
                    // if player presses up arrow, W, or space
                    if (k == Keys.Up || k == Keys.Space || k == Keys.W)
                    {
                        // set jump value and inAir status to true, and onFloor status to false
                        _jump = 25;
                        _inAir = true;
                        _onFloor = false;
                    }
                }
            }

            // Gravity, always active
            if (!_floorCollide && _inAir)
            {
                // if the character is in the air and not colliding with the floor then gravity is active
                _gravity = 10;
            }
            else
            {
                // the player is colliding with the floor so inAir is false and gravity is set to 0 to emulate the floor holding the entity
                _inAir = false;
                _gravity = 0;
            }

            // if onFloor status is false but the entity is colliding with the floor and not in the air
            if (!_onFloor && _floorCollide && !_inAir)
            {
                // ensure gravity is still 0
                _gravity = 0;
                // set counterforce to push the entity ot of the floor
                _counterForce = 1;
            }

            // if the entity has just been pushed out of the floor and is now not colliding with it
            if (!_onFloor && !_floorCollide && !_inAir)
            {
                // set onFloor status to true and set counter force to push the entity back into collision with the floor
                _onFloor = true;
                _counterForce = -1;
            }

            // if the onFloor Status is true and the entity is colliding with the floor
            if (_onFloor && _floorCollide)
            {
                // set counterforce to 0 to leave the entity colliding with the floor
                _counterForce = 0;
            }

            // if the onFloor status is true and the entity is not colliding with the floorCollide and also isnt being pushed out/into the floor
            if (_onFloor && !_floorCollide && _counterForce == 0)
            {
                // then the entity is in the air
                _inAir = true;
                _onFloor = false;
            }

            
            // if Right or Left collision flag is true
            if (_leftCollide || _rightCollide)
            {
                // set collision flags to false
                _rightCollide = false;
                _leftCollide = false;
            }

            // apply gravity to the entity
            return (_jump - _gravity + _counterForce) * _velocity.Y;
        }

        // Handles new collisions args passed by entity
        public override bool OnNewCollision(ICollisionInput args)
        {
            // Run super
            bool rtnValue = base.OnNewCollision(args);

            // on collision with Floor change floorCollide flag to true
            if (_collidedWith == "Floor" && _collidedThis == "PlayerB")
            {
                _floorCollide = true;
            }

            // on collision with Hostile Bottom collider(HosileB), or Saw collider
            if (_collidedWith == "HostileB" && _collidedThis == "PlayerT" || _collidedWith == "HostileB" && _collidedThis == "PlayerB" || _collidedWith == "Saw" && _collidedThis == "PlayerT" || _collidedWith == "Saw" && _collidedThis == "PlayerB")
            {
                // set return value to true to remove the player from the scene
                rtnValue = true;

                // reset floor collision flag statuses
                _inAir = true;
                _floorCollide = false;
                _onFloor = false;

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

            // on collision between the ceiling and the player top collider
            if (_collidedWith == "Ceiling" && _collidedThis == "PlayerT")
            {
                // set jump value to 0
                _jump = 0;
            }

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

            // on Collision with a CoinGold collider and the player
            if (_collidedWith == "CoinGold" && _collidedThis == "PlayerB" || _collidedWith == "CoinGold" && _collidedThis == "PlayerT")
            {
                // add points to the player score and out put score to the console
                _score += 100;
                Console.WriteLine("Player Score: " + _score);
            }

            // on Collision with a RelicSaw collider and the player
            if (_collidedWith == "RelicSaw" && _collidedThis == "PlayerB" || _collidedWith == "RelicSaw" && _collidedThis == "PlayerT")
            {
                // add points to the player score and out put score to the console
                _score += 500;
                Console.WriteLine("Player Score: " + _score);
                Console.WriteLine("Bone Saw Relic Aquired");
            }

            // Reset Collided with and this to null
            _collidedWith = null;
            _collidedThis = null;
            
            // return rtnValue
            return rtnValue;
        }

        public override void Update()
        {
            // if floor Collide flag is true but there is no collision
            if (_floorCollide && _collidedWith == null)
            {
                // set floorCollide to false
                _floorCollide = false;
            }

            // If player has jumped
            if (_jump > 0)
            {
                // decrement jump
                _jump -= 0.4f;
            } // if jump is less than 0
            else if (_jump < 0)
            {
                // set jump to 0
                _jump = 0;
            }
        }
    }
}