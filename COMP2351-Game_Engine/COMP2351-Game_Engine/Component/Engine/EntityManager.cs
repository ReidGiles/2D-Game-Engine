using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    class EntityManager : IEntityManager, IUpdatable
    {
        // int used to set the unique uID
        private int _increment;
        // refernce to the list of entities
        private IList<IEntity> _entityList;
        // reference to the collision manager
        private ICollisionManager _collisionManager;
        // reference to the AI Component Manager
        private IAIComponentManager _AIComponentManager;
        // reference to the sceneGraph
        private ISceneGraph _sceneGraph;
        // entity to be removed
        private IEntity _removeEntity;

        /// <summary>
        /// Constructor for the class EntityManager
        /// </summary>
        /// <param name="pCollisionManager"></param>
        /// <param name="pSceneGraph"></param>
        public EntityManager(ICollisionManager pCollisionManager, ISceneGraph pSceneGraph, IAIComponentManager pAIComponentManager)
        {
            // initialise entity list
            _entityList = new List<IEntity>();
            // initialise collision manager
            _collisionManager = pCollisionManager;
            // initialuse AI component Manager
            _AIComponentManager = pAIComponentManager;
            // initialise the sceneGraph
            _sceneGraph = pSceneGraph;
        }

        /// <summary>
        /// Returns an instance of requested entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pUName"></param>
        /// <param name="pTexture"></param>
        /// <param name="pAIComponentManager"></param>
        /// <returns></returns>
        public IEntity RequestInstance<T>(String pUName, Texture2D pTexture) where T : IEntity, new()
        {
            // request a new instance of an entity
            IEntity entity = new T();
            // if entity has implements ICollisionListenr add the listener
            if (entity is ICollisionListener)
            {
                _collisionManager.AddListener(((ICollisionListener)entity).OnNewCollision);
            }
            // give the entity a mind
            entity.SetAIComponentManager(_AIComponentManager);
            // set the texture for the entity
            entity.SetTexture(pTexture);
            // initialise the entity
            entity.Initialise();
            // Generate the entities UID and UName
            GenerateUID(entity, pUName);
            // Add entity to the entity list
            _entityList.Add(entity);
            
            // return the requested entity
            return entity;
        }

        /// <summary>
        /// Request the instance of and existing entity by its UName
        /// </summary>
        /// <param name="pUName"></param>
        /// <returns></returns>
        public IEntity RequestInstance(String pUName)
        {
            // check each entity in the entity list for its UName
            foreach (IEntity e in _entityList)
            {
                if (e.GetUname() == pUName)
                {
                    // return the entity
                    return e;
                }
            }
            return null;
        }

        /// <summary>
        /// Terminate an entity by its UID
        /// </summary>
        /// <param name="pUID"></param>
        public void Terminate(int pUID)
        {
            // Find the entity that matches the passed pUID
            foreach (IEntity e in _entityList)
            {
                if (e.GetUID() == pUID)
                {
                    // set remove entity the entity that matches the pUID
                    _removeEntity = e;
                }
            }

            // if remove entity has a value
            if (_removeEntity != null)
            {
                // remove mind from the mindlist
                _AIComponentManager.RemoveMind(_removeEntity.GetMind());
                // remove entity from the sceneGraph
                _sceneGraph.Remove(_removeEntity.GetUID());
                // remove entity from the entity list
                _entityList.Remove(_removeEntity);
                // set the remove entity back to null after completion
                _removeEntity = null;
            }
        }

        /// <summary>
        /// Terminate an entity by its UName
        /// </summary>
        /// <param name="pUName"></param>
        public void Terminate(String pUName)
        {
            // Find the entity that matches the passed pUName
            foreach (IEntity e in _entityList)
            {
                if (e.GetUname() == pUName)
                {
                    // set remove entity the entity that matches the pUID
                    _removeEntity = e;
                }
            }
            if (_removeEntity != null)
            {
                // remove mind from the mindlist
                _AIComponentManager.RemoveMind(_removeEntity.GetMind());
                // remove entity from the sceneGraph
                _sceneGraph.Remove(_removeEntity.GetUID());
                // remove entity from the entity list
                _entityList.Remove(_removeEntity);
                // set the remove entity back to null after completion
                _removeEntity = null;
            }
        }

        /// <summary>
        /// Generates entity unique identification number
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="pUName"></param>
        private void GenerateUID(IEntity pEntity, String pUName)
        {
            // incerement _increment each time the method is called
            _increment++;
            // Set the enities UID to _increment and UName to pUName
            pEntity.SetUp(_increment, pUName);
        }

        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        {
            /*
            // check each entity in the entity list to see if it needs to be removed
            foreach (IEntity e in _entityList)
            {
                if (e.KillSelf())
                {
                    // if an entities killself = true then set _removeEntity to that entity
                    _removeEntity = e;
                }
            }

            // if _removeEntity has an entity
            if (_removeEntity != null)
            {
                // Terminate the entity
                Terminate(_removeEntity.GetUID());
            }*/
        }
    }
}