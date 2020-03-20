﻿using System;
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
    public abstract class Mind : IMind, IUpdatable
    {
        // Entity UID
        protected int _entityUID;
        // Entity texture:
        protected Texture2D _texture;
        // Entity texture FacingDirection
        protected float _facingDirectionX;
        // Entity location:
        protected Vector2 _location;
        // reference to the mind ID
        protected String _mindID;
        // list of the colliders in the mind
        protected List<ICreateCollider> _colliders;
        // this entities collider that is colliding
        protected String _collidedThis;
        // enitity that collided with this entities collider
        protected String _collidedWith;
        // this entities collider that is collidings UID
        protected int _collidedThisUID;
        // enitity that collided with this entities collider UID
        protected int _collidedWithUID;
        // overlap of collided entities
        protected Vector2 _overlap;
        // cNormal of collided entities
        protected Vector2 _cNormal;
        // Physics component
        protected IPhysicsComponent _physicsComponent;
        // dictionary storing state machine states
        protected Dictionary<string, IState> _stateDictionary = new Dictionary<string, IState>();
        // current state
        protected IState _currentState;
        // Animator
        protected IAnimator _animator;
        // Audio player
        protected IAudioPlayer _audioPlayer;
        // Game Time
        protected GameTime _gameTime;
        // delegates for entity
        public PassVector2 eTranslate;

        public PassFloat eInvertTexture;

        public GetVector2 eGetLocation;

        public void SetEntityUID(int pUID)
        {
            _entityUID = pUID;
        }

        public void SetAnimator(IAnimator pAnimator)
        {
            _animator = pAnimator;
        }

        public void SetAudioPlayer(IAudioPlayer pAudioPlayer)
        {
            _audioPlayer = pAudioPlayer;
        }

        public void SetDelegates(PassVector2 pETranslate,PassFloat pEInvertTexture, GetVector2 pEGetLocation)
        {
            eTranslate = pETranslate;
            eInvertTexture = pEInvertTexture;
            eGetLocation = pEGetLocation;
        }

        /// <summary>
        /// State machine logic.
        /// </summary>
        public void StateMachine()
        {
            // Update state logic
            ((IUpdatable)_currentState).Update(_gameTime);
            // Check trigger and store result
            string trigger = _currentState.Trigger();
            // Change state if a trigger is received
            if (trigger != null)
                _currentState = _stateDictionary[trigger];

            /*switch (_currentState)
            {
                case PlayerIdleState pis:
                    ((IUpdatable)_stateDictionary["Idle"]).Update(_gameTime);
                    trigger = _stateDictionary["Idle"].Trigger();
                    if (trigger != null)
                    {
                        _currentState = _stateDictionary[trigger];
                    }
                    break;
                case PlayerJumpState pjs:
                    ((IUpdatable)_stateDictionary["Jump"]).Update(_gameTime);
                    trigger = _stateDictionary["Jump"].Trigger();
                    if (trigger != null)
                    {
                        _currentState = _stateDictionary[trigger];
                    }
                    break;
                case PlayerRunState prs:
                    ((IUpdatable)_stateDictionary["Run"]).Update(_gameTime);
                    trigger = _stateDictionary["Run"].Trigger();
                    if (trigger != null)
                    {
                        _currentState = _stateDictionary[trigger];
                    }
                    break;
            }*/
        }

        /// <summary>
        /// Updates entity location
        /// </summary>
        /// <param name="pLocation"></param>
        public void UpdateLocation(Vector2 pLocation)
        {
            // set _location
            _location = pLocation;
        }

        /// <summary>
        /// Returns the Facing Direction
        /// </summary>
        /// <param name="pTexture"></param>
        public float GetFacingDirection()
        {
            return _facingDirectionX;
        }

        /// <summary>
        /// Updates entity texture
        /// </summary>
        /// <param name="pColliders"></param>
        public void SetCollider(List<ICreateCollider> pColliders)
        {
            // set _colliders
            _colliders = pColliders;
        }

        public virtual void SetPhysicsComponent(IPhysicsComponent pPhysicsComponent)
        {
            _physicsComponent = pPhysicsComponent;
        }

        /// <summary>
        /// Translates x position
        /// </summary>
        public virtual float TranslateX()
        {
            return 0;
        }

        /// <summary>
        /// Translates y position
        /// </summary>
        public virtual float TranslateY()
        {
            return 0;
        }

        /// <summary>
        /// Translates y position
        /// </summary>
        public virtual Vector2 Translate()
        {
            Vector2 dPos = new Vector2(0, 0);
            return dPos;
        }

        /// <summary>
        /// called when a collision event occures
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual bool OnNewCollision(ICollisionInput args)
        {
            // check if the entity has any colliders
            if (_colliders != null)
            {
                // find which collider in the _colliders is colliding
                foreach (ICreateCollider i in _colliders)
                {
                    // set _colliderThis and _collided with
                    if (i.GetTag() == args.GetCollided()[0])
                    {
                        _collidedThis = args.GetCollided()[0];
                        _collidedWith = args.GetCollided()[1];
                        _collidedThisUID = args.GetUID()[0];
                        _collidedWithUID = args.GetUID()[1];
                    }
                    else if(i.GetTag() == args.GetCollided()[1])
                    {
                        _collidedThis = args.GetCollided()[1];
                        _collidedWith = args.GetCollided()[0];
                        _collidedThisUID = args.GetUID()[1];
                        _collidedWithUID = args.GetUID()[0];
                    }
                    _overlap = args.GetOverlap();
                    _cNormal = args.GetCNormal();
                }
            } 
            //return whether the entity should kill itself
            return false;
        }


        /// <summary>
        /// Updates mind, called by AIComponentManager
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}