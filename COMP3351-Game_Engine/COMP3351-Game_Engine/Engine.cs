using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace COMP3351_Game_Engine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        // reference to the Graphics Device Manager
        GraphicsDeviceManager graphics;
        // reference to the SpriteBatch
        SpriteBatch spriteBatch;
        // int containing the display window ScreenWidth
        public static int ScreenWidth;
        // int containing the display window ScreenHeight
        public static int ScreenHeight;
        // reference to the entity Manager
        private IEntityManager entityManager;
        // reference to the scene Manager
        private ISceneManager sceneManager;
        // reference to the collision Manager
        private ICollisionManager collisionManager;
        // reference to the AI component Manager
        private IAIComponentManager aiComponentManager;
        // reference to the input Manager
        private IInputManager inputManager;
        // Reference to the render manager
        private IUpdatable _renderManager;
        // Reference to the audio manager
        private IAudioPlayer _audioManager;
        // reference to the sceneGraph
        private ISceneGraph sceneGraph;
        // List of Textures
        private Texture2D[] textures;
        // reference to the headerLoaction
        private Vector2 backgroundLocation;
        private Vector2 backgroundLocation2;
        private Vector2 backgroundLocation3;
        private Vector2 backgroundLocation4;
        // camera position to follow the player
        private Vector3 cameraPos;

        public bool _ready;

        public Engine()
        {
            // set graphics
            graphics = new GraphicsDeviceManager(this);
            // set Content directory
            Content.RootDirectory = "Content";
            // set graphics height
            graphics.PreferredBackBufferHeight = 900;
            // set graphics width
            graphics.PreferredBackBufferWidth = 1600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // set ScreenHeight
            ScreenHeight = GraphicsDevice.Viewport.Height;
            // set ScreenWidth
            ScreenWidth = GraphicsDevice.Viewport.Width;
            // set cameraPos
            cameraPos = new Vector3(ScreenWidth / 2, 0, 0);
            // initialise a new sceneGraph
            sceneGraph = new SceneGraph();
            // initialise a new sceneManager
            sceneManager = new SceneManager(sceneGraph);
            // initialise a new collisionManager
            collisionManager = new CollisionManager(sceneManager);
            // initialise a new inputManager
            inputManager = new InputManager();
            // Initialise render manager
            _renderManager = new RenderManager(graphics, sceneManager, Content);
            // Initialise audio manager
            _audioManager = new SoundManager(Content);
            // initialise a new aiComponontManager
            aiComponentManager = new AIComponentManager(inputManager, (IAnimator)_renderManager, _audioManager, sceneManager);
            // initialise a new entityManager
            entityManager = new EntityManager(collisionManager, sceneGraph, aiComponentManager);           
            // set headerLoaction
            backgroundLocation = new Vector2(-ScreenWidth / 2, 0);
            backgroundLocation2 = new Vector2(backgroundLocation.X + ScreenWidth + 1, 0);
            backgroundLocation3 = new Vector2(-ScreenWidth / 2, 0);
            backgroundLocation4 = new Vector2(-ScreenWidth / 2, 0);
            // initialise
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _ready = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // if ESC key is pressed end the simulation
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update all the managers
            ((IUpdatable)collisionManager).Update(gameTime);
            ((IUpdatable)sceneManager).Update(gameTime);
            ((IUpdatable)aiComponentManager).Update(gameTime);
            ((IUpdatable)inputManager).Update(gameTime);
            ((IUpdatable)entityManager).Update(gameTime);

            // update teh engineDemo
            //gameDemo.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Updates render manager
            _renderManager.Update(gameTime);

            base.Draw(gameTime);
        }

        public IEntity Spawn<T>(string pName, Texture2D pTexture, float pX, float pY) where T : IEntity, new()
        {
            IEntity entity = entityManager.RequestInstance<T>(pName, pTexture);
            sceneManager.Spawn(entity, pX, pY);
            return entity;
        }

        public Texture2D LoadTexture(string pFileName)
        {
            return Content.Load<Texture2D>(pFileName);
        }

        public void PlaySound(string pFileName)
        {
        }

        public void PlaySong(string pFileName, float pVolume, bool pRepeating)
        {
            _audioManager.PlaySong(pFileName, pVolume, pRepeating);
        }

        public void PlaySong(string pFileName, float pVolume)
        {
            _audioManager.PlaySong(pFileName, pVolume, false);
        }

        public void PlaySong(string pFileName, bool pRepeating)
        {
            _audioManager.PlaySong(pFileName, 1.00f, pRepeating);
        }

        public void PlaySong(string pFileName)
        {
            _audioManager.PlaySong(pFileName, 1.00f, false);
        }

        public void SubscribeListener(IKeyboardListener pListener)
        {
            inputManager.AddListener(pListener.OnNewKeyboardInput);
        }
        public void SubscribeListener(IMouseListener pListener)
        {
            inputManager.AddListener(pListener.OnNewMouseInput);
        }
    }
}