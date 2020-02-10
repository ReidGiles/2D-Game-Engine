using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP2351_Game_Engine.Component.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IMind
    {
        void UpdateLocation(Vector2 pLocation);
        float UpdateTexture(Texture2D pTexture);
        void SetCollider(List<ICreateCollider> pColliders);
        void SetPhysicsComponent(PhysicsComponent pPhysicsComponent);
        float TranslateX();
        float TranslateY();
        bool OnNewCollision(ICollisionInput args);
        void SetAnimator(IAnimator pAnimator);
        void SetAudioPlayer(IAudioPlayer pAudioPlayer);
        void StateMachine();
    }
}
