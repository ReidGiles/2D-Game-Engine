using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IAIComponentManager
    {
        IMind RequestMind<T>() where T : IMind, new();
        void RemoveMind(IMind pMind);
    }
}
