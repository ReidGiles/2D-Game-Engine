﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IAnimator
    {
        void SetTexture(string pUName, string pTexture);
        void Animate(string pEntityName, string pTextureAtlas, int pRows, int pColumns, float pFrameTime);
    }
}
