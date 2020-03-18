using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    class PlayerIdleState : IState, IUpdatable
    {
        private IAnimator _animator;
        // args to store the keyboard inputs
        IKeyboardInput _args;
        // Animation frame time
        private float _frameTime;

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
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
        }

        /// <summary>
        /// Updates state
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Behavior();
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
                if (k == Keys.Up || k == Keys.Space || k == Keys.W)
                {
                    return "Jump";
                }
                else if (k == Keys.Left || k == Keys.Right || k == Keys.A|| k == Keys.D)
                {
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
        private void Behavior()
        {
            _animator.Animate("Player", "SmileyWalkAtlas", 4, 4, _frameTime);
        }
    }
}