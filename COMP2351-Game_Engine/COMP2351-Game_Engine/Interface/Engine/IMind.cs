﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP2351_Game_Engine.Component.Engine;
using COMP2351_Game_Engine.Interface.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    interface IMind
    {
        void UpdateLocation(Vector2 pLocation);
        float UpdateTexture(Texture2D pTexture);
        void SetCollider(List<ICreateCollider> pColliders);
        void SetPhysicsComponent(IPhysicsComponent pPhysicsComponent);
        float TranslateX();
        float TranslateY();
        Vector2 Translate();
        bool OnNewCollision(ICollisionInput args);
        void SetAnimator(IAnimator pAnimator);
        void SetAudioPlayer(IAudioPlayer pAudioPlayer);
        void StateMachine();
    }
}
