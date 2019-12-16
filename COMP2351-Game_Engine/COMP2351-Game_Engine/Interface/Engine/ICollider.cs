using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    interface ICollider
    {
        //Translate the position of the Collider
        void Translate(float dx, float dy);
    }
}
