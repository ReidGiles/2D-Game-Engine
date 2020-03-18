using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3351_Game_Engine
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
