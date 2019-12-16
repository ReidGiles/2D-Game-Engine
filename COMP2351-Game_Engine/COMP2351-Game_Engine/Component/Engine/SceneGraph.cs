using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP2351_Game_Engine
{
    class SceneGraph : ISceneGraph
    {
        // list containing entities
        IList<IEntity> _entityList;
        // entity to be removed
        IEntity removeEntity;

        /// <summary>
        /// class constructor
        /// </summary>
        public SceneGraph()
        {
            // set _entityList
            _entityList = new List<IEntity>();
        }

        /// <summary>
        /// This is called from scene manager when a new entity is spawned.
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public void Spawn(IEntity pEntity, float pX, float pY)
        {
            // set a booloean flag when an existing entity is found
            Boolean found = false;

            // check if entity is in the _entityList
            foreach (IEntity e in _entityList)
            {
                if (e == pEntity)
                {
                    // Set entity location
                    pEntity.SetLocation(pX, pY);
                    // set found to true
                    found = true;
                }
            }

            // if entity is not in the _entityList then found is false
            // found is false
            if (!found)
            {
                // Record entity has been spawned by adding it to the _entityList
                _entityList.Add(pEntity);
                // Set entity location
                pEntity.SetLocation(pX, pY);
            }
        }

        /// <summary>
        /// This is called from scene manager when an existing entity is spawned.
        /// </summary>
        /// <param name="pUID"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public void Spawn(int pUID, int pX, int pY)
        {
            // Check each entity in the list to see if its UID = pUID
            foreach (IEntity e in _entityList)
            {
                if (e.GetUID() == pUID)
                {
                    // Set entity location
                    e.SetLocation(pX, pY);
                }
            }
        }

        /// <summary>
        /// This is called from scene manager when an entity needs to be removed.
        /// </summary>
        /// <param name="pUID"></param>
        public void Remove(int pUID)
        {
            // Check each entity in the list to see if its UID = pUID
            foreach (IEntity e in _entityList)
            {
                if (e.GetUID() == pUID)
                {
                    // set removeEntity to the entity e
                    removeEntity = e;
                }
            }

            // if removeEntity has a value
            if (removeEntity != null)
            {
                // remove the entity from the entityList
                _entityList.Remove(removeEntity);
                // reset removeEntity to null
                removeEntity = null;
            }            
        }

        /// <summary>
        /// This is called from scene manager when an entity needs to be removed.
        /// </summary>
        /// <param name="pUName"></param>
        public void Remove(String pUName)
        {
            // Check each entity in the list to see if its UName = pUName
            foreach (IEntity e in _entityList)
            {
                if (e.GetUname() == pUName)
                {
                    // set removeEntity to the entity e
                    removeEntity = e;
                }
            }

            // if removeEntity has a value
            if (removeEntity != null)
            {
                // remove the entity from the entityList
                _entityList.Remove(removeEntity);
                // reset removeEntity to null
                removeEntity = null;
            }
        }


        /// <summary>
        /// Returns a list of entities on the scene.
        /// </summary>
        /// <returns></returns>
        public IList<IEntity> GetEntity()
        {
            return _entityList;
        }
    }
}