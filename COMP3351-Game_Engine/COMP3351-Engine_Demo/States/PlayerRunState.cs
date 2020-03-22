using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using COMP3351_Game_Engine;
using COMP3351_Game_Engine.Interface.Engine;
using static COMP3351_Game_Engine.Component.Engine.Delegates;

namespace COMP3351_Engine_Demo
{
    class PlayerRunState : IState, IUpdatable
    {
        // animator to set textures/animate
        private IAnimator _animator;
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // string holding current texture
        private string _currentTexture = "Player_Run_1";
        // Game time
        private GameTime _gameTime;
        // DECLARE an IAudioPlayer, call it '_audioPlayer'
        private IAudioPlayer _audioPlayer;

        private float _soundTime;

        // Animation frame time
        private float _frameTime;
        private int _entityUID;

        private float _xSpeed;
        private float _ySpeed;
        private float _facingDirectionX;

        private PassFloat _invertTexture;
        private IPhysicsComponent _physicsComponent;

        /// <summary>
        /// Constructor for PlayerRunState
        /// </summary>
        public PlayerRunState(int pEntityID, IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs, PassFloat pInvertTexture, IPhysicsComponent pPhysicsComponent)
        {
            // INSTANTIATE _animator
            _animator = pAnimator;
            // INSTANTIATE _audioPlayer
            _audioPlayer = pAudioPlayer;
            // INSTANTIATE _args
            _args = pArgs;
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
            _entityUID = pEntityID;

            // set _xSpeed
            _xSpeed = 7;
            // set _ySpeed
            _ySpeed = 35;
            // set facing direction
            _facingDirectionX = 1;

            _invertTexture = pInvertTexture;
            _physicsComponent = pPhysicsComponent;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        public void Update(GameTime gameTime)
        {

            _gameTime = gameTime;

            _animator.Animate(_entityUID, "SmileyWalkAtlas", 4, 4, _frameTime);

            // Calculate elapsed game time for audio
            _soundTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_soundTime > 0.3)
            {
                _audioPlayer.PlaySound("Run");
                _soundTime = 0f;
            }


            //Declare a vector to store the force needed to move
            Vector2 force = new Vector2(0, 0);

            // Player input controlling movement, only active on key down
            foreach (Keys k in _args.GetInputKey())
            {

                // if player presses right arrow or D
                if (k == Keys.Right || k == Keys.D)
                {
                    // set facing direction to right(1)
                    _facingDirectionX = 1;
                    force.X = _xSpeed * _facingDirectionX;
                }

                // if player presses left arrow or A
                if (k == Keys.Left || k == Keys.A)
                {
                    // set facing direction to left(-1)
                    _facingDirectionX = -1;
                    force.X = _xSpeed * _facingDirectionX;
                }
            }
            // update facing direction in entity to update texture orientation
            _invertTexture(_facingDirectionX);
            // apply force to the physics component to move entity
            _physicsComponent.ApplyForce(force);
        }

        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        public string Trigger()
        {
            if (!(_args.GetInputKey().Contains(Keys.Left)) && !(_args.GetInputKey().Contains(Keys.Right)))
            {
                _currentTexture = null;
                return "Idle";
            }
            foreach (Keys k in _args.GetInputKey())
            {
                if (k == Keys.Up)
                {
                    _currentTexture = null;
                    return "Jump";
                }
            }
            return null;
        }
    }
}