using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using COMP3351_Game_Engine.Component.Engine;
using COMP3351_Game_Engine.Interface.Engine;
using COMP3351_Game_Engine;

namespace COMP3351_Engine_Demo
{
    class Player : Entity, IPlayer, ICollisionListener
    {
        public Player()
        {
        }

        /// <summary>
        /// Initialisation logic
        /// </summary>
        public override void Initialise()
        {
            // Set initial entity mind:
            SetMind(_aiComponentManager.RequestMind<PlayerMind>());

            _physicsComponent = new PhysicsComponent(location,1,new Vector2(0f, 0.9f),new Vector2(0f,0.9f));
            _mind.SetPhysicsComponent(_physicsComponent);
            _mind.SetEntityUID(GetUID());
            Console.WriteLine(GetUID());
        }

        public void OnNewCollision(object sender, ICollisionInput args)
        {
            // Check if this entity is the one colliding
            if (_uid == args.GetUID()[0] || _uid == args.GetUID()[1])
            {
                // If entity is not flagged for the removal from the scene using _killSelf
                if (!_killSelf)
                {
                    //set _killSelf to the result of the collision method in the mind
                    this._killSelf = _mind.OnNewCollision(args);
                }
            }
        }

        public override void SetCollider()
        {
            // Create a list of colliders
            _colliders = new List<ICollider>();
            // create an origin point for the collider
            Vector2 ColliderOrigin;

            // Set MidPhase Collider (encompasses all other colliders within its area)
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height, "Overall"));

            // Set Collider for the Top of the Player
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.125f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height/4, "PlayerT"));
            
            // Set Collider for the Bottom of the Player
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + texture.Height-5;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, 10, "PlayerB"));

            // Set Collider for the Middle of the Player
            ColliderOrigin.X = location.X + 0.5f * texture.Width;
            ColliderOrigin.Y = location.Y + 0.5f * texture.Height;
            // Add collider to list
            _colliders.Add(new RectCollider(ColliderOrigin, texture.Width, texture.Height - 20, "PlayerM"));

            // Add the collider list to the mind
            _mind.SetCollider(_colliders.Cast<ICreateCollider>().ToList());

            // SET has collider bool to true
            hasCollider = true;
        }

        /// <summary>
        /// Overides Update() with unique entity behaviour.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // if there are no colliders then set them using SetCollider method
            /*
            if (_mind != null)
            {
                //tell the mind the location of the player
                _mind.UpdateLocation(location);
                //tell the mind the value for texture and check to see if texture needs to be inverted
                InvertTexture(_mind.GetFacingDirection());
                //updates the position of the player
                //float DX = _mind.TranslateX();
                //float DY = _mind.TranslateY();
                //Translate(DX, DY);
                // Change in Entity location
                Vector2 dlocation = _mind.Translate();
                Translate(dlocation);
            }*/
            //else Console.WriteLine("Error: Mind is null");
            /*Console.WriteLine("Top"+((ICreateCollider)_collider).CreateCollider()[0]);
            Console.WriteLine("Bottom" + ((ICreateCollider)_collider).CreateCollider()[1]);
            Console.WriteLine("Left" + ((ICreateCollider)_collider).CreateCollider()[2]);
            Console.WriteLine("Right" + ((ICreateCollider)_collider).CreateCollider()[3]);*/
        }
    }
}