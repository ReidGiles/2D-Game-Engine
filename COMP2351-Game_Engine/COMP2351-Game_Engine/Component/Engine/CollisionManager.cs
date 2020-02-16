
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    class CollisionManager : IUpdatable, ICollisionManager
    {
        // create a variable to store all the subscribers to the event
        public event EventHandler<ICollisionInput> NewCollisionHandler;

        // Declare a variable for collison interpenetration Type:Vector2, called _overlap
        private Vector2 _overlap;

        // Declare a variable for collison normal Type:Vector2, called _cNormal
        private Vector2 _cNormal;

        // refernce to the sceneGraph
        ISceneManager _sceneManager;

        /// <summary>
        /// contructor for the Collision Manager
        /// </summary>
        /// <param name="pSceneGraph"></param>
        public CollisionManager(ISceneManager pSceneGraph)
        {
            // initialise _sceneGraph
            _sceneManager = pSceneGraph;
            // initialise _overlap
            _overlap = new Vector2(0,0);
            // Initialise _cNormal
            _cNormal = new Vector2(0, 0);
        }

        /// <summary>
        /// Publisher method, contacts all listeners
        /// </summary>
        /// <param name="pCollided"></param>
        /// <param name="pUID"></param>
        protected virtual void OnNewCollision(String[] pCollided, int[] pUID, Vector2 pOverlap, Vector2 pCNormal)
        {
            // pass the parameters into the new keybaord input then add to NewKeyboardInput
            ICollisionInput args = new CollisionHandler(pCollided, pUID, pOverlap, pCNormal);
            NewCollisionHandler(this, args);
            
        }

        /// <summary>
        /// Subscription method, used to store reference to listeners
        /// </summary>
        /// <param name="handler"></param>
        public void AddListener(EventHandler<ICollisionInput> handler)
        {
            // ADD event handler
            NewCollisionHandler += handler;
        }

        /// <summary>
        /// Method to check collisions on AABB
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        private bool AABB(ICreateCollider A, ICreateCollider B)
        {
            // Distance between x axis origin values for A and B
            float Dx = Math.Abs(A.CreateCollider()[0] - B.CreateCollider()[0]);

            // Distance between y axis origin values for A and B
            float Dy = Math.Abs(A.CreateCollider()[1] - B.CreateCollider()[1]);

            // Check if D is less than half the width of A and B colliders combined for x and y
            if ((Dx < (A.CreateCollider()[2] + B.CreateCollider()[2]) * 0.5f) && (Dy < (A.CreateCollider()[3] + B.CreateCollider()[3]) * 0.5f))
            {
                // find overlap
                _overlap.X = ((A.CreateCollider()[2] + B.CreateCollider()[2]) * 0.5f) - Dx;
                _overlap.Y = ((A.CreateCollider()[3] + B.CreateCollider()[3]) * 0.5f) - Dy;
                // find cNormal
                _cNormal.X = B.CreateCollider()[0] - A.CreateCollider()[0];
                _cNormal.Y = B.CreateCollider()[1] - A.CreateCollider()[1];
                _cNormal.Normalize();
                return true;
            }
            else
            {
                return false;
            }
                
        }

        /// <summary>
        /// Checks through the collider list for collisions
        /// </summary>
        private void CheckCollision()
        {
            for(int i = 0; i < _sceneManager.GetEntity().Count -1; i++)
            {
                for (int j=i+1; j < _sceneManager.GetEntity().Count; j++)
                {
                    // check entity has a collision listener
                    if (_sceneManager.GetEntity()[i] is ICollisionListener && _sceneManager.GetEntity()[j] is ICollisionListener)
                    {
                        // check the entity has a collider set up
                        if (_sceneManager.GetEntity()[i].CheckCollider() && _sceneManager.GetEntity()[j].CheckCollider())
                        {
                            // get a reference to the entities colliders for I and J                            
                            List<ICreateCollider> colliderI = _sceneManager.GetEntity()[i].GetCollider();
                            List<ICreateCollider> colliderJ = _sceneManager.GetEntity()[j].GetCollider();
                                                        
                            // check midphase collider
                            // Check if D is less than half the width of I and J colliders combined for x and y
                            if (AABB(colliderI[0], colliderJ[0]))
                            {
                                colliderI.Remove(_sceneManager.GetEntity()[i].GetCollider()[0]);
                                colliderJ.Remove(_sceneManager.GetEntity()[j].GetCollider()[0]);

                                // each collider in I
                                foreach (ICreateCollider k in colliderI)
                                {
                                    // each collider in j
                                    foreach (ICreateCollider l in colliderJ)
                                    {
                                        // Check if D is less than half the width of I and J colliders combined for x and y
                                        if (AABB(k,l))
                                        {
                                            // colliding
                                            // get the collider tag
                                            String[] collided = { k.GetTag(), l.GetTag() };
                                            // get the colliding entity ID
                                            int[] uID = { _sceneManager.GetEntity()[i].GetUID(), _sceneManager.GetEntity()[j].GetUID() };
                                            // publish collision
                                            OnNewCollision(collided, uID,_overlap,_cNormal);

                                        }
                                    }
                                }
                            }                       
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs publisher methods when input is detected
        /// </summary>
        public void Update(GameTime gameTime)
        {
            CheckCollision();
        }
    }
}