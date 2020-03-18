using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3351_Game_Engine.Component.Engine;
using COMP3351_Game_Engine.Interface.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static COMP3351_Game_Engine.Component.Engine.Delegates;

namespace COMP3351_Game_Engine
{
    public interface IMind
    {
        void UpdateLocation(Vector2 pLocation);
        float GetFacingDirection();
        void SetCollider(List<ICreateCollider> pColliders);
        void SetPhysicsComponent(IPhysicsComponent pPhysicsComponent);
        void SetDelegates(PassVector2 pETranslate, PassFloat pEInvertTexture, GetVector2 pEGetLocation);
        float TranslateX();
        float TranslateY();
        Vector2 Translate();
        bool OnNewCollision(ICollisionInput args);
        void SetAnimator(IAnimator pAnimator);
        void SetAudioPlayer(IAudioPlayer pAudioPlayer);
        void StateMachine();
    }
}
