using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class PlayerIdleState : IState, IUpdatable
    {
        private IAnimator _animator;
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // string holding current texture
        private string _currentTexture;

        private IAudioPlayer _audioPlayer;

        /// <summary>
        /// Constructor for PlayerIdleState
        /// </summary>
        /// <param name="pAnimator"></param>
        /// <param name="pAudioPlayer"></param>
        /// <param name="pArgs"></param>
        public PlayerIdleState(IAnimator pAnimator, IAudioPlayer pAudioPlayer, IKeyboardInput pArgs)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            _args = pArgs;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Behavior("Player", "Player_Idle");
        }

        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        public string Trigger()
        {
            foreach (Keys k in _args.GetInputKey())
            {
                if (k == Keys.Up)
                {
                    _currentTexture = null;
                    return "Jump";
                }
                else if (k == Keys.Left || k == Keys.Right)
                {
                    _currentTexture = null;
                    return "Run";
                }
            }
            return null;
        }
        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        private void Behavior(string entity, string texture)
        {
            if (_currentTexture != texture)
            {
                _animator.SetTexture(entity, texture);
            }
            _currentTexture = texture;
        }
    }
}