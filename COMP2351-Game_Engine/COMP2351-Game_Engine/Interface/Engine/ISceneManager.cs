using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP2351_Game_Engine
{
    interface ISceneManager
    {
        void Spawn(IEntity pEntity, float pX, float pY);
        void Spawn(int pUID, int pX, int pY);
        void Remove(int pUID);
        void Remove(String pUName);
        IEntity GetEntity(int pUID);
        IEntity GetEntity(string pUName);
        IList<IEntity> GetEntity();
    }
}