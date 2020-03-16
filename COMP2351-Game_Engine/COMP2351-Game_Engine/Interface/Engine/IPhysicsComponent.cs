using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine.Interface.Engine
{
    interface IPhysicsComponent
    {
        void ApplyForce(Vector2 force);
        void UpdatePhysics();
        void ApplyImpulse(Vector2 closingVelocity);
        Vector2 GetPosition();
        void RemoveOverlapY(float pOverlapY);
        void RemoveOverlapX(float pOverlapX);
        void ResetPosition(Vector2 pPos);
    }
}
