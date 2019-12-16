using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace COMP2351_Game_Engine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
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
        // reference to the sceneGraph
        private ISceneGraph sceneGraph;
        // List of Textures
        private Texture2D[] textures;
        // reference to the engineDemo class
        private GameDemo gameDemo;
        // reference to the headerLoaction
        private Vector2 headerLocation;
        // camera position to follow the player
        private Vector3 cameraPos;

        public Game1()
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
            // initialise a new aiComponontManager
            aiComponentManager = new AIComponentManager(inputManager);
            // initialise a new entityManager
            entityManager = new EntityManager(collisionManager, sceneGraph, aiComponentManager);
            // initialise a new engineDemo
            gameDemo = new GameDemo();
            // run engineDemo initialise method
            gameDemo.Initialise(entityManager, sceneManager, collisionManager, aiComponentManager, inputManager, sceneGraph);
            // add input listeners to the engineDemo
            inputManager.AddListener(((IKeyboardListener)gameDemo).OnNewKeyboardInput);
            inputManager.AddListener(((IMouseListener)gameDemo).OnNewMouseInput);
            // set headerLoaction
            headerLocation = new Vector2(-ScreenWidth / 2, 0);
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

            // set textures
            textures = new Texture2D[] {Content.Load<Texture2D>("Header"), Content.Load<Texture2D>("Player"), Content.Load<Texture2D>("Hostile"), Content.Load<Texture2D>("Platform"), Content.Load<Texture2D>("Saw"), Content.Load<Texture2D>("CoinGold"), Content.Load<Texture2D>("Relic"), Content.Load<Texture2D>("Floor"), Content.Load<Texture2D>("Wall") };

            // load textures into engineDemo
            gameDemo.LoadTextures(textures);
            // load gameDemo Content
            gameDemo.LoadContent();
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
            ((IUpdatable)collisionManager).Update();
            ((IUpdatable)sceneManager).Update();
            ((IUpdatable)aiComponentManager).Update();
            ((IUpdatable)inputManager).Update();
            ((IUpdatable)entityManager).Update();

            // update teh engineDemo
            gameDemo.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Set graphics background colour
            GraphicsDevice.Clear(Color.Purple);

            // Set cameraPos to follow the player on the x axis if there is one
            if (sceneManager.GetEntity("Player") != null)
            {
                IEntity e = sceneManager.GetEntity("Player");
                cameraPos.X = e.GetLocation().X * -1 + ScreenWidth / 2;
                // if the player is higher than half the ScreenWidth then follow them on the y axis as well
                if (e.GetLocation().Y < ScreenHeight / 2)
                {
                    cameraPos.Y = e.GetLocation().Y * -1 + ScreenHeight / 2;
                }
                else
                {
                    cameraPos.Y = 0;
                }
            }


            // Begin SpriteBatch
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Matrix.CreateTranslation(cameraPos));
            // syntax for the XNA 2d camera to follow the player is from https://www.reddit.com/r/monogame/comments/6lxd69/how_do_i_make_camera_follow_the_player/

            // Draw all entities from list
            foreach (IEntity e in sceneManager.GetEntity())
            {
                e.Draw(spriteBatch);
            }
            // Draw the header
            //spriteBatch.Draw(textures[0], headerLocation, Color.AntiqueWhite);
            // end Spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}