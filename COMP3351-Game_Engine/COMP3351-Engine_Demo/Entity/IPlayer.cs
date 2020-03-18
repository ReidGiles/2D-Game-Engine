using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3351_Engine_Demo
{
    interface IPlayer
    {
        void SetTexture(Texture2D pTexture);
        void SetLocation(float pX, float pY);
        void Draw(SpriteBatch spriteBatch);
    }
}
