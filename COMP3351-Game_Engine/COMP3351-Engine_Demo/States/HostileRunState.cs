using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3351_Game_Engine;
using Microsoft.Xna.Framework;

namespace COMP3351_Engine_Demo
{
    class HostileRunState : IState, IUpdatable
    {
        private IAnimator _animator;

        // Animation frame time
        private float _frameTime;

        private IAudioPlayer _audioPlayer;

        public HostileRunState(IAnimator pAnimator, IAudioPlayer pAudioPlayer)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
        }

        public string Trigger()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            Behavior();
        }

        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        private void Behavior()
        {
            //_animator.Animate("Player", "SmileyWalkAtlas", 4, 4, _frameTime);
        }
    }
}