using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Constructor for RenderManager
        /// </summary>
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="sceneManager"></param>
        public RenderManager(GraphicsDeviceManager graphicsDeviceManager, ISceneManager sceneManager)
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
        }

        /// <summary>
        /// Set the texture of an entity
        /// </summary>
        /// <param name="pUName"></param>
        /// <param name="pTexture"></param>
        public void SetTexture(string pUName, Texture2D pTexture)
        {
            // Retrieve entity by unique name
            IEntity e = _sceneManager.GetEntity(pUName);
            // Set new texture for entity
            e.SetTexture(pTexture);
        }

        /// <summary>
        /// Renders all active entities
        /// </summary>
        public void Update()
        {
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
