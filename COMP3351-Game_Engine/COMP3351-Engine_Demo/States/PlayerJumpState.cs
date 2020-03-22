using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3351_Game_Engine;
using COMP3351_Game_Engine.Interface.Engine;
using System.Timers;

namespace COMP3351_Engine_Demo
{
    class PlayerJumpState : IState, IUpdatable
    {
        // animator to set textures/animate
        private IAnimator _animator;
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // Animation frame time
        private float _frameTime;
        private int _entityUID;

        private IAudioPlayer _audioPlayer;

        private float _ySpeed;

        private IPhysicsComponent _physicsComponent;

        private Timer _timer;

        private bool _ignoreInput;

        /// <summary>
        /// PlayerJumpState constructor
        /// </summary>
        /// <param name="pAnimator"></param>
        /// <param name="pArgs"></param>
        public PlayerJumpState(int pEntityID, IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs, IPhysicsComponent pPhysicsComponent)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            _args = pArgs;
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
            _entityUID = pEntityID;

            _ySpeed = 3;

            _physicsComponent = pPhysicsComponent;

            _timer = new Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _ignoreInput = false;
        }

        /// <summary>
        /// Updates the state
        /// </summary>
        public void Update(GameTime gameTime)
        {
            Behavior();
        }
        public string Trigger()
        {
            if ( !( _args.GetInputKey().Contains(Keys.Up) ) )
            {
                return "Idle";
            }
            return null;
        }
        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        private void Behavior()
        {
            _animator.Animate(_entityUID, "SmileyWalkAtlas", 4, 4, _frameTime);
            //_audioPlayer.PlaySound("Jump");

            //Declare a vector to store the force needed to move
            Vector2 force = new Vector2(0, 0);

            // Player input controlling movement, only active on key down
            foreach (Keys k in _args.GetInputKey())
            {
                // if player presses up arrow, W, or space
                if (k == Keys.Up || k == Keys.Space || k == Keys.W)
                {
                    // set jump value and inAir status to true
                    force.Y = -_ySpeed;
                }
            }
            // apply force to the physics component to move entity
            _physicsComponent.ApplyForce(force);
        }
    }
}