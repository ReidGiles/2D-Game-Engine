using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface IEngine
    {
        void Run();
        IEntity Spawn<T>(string pName, Texture2D pTexture, float pX, float pY) where T : IEntity, new();
        Texture2D LoadTexture(string fileName);
    }
}
