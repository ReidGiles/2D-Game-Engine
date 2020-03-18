using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface ICollisionInput
    {
        String[] GetCollided();
        int[] GetUID();
        Vector2 GetOverlap();
        Vector2 GetCNormal();
    }
}