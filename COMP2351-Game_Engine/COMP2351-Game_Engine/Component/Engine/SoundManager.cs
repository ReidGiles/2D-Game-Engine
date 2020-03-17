using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

        public void PlaySong(string pSong, float pVolume, bool pRepeating)
        {
            Song song = _content.Load<Song>(pSong);
            MediaPlayer.Volume = pVolume;
            MediaPlayer.Play(song);
            if (pRepeating)
                MediaPlayer.IsRepeating = true;
            else MediaPlayer.IsRepeating = false;
        }

        public void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public void ResumeSong()
        {
            MediaPlayer.Resume();
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        public void SongVolume(float pVolume)
        {
            MediaPlayer.Volume = pVolume;
        }
    }
}