using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
    class World : IKeyboardListener
    {
        private Demo _demo;
        private Timer _timer;
        private bool _ignoreInput;
        public World(Demo demo)
        {
            _demo = demo;
            _timer = new Timer(3000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _ignoreInput = false;
        }

        /// <summary>
        /// Loads the world
        /// </summary>
        public void Load()
        {
            // Load floors
            for (int i = 0; i < 2; i++)
            {
                _demo.Spawn<Floor>("Floor", _demo.LoadTexture("Floor"), i * 1000 - 800, 900 - _demo.LoadTexture("Floor").Height);
            }

            // Load saws
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 100, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 400, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 700, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);

            _demo.Spawn<Platform>("Platform", _demo.LoadTexture("Platform"), 200, 700);
        }

        public void OnNewKeyboardInput(object sender, IKeyboardInput args)
        {
            if (!_ignoreInput)
            {
                if (args.GetInputKey().Contains(Keys.P))
                {
                    _ignoreInput = true;

                    // Spawn Player
                    _demo.Spawn<Player>("Player", _demo.LoadTexture("Smiley"), 0, 500);
                }
                else if (args.GetInputKey().Contains(Keys.H))
                {
                    _ignoreInput = true;

                    // Spawn Hostile
                    _demo.Spawn<Hostile>("Hostile", _demo.LoadTexture("Hostile"), 100, 300);
                }
                else if (args.GetInputKey().Contains(Keys.M))
                {
                    _ignoreInput = true;

                    // Load background music
                    _demo.PlaySong("Background_Music", 1.0f, true);
                }
            }
        }

    }
}