using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class SoundManager : IAudioPlayer
    {
        // content manager
        private ContentManager _content;

        /// <summary>
        /// Constructor for SoundManager
        /// </summary>
        public SoundManager(ContentManager pContentManager)
        {
            // Set content manager
            _content = pContentManager;
        }

        /// <summary>
        /// Plays sounds based on parameters
        /// </summary>
        public void PlaySound(string pSound)
        {
            SoundEffect sound = _content.Load<SoundEffect>(pSound);
            SoundEffectInstance instance = sound.CreateInstance();
            instance.Play();
        }
    }
}