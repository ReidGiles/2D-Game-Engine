﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace COMP3351_Game_Engine
{
    class AIComponentManager : IUpdatable, IAIComponentManager
    {
        // reference to a list to store Minds
        private IList<IMind> _mindList;
        // reference to the input manager
        private IInputManager _inputManager;
        // Reference to the audio manager
        private IAudioPlayer _audioManager;

        private ISceneManager _sceneManager;

        private ContentManager _contentManager;

        /// <summary>
        /// //constructor for the AI component manager
        /// </summary>
        /// <param name="pInputManager"></param>
        public AIComponentManager(IInputManager pInputManager, IAudioPlayer pAudioManager, ISceneManager pSceneManager, ContentManager pContentManager)
        {
            //initialise _mindList
            _mindList = new List<IMind>();
            //initialise _inputManager
            _inputManager = pInputManager;
            // Initialise _audioManager
            _audioManager = pAudioManager;

            _sceneManager = pSceneManager;

            _contentManager = pContentManager;
        }

        /// <summary>
        /// Returns an instance of requested entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMind RequestMind<T>() where T : IMind, new()
        {
            //instanciate a new Mind
            IMind mind = new T();
            // Deploy _animator to mind
            mind.SetAnimator(new Animator(_sceneManager, _contentManager));
            // Deploy _audioManager to mind
            mind.SetAudioPlayer(_audioManager);
            // Add a keyboard listener if the class implements the interface for it
            if (mind is IKeyboardListener)
            {
                // add listener
                _inputManager.AddListener(((IKeyboardListener)mind).OnNewKeyboardInput);
            }
            //add the newly created mind to the mind list
            _mindList.Add(mind);

            return mind;
        }

        /// <summary>
        /// Method to remove a mind from the _mind List
        /// </summary>
        /// <param name="pMind"></param>
        public void RemoveMind(IMind pMind)
        {
            _mindList.Remove(pMind);
        }

        /// <summary>
        /// Updates all minds
        /// </summary>
        public void Update(GameTime gameTime)
        {
            foreach (IEntity e in _sceneManager.GetEntity())
            {
                if (e.KillSelf())
                {
                }
                else
                {
                    ((IUpdatable)e.GetMind()).Update(gameTime);
                }
            }
        }
    }
}