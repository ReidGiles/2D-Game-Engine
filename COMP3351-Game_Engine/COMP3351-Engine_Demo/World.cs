using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Engine_Demo
{
    class World
    {
        private Demo _demo;
        public World(Demo demo)
        {
            _demo = demo;
        }

        /// <summary>
        /// Loads the world
        /// </summary>
        public void Load()
        {
            // Load background music
            _demo.PlaySong("Background_Music", 1.0f, true);

            // Load floors
            for (int i = 0; i < 2; i++)
            {
                _demo.Spawn<Floor>("Floor", _demo.LoadTexture("Floor"), i * 1000 - 800, 900 - _demo.LoadTexture("Floor").Height);
            }

            // Load saws
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 100, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 400, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);
            _demo.Spawn<Saw>("Saw", _demo.LoadTexture("Saw"), 700, (900 - _demo.LoadTexture("Saw").Height) - _demo.LoadTexture("Floor").Height);

            // Load Player
            _demo.Spawn<Player>("Player", _demo.LoadTexture("Player_Idle"), 0, 500);

            // Load Hostile
            _demo.Spawn<Hostile>("Hostile", _demo.LoadTexture("Hostile"), 100, 300);
        }
    }
}
