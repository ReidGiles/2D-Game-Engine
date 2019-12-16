using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IPlayer
    {
        void SetTexture(Texture2D pTexture);
        void SetLocation(float pX, float pY);
        void Draw(SpriteBatch spriteBatch);
    }
}
