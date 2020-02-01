using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP2351_Game_Engine
{
    class PlayerJumpState : IState, IUpdatable
    {
        private IAnimator _animator;
        private GameTime _gameTime;
        public PlayerJumpState(IAnimator pAnimator)
        {
            _animator = pAnimator;
        }
        public void Update()
        {

        }
    }
}