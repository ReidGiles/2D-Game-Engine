using System;
using COMP3351_Game_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3351_Engine_Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Engine
    {
        public Game1()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_ready)
            {
                Load();
                _ready = false;
            }
        }

        private void Load()
        {
            Spawn<Player>("Player", LoadTexture("Player_Idle"), 500, 5);
        }
    }
}