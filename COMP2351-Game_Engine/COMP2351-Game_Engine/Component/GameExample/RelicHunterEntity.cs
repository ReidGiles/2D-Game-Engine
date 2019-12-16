using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    abstract class RelicHunterEntity: Entity, IRelicHunterEntity
    {
        /// <summary>
        /// Basic collision behaviour using rectangle.
        /// Needs updating.
        /// </summary>
        public RelicHunterEntity()
        {
        }
    }
}