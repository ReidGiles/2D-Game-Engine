using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3351_Game_Engine
{
    public interface IAnimator
    {
        void Animate(string pEntityName, string pTextureAtlas, int pRows, int pColumns, float pFrameTime);
    }
}
