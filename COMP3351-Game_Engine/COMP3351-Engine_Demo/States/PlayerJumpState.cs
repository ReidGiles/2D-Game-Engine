﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3351_Game_Engine;

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

        /// <summary>
        /// PlayerJumpState constructor
        /// </summary>
        /// <param name="pAnimator"></param>
        /// <param name="pArgs"></param>
        public PlayerJumpState(int pEntityID, IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            _args = pArgs;
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
            _entityUID = pEntityID;
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
        }
    }
}