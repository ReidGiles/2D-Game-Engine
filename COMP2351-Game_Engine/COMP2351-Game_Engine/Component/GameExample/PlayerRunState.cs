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

        private float _elapsedGameTime;

        /// <summary>
        /// Constructor for PlayerRunState
        /// </summary>
        public PlayerRunState(IAnimator pAnimator, IKeyboardInput pArgs)
        {
            _animator = pAnimator;
            _args = pArgs;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        public void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
            _elapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (_elapsedGameTime > 0.25)
            {
                _animator.SetTexture("Player", Behavior());
                Console.WriteLine("TexChange");
                _elapsedGameTime = 0f;
            }
        }

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