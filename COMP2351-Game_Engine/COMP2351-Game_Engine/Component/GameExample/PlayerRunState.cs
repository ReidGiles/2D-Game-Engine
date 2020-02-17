﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace COMP2351_Game_Engine
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

        private float _renderTime;
        private float _soundTime;

        private float _frameTime;

        /// <summary>
        /// Constructor for PlayerRunState
        /// </summary>
        public PlayerRunState(IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs)
        {
            // INSTANTIATE _animator
            _animator = pAnimator;
            // INSTANTIATE _audioPlayer
            _audioPlayer = pAudioPlayer;
            // INSTANTIATE _args
            _args = pArgs;

            _frameTime = 0.09f;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            // Calculate elapsed game time for animations
            _renderTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Calculate elapsed game time for audio
            _soundTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_renderTime > _frameTime)
            {
                _animator.SetTexture("Player", Behavior());
                Console.WriteLine("TexChange");
                _renderTime = 0f;
            }

            if (_soundTime > 0.3)
            {
                _audioPlayer.PlaySound("Run");
                _soundTime = 0f;
            }
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

        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        private string Behavior()
        {
            if (_currentTexture == "Player_Run_1")
            {
                _currentTexture = "Player_Run_2";
                return "Player_Run_2";
            }
            else
            {
                _currentTexture = "Player_Run_1";
                return "Player_Run_1";
            }            
        }
    }
}