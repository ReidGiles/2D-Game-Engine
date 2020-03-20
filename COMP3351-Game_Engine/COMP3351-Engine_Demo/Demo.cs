using System;
using COMP3351_Game_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3351_Engine_Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Demo : Engine
    {
        private World _world;
        /// <summary>
        /// Constructor for the demo
        /// </summary>
        public Demo()
        {
            _world = new World(this);
            IsMouseVisible = true;
        }

        /// <summary>
        /// Starting point for the demo, runs after engine declares itself ready
        /// </summary>
        private void Begin()
        {
            // Loads the demo world
            SubscribeListener(_world);
            _world.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update engine
            base.Update(gameTime);
            // Check if engine is ready for use
            if (_ready)
            {
                // Begin demo
                Begin();
                _ready = false;
            }
        }
    }
}