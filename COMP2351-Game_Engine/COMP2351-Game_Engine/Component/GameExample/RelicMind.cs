using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class RelicMind : Mind
    {
        public RelicMind()
        {
            //initialise the mind ID
            _mindID = "Relic";
        }

        public override bool OnNewCollision(ICollisionInput args)
        {
            bool rtnValue = base.OnNewCollision(args);

            // if the player and Relic collide
            if (_collidedWith == "PlayerB" && _collidedThis == "RelicSaw" || _collidedWith == "PlayerT" && _collidedThis == "RelicSaw")
            {
                // remove the CoinGold from the secene
                rtnValue = true;
            }

            // Reset Collided with and this to null
            _collidedWith = null;
            _collidedThis = null;

            // return rtnValue
            return rtnValue;
        }
    }
}
