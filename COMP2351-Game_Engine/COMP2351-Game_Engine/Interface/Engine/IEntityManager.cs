using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IEntityManager
    {
        IEntity RequestInstance<T>(String pUName, Texture2D pTexture) where T : IEntity, new();
        IEntity RequestInstance(String pUName);
        void Terminate(int pUID);
        void Terminate(String pUName);
    }
}