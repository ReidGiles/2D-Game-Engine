using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    class RenderManager : IUpdatable, IAnimator
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private ISceneManager _sceneManager;
        // int containing the display window ScreenWidth
        public static int ScreenWidth;
        // int containing the display window ScreenHeight
        public static int ScreenHeight;
        // camera position to follow the player
        private Vector3 cameraPos;
        // content manager
        private ContentManager _content;
        // Game time
        private GameTime _gameTime;
        // Render time for animation
        private float _renderTime;
        // Rows inside _textureAtlas
        private int _rows;
        // Columns inside _textureAtlas
        private int _columns;
        // Current animation frame
        private int _currentFrame;
        // Total number of animation frames
        private int _totalFrames;
        // Animation frame time
        private float _frameTime;

        private float _fixedTimeStep;

        /// <summary>
        /// Constructor for RenderManager
        /// </summary>
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="sceneManager"></param>
        public RenderManager(GraphicsDeviceManager graphicsDeviceManager, ISceneManager sceneManager, ContentManager pContentManager)
        {
            _graphicsDeviceManager = graphicsDeviceManager;
            _spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);
            _sceneManager = sceneManager;
            // set ScreenHeight
            ScreenHeight = _graphicsDeviceManager.GraphicsDevice.Viewport.Height;
            // set ScreenWidth
            ScreenWidth = _graphicsDeviceManager.GraphicsDevice.Viewport.Width;
            // set cameraPos
            cameraPos = new Vector3(ScreenWidth / 2, 0, 0);
            // Set content manager
            _content = pContentManager;

            _fixedTimeStep = 0166667;
        }

        /// <summary>
        /// Animates an entity using a custom texture atlas and frame time
        /// </summary>
        /// <param name="pEntityName"></param>
        /// <param name="pTextureAtlas"></param>
        /// <param name="pFrameTime"></param>
        public void Animate(string pEntityName, string pTextureAtlas, int pRows, int pColumns, float pFrameTime)
        {
            SetupTextureAtlas(pRows, pColumns, pFrameTime);
            // Calculate elapsed game time for animations
            if (_gameTime != null)
            {
                _renderTime += (float)_gameTime.ElapsedGameTime.TotalSeconds;
                Console.WriteLine(_renderTime);
            }

            // Move to next frame
            _currentFrame++;


            // If current frame is last frame
            if (_currentFrame == _totalFrames)
            {
                // Restart animation from first frame
                _currentFrame = 0;
            }
            // FOR loop, remove frame time from render time and count how many cycles it takes for frame time to become less than render time
            // IF the result is 0, increment one frame, otherwise increment more frames
            // remember to subtract 1 from for loop variable
            if (_renderTime > pFrameTime)
            {
                SetTexture(pEntityName, pTextureAtlas);
                _renderTime = 0f;
            }
        }

        /// <summary>
        /// Sets variables relevant to texture atlas animation
        /// </summary>
        /// <param name="pRows"></param>
        /// <param name="pColumns"></param>
        /// <param name="pFrameTime"></param>
        private void SetupTextureAtlas(int pRows, int pColumns, float pFrameTime)
        {
            // SET _rows
            _rows = pRows;
            // SET _columns
            _columns = pColumns;
            // SET _totalFrames
            _totalFrames = pRows * pColumns;
            // SET _frameTime
            _frameTime = pFrameTime;
        }

        /// <summary>
        /// Set the texture of an entity
        /// </summary>
        /// <param name="pUName"></param>
        /// <param name="pTexture"></param>
        public void SetTexture(string pUName, string pTextureName)
        {
            // Retrieve entity by unique name
            IEntity e = _sceneManager.GetEntity(pUName);         
            // Set new texture for entity
            e.SetTexture(_content.Load<Texture2D>(pTextureName), _rows, _columns, _currentFrame);
        }

        /// <summary>
        /// Renders all active entities
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // SET _gameTime for use by Animate()
            _gameTime = gameTime;

            // Set graphics background colour
            _graphicsDeviceManager.GraphicsDevice.Clear(Color.Purple);

            // Set cameraPos to follow the player on the x axis if there is one
            if (_sceneManager.GetEntity("Player") != null)
            {
                IEntity e = _sceneManager.GetEntity("Player");
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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Matrix.CreateTranslation(cameraPos));
            // syntax for the XNA 2d camera to follow the player is from https://www.reddit.com/r/monogame/comments/6lxd69/how_do_i_make_camera_follow_the_player/

            // Draw all entities from list
            foreach (IEntity e in _sceneManager.GetEntity())
            {
                e.Draw(_spriteBatch);
            }
            // end Spritebatch
            _spriteBatch.End();
        }
    }
}
