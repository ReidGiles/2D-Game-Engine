using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface ICollider
    {
        //Translate the position of the Collider
        void Translate(float dx, float dy);

        void Translate(Vector2 dPos);
    }
}
