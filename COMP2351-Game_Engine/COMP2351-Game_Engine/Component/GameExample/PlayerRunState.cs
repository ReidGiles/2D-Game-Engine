using System;
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

        private IAudioPlayer _audioPlayer;

        private float _renderTimer;
        private float _soundTimer;

        /// <summary>
        /// Constructor for PlayerRunState
        /// </summary>
        public PlayerRunState(IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            _args = pArgs;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            _renderTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _soundTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_renderTimer > 0.09)
            {
                _animator.SetTexture("Player", Behavior());
                Console.WriteLine("TexChange");
                _renderTimer = 0f;
            }

            if (_soundTimer > 0.3)
            {
                _audioPlayer.PlaySound("Run");
                _soundTimer = 0f;
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