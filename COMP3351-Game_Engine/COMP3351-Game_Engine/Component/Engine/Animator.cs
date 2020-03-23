using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Various elements of animation throughout this project have been influenced by a tutorial provided by RB Whitaker's tutorials. The code here is adopted from the concepts taught and 
/// required great change to be intoduced to our game engine, but this reference sits here for safety.
/// 
/// URL: http://rbwhitaker.wikidot.com/texture-atlases-1
/// Author: RB Whitaker
/// Date: Unknown
/// </summary>

namespace COMP3351_Game_Engine
{
    class Animator : IAnimator, IUpdatable
    {
        private ISceneManager _sceneManager;
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

        public Animator(ISceneManager sceneManager, ContentManager pContentManager)
        {
            _sceneManager = sceneManager;
            _content = pContentManager;
        }

        /// <summary>
        /// Animates an entity using a custom texture atlas and frame time
        /// </summary>
        /// <param name="pEntityName"></param>
        /// <param name="pTextureAtlas"></param>
        /// <param name="pFrameTime"></param>
        public void Animate(int pUID, string pTextureAtlas, int pRows, int pColumns, float pFrameTime)
        {
            SetupTextureAtlas(pRows, pColumns, pFrameTime);
            // Calculate elapsed game time for animations
            if (_gameTime != null)
                _renderTime += (float)_gameTime.ElapsedGameTime.TotalSeconds;

            // Move to next frame
            _currentFrame++;


            // If current frame is last frame
            if (_currentFrame == _totalFrames)
                // Restart animation from first frame
                _currentFrame = 0;

            /*FOR loop, remove frame time from render time and count how many cycles it takes for frame time to become less than render time
              IF the result is 0, increment one frame, otherwise increment more frames*/

            if (_renderTime > pFrameTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Subtract frame time from render time
                    float a = _renderTime - pFrameTime;
                    // IF frame time < render time, increment frame by i
                    if (a < _renderTime)
                    {
                        // Move to next frame
                        _currentFrame += i;
                        // Update texture
                        SetTexture(pUID, pTextureAtlas);
                        // Reset render time
                        _renderTime = 0f;
                        // Break loop
                        break;
                    }
                }
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
        private void SetTexture(int pUID, string pTextureName)
        {
            // Retrieve entity by unique name
            IEntity e = _sceneManager.GetEntity(pUID);
            // Set new texture for entity
            e.SetTexture(_content.Load<Texture2D>(pTextureName), _rows, _columns, _currentFrame);

        }

        public void Update(GameTime gameTime)
        {
            _gameTime = gameTime;
        }
    }
}
