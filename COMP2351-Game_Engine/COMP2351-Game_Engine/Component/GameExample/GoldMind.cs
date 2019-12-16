using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class GoldMind : Mind
    {
        public GoldMind()
        {
            //initialise the mind ID
            _mindID = "Gold";
        }

        public override bool OnNewCollision(ICollisionInput args)
        {
            bool rtnValue = base.OnNewCollision(args);

            // if the player and CoinGold collide
            if (_collidedWith == "PlayerB" && _collidedThis == "CoinGold" || _collidedWith == "PlayerT" && _collidedThis == "CoinGold")
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
