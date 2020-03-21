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
        private int _entityUID;

        private IAudioPlayer _audioPlayer;

        public HostileRunState(int pEntityID, IAnimator pAnimator, IAudioPlayer pAudioPlayer)
        {
            _animator = pAnimator;
            _audioPlayer = pAudioPlayer;
            // INSTANTIATE _frameTime
            _frameTime = 0.009f;
            _entityUID = pEntityID;
        }

        public string Trigger()
        {
            return null;
        }

        public void Update(GameTime gameTime)
        {
            ((IUpdatable)_animator).Update(gameTime);
            Behavior();
        }

        /// <summary>
        /// State behaviour logic
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="texture"></param>
        private void Behavior()
        {
            _animator.Animate(_entityUID, "Hostile", 1, 1, 0f);
        }
    }
}