using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface IAudioPlayer
    {
        void PlaySound(string pSound);
        void PlaySong(string pSong, float pVolume, bool pRepeating);
        void PauseSong();
        void ResumeSong();
        void StopSong();
        void SongVolume(float pVolume);
    }
}