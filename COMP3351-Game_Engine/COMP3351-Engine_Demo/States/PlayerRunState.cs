using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using COMP3351_Game_Engine;

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
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        public void Update(GameTime gameTime)
        {

            _gameTime = gameTime;

            _animator.Animate("Player", "SmileyWalkAtlas", 4, 4, _frameTime);

            // Calculate elapsed game time for audio
            _soundTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
    }
}