using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP3351_Game_Engine
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
        public void PlaySound(string pSound, float pVolume, bool pRepeating)
        {
            // Declare and set sound
            SoundEffect sound = _content.Load<SoundEffect>(pSound);
            // Create new sound instance
            SoundEffectInstance instance = sound.CreateInstance();
            // Set instance volume
            instance.Volume = pVolume;
            // Set instance isLooped
            instance.IsLooped = pRepeating;
            // Play instance
            instance.Play();
        }

        /// <summary>
        /// Plays sounds based on parameters
        /// </summary>
        public void PlaySound(string pSound, float pVolume)
        {
            SoundEffect sound = _content.Load<SoundEffect>(pSound);
            SoundEffectInstance instance = sound.CreateInstance();
            instance.Volume = pVolume;
            instance.Play();
        }

        /// <summary>
        /// Plays sounds based on parameters
        /// </summary>
        public void PlaySound(string pSound, bool pRepeating)
        {
            SoundEffect sound = _content.Load<SoundEffect>(pSound);
            SoundEffectInstance instance = sound.CreateInstance();
            instance.IsLooped = pRepeating;
            instance.Play();
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

        /// <summary>
        /// Plays a song using a file name, a volume and a repeating switch
        /// </summary>
        /// <param name="pSong"></param>
        /// <param name="pVolume"></param>
        /// <param name="pRepeating"></param>
        public void PlaySong(string pSong, float pVolume, bool pRepeating)
        {
            // Declare and set song
            Song song = _content.Load<Song>(pSong);
            // Set volume
            MediaPlayer.Volume = pVolume;
            // Play song
            MediaPlayer.Play(song);
            // Set isRepeating
            if (pRepeating)
                MediaPlayer.IsRepeating = true;
            else MediaPlayer.IsRepeating = false;
        }

        /// <summary>
        /// Pauses a running song
        /// </summary>
        public void PauseSong()
        {
            MediaPlayer.Pause();
        }

        /// <summary>
        /// Resumes currently paused song
        /// </summary>
        public void ResumeSong()
        {
            MediaPlayer.Resume();
        }

        /// <summary>
        /// Stops song
        /// </summary>
        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Changes song volume
        /// </summary>
        /// <param name="pVolume"></param>
        public void SongVolume(float pVolume)
        {
            MediaPlayer.Volume = pVolume;
        }
    }
}