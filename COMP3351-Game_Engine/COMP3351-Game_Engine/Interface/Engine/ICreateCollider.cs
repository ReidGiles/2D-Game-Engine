using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface ICreateCollider
    {
        //Create the corners of the collider
        float[] CreateCollider();
        
        //Get the collider Tag
        String GetTag();
    }
}
