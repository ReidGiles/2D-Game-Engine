using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace COMP2351_Game_Engine
{
    interface IKeyboardInput
    {
        /// <summary>
        /// Returns keys pressed on keyboard
        /// </summary>
        Keys[] GetInputKey();
    }
}
