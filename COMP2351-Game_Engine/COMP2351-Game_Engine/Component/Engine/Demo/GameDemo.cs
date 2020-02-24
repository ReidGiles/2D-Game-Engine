using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    class GameDemo : IKeyboardListener, IMouseListener
    {
        // reference to the entity manager
        private IEntityManager entityManager;
        // refernce to the scene manager
        private ISceneManager sceneManager;
        // reference to the collision manager
        private ICollisionManager collisionManager;
        // reference to the AI component manager
        private IAIComponentManager aiComponentManager;
        // reference to the input manager
        private IInputManager inputManager;
        // reference to teh scene graph
        private ISceneGraph sceneGraph;
        // reference to the textures used
        private Texture2D[] textures;
        // Vector2 list for spawning middle platforms
        private List<Vector2> _platformSpawn;
        // Vector2 list for spawning Single platforms
        private List<Vector2> _platformSSpawn;
        // Vector2 list for spawning end platforms left side
        private List<Vector2> _platformEndLSpawn;
        // Vector2 list for spawning end platforms right side
        private List<Vector2> _platformEndRSpawn;
        // Vector2 list for spawning walls
        private List<Vector2> _wallSpawn;
        // Vector2 list for spawning hostiles
        private List<Vector2> _hostileSpawn;
        // Vector2 list for spawning saws
        private List<Vector2> _sawSpawn;
        // Vector2 list for spawning gold
        private List<Vector2> _goldSpawn;

        // constructor for the class
        public GameDemo()
        {
        }
        
        // method to initialise the class
        public void Initialise(IEntityManager pEntityManager, ISceneManager pSceneManager, ICollisionManager pCollisionManager, IAIComponentManager pAiComponentManager, IInputManager pInputManager, ISceneGraph pSceneGraph)
        {
            sceneGraph = pSceneGraph;
            collisionManager = pCollisionManager;
            entityManager = pEntityManager;
            sceneManager = pSceneManager;
            inputManager = pInputManager;
            aiComponentManager = pAiComponentManager;
            _platformSpawn = new List<Vector2>();
            _platformSSpawn = new List<Vector2>();
            _platformEndLSpawn = new List<Vector2>();
            _platformEndRSpawn = new List<Vector2>();
            _wallSpawn = new List<Vector2>();
            _hostileSpawn = new List<Vector2>();
            _sawSpawn = new List<Vector2>();
            _goldSpawn = new List<Vector2>();
            // Populate spawn arrays
            LoadLevel();
        }

        // method to set the value of textures
        public void LoadTextures(Texture2D[] pTextures)
        {
            textures = pTextures;
        }

        // method to load the content for the gameDemo
        public void LoadContent()
        {
            // // Request floor entity from entity manager
            for (int i = 0; i < 8; i++)
            {
                IEntity entity = entityManager.RequestInstance<Floor>("Floor", textures[7]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, i*1000 - 800, 900 - textures[7].Height);
            }

            foreach (Vector2 v in _platformSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<Platform>("Platform", textures[3]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _platformSSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<PlatformSingle>("Platform", textures[3]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _platformEndLSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<PlatformEndL>("Platform", textures[3]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _platformEndRSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<PlatformEndR>("Platform", textures[3]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _wallSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<Platform>("Platform", textures[8]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _hostileSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<Hostile>("Hostile", textures[2]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _sawSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<Saw>("Saw", textures[4]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            foreach (Vector2 v in _goldSpawn)
            {
                // Request Floor entity from entity manager
                IEntity entity = entityManager.RequestInstance<Gold>("Gold", textures[5]);
                // Scene manager places entity on the scene
                sceneManager.Spawn(entity, v.X, v.Y);
            }

            IEntity relic = entityManager.RequestInstance<Relic>("Relic", textures[6]);
            sceneManager.Spawn(relic, 3050, -850);

            // Request player entity from entity manager
            IEntity player = entityManager.RequestInstance<Player>("Player", textures[1]);
            // Scene manager places entity on the scene
            sceneManager.Spawn(player, 0, 300);

        }

        private void LoadLevel()
        {
            // Distance between platforms
            int platformXIncrement = 200;
            int platformYIncrement = 0;

            // Populate the level with the Start point for Platforms
            Populate(650, -200, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            Populate(700, -600, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            Populate(900, -850, 1, platformXIncrement, platformYIncrement, _platformEndLSpawn);
            Populate(1100, -850, 7, platformXIncrement, platformYIncrement, _platformSpawn);
            Populate(2500, -850, 1, platformXIncrement, platformYIncrement, _platformEndRSpawn);
            Populate(550, -350, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            Populate(950, 50, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            Populate(1150, 300, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            Populate(1350, 550, 1, platformXIncrement, platformYIncrement, _platformSSpawn);
            

            // Distance between platforms
            platformXIncrement = 135;
            Populate(965, -350, 1, platformXIncrement, platformYIncrement, _platformEndLSpawn);
            Populate(1100, -350, 3, platformXIncrement, platformYIncrement, _platformSpawn);
            Populate(1505, -350, 1, platformXIncrement, platformYIncrement, _platformEndRSpawn);

            // Distance between platforms
            platformXIncrement = 100;
            platformYIncrement = -100;

            Populate(2900, 650, 9, platformXIncrement, platformYIncrement, _platformSSpawn);

            // Distance between platforms
            platformXIncrement = 200;
            platformYIncrement = 150;

            Populate(3000, -800, 4, platformXIncrement, platformYIncrement, _platformSSpawn);

            // Distance between platforms
            platformXIncrement = 200;
            platformYIncrement = 250;

            Populate(1700, -150, 3, platformXIncrement, platformYIncrement, _platformSSpawn);

            // Distance between platforms
            platformXIncrement = 0;
            platformYIncrement = -250;
            // Populate the level with single platforms
            Populate(2700, 450, 8, platformXIncrement, platformYIncrement, _wallSpawn);
            Populate(4000, 550, 8, platformXIncrement, platformYIncrement, _wallSpawn);

            // Distance between Hostiles
            platformXIncrement = 250;
            platformYIncrement = 0;
            // Populate the level with hostiles
            Populate(1100, -400, 2, platformXIncrement, platformYIncrement, _hostileSpawn);

            // Distance between Hostiles
            platformXIncrement = 100;
            platformYIncrement = -100;
            // Populate the level with hostiles
            Populate(2950, 600, 9, platformXIncrement, platformYIncrement, _hostileSpawn);

            // Distance between Hostiles
            /*platformXIncrement = 200;
            platformYIncrement = -300;
            // Populate the level with hostiles
            Populate(3650, -400, 9, platformXIncrement, platformYIncrement, _hostileSpawn);*/

            // Distance between saws
            platformXIncrement = 200;
            platformYIncrement = 0;
            // Populate the level with saws
            Populate(1050, -850, 8, platformXIncrement, platformYIncrement, _sawSpawn);

            // Distance between saws
            platformXIncrement = 300;
            platformYIncrement = 0;
            // Populate the level with saws
            Populate(250, 750, 4, platformXIncrement, platformYIncrement, _sawSpawn);

            // Distance between saws
            platformXIncrement = 200;
            platformYIncrement = 250;
            // Populate the level with saws
            Populate(1670, -200, 3, platformXIncrement, platformYIncrement, _sawSpawn);
            Populate(1830, -200, 3, platformXIncrement, platformYIncrement, _sawSpawn);

            platformXIncrement = 0;
            platformYIncrement = -50;
            Populate(1600, 750, 22, platformXIncrement, platformYIncrement, _sawSpawn);

            // Distance between gold
            platformXIncrement = 300;
            platformYIncrement = 0;
            // Populate the level with gold
            Populate(265, 550, 4, platformXIncrement, platformYIncrement, _goldSpawn);

            // Distance between gold
            platformXIncrement = 0;
            platformYIncrement = 0;
            // Populate the level with gold
            Populate(1550, 250, 1, platformXIncrement, platformYIncrement, _goldSpawn);
            Populate(550, -400, 1, platformXIncrement, platformYIncrement, _goldSpawn);

            // Distance between gold
            platformXIncrement = 300;
            platformYIncrement = 0;
            // Populate the level with gold
            Populate(1150, -600, 2, platformXIncrement, platformYIncrement, _goldSpawn);

            // Distance between gold
            platformXIncrement = 200;
            platformYIncrement = 250;
            // Populate the level with gold
            Populate(1760, -200, 3, platformXIncrement, platformYIncrement, _goldSpawn);

            // Distance between gold
            platformXIncrement = 0;
            platformYIncrement = -50;
            // Populate the level with gold
            Populate(2550, -900, 3, platformXIncrement, platformYIncrement, _goldSpawn);
            Populate(2600, -900, 3, platformXIncrement, platformYIncrement, _goldSpawn);
        }

        // Populate an Vector2 array with custom data
        private void Populate(int pXpos, int pYpos, int pLimit, int pIncrement, int yIncrement, List<Vector2> pList)
        {
            int XPosition = pXpos;
            int YPosition = pYpos;
            // Add data to Vector2 array using paramaters
            for (int i = 0; i < pLimit; i++)
            {
                pList.Add(new Vector2(XPosition + i * pIncrement, YPosition + i * yIncrement));
            }
        }

        // keyboard event to listen for keyboard inputs
        public void OnNewKeyboardInput(object sender, IKeyboardInput args)
        {
            
        }

        // Mouse event to listen for Mouse inputs
        public void OnNewMouseInput(object sender, IMouseInput args)
        {
            
        }

        // update the class 
        public void Update()
        {
            // Respawn player on death
            if (sceneManager.GetEntity("Player") == null)
            {
                sceneManager.Spawn(entityManager.RequestInstance("Player"), 0, 300);
            }
        }
    }
}