using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine.Component.Engine
{
    public class Delegates
    {
        public delegate void PassVector2(Vector2 dPosition);

        public delegate void PassFloat(float pFacingDirection);

        public delegate Vector2 GetVector2();
    }
}
