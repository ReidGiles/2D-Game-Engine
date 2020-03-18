using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace COMP3351_Game_Engine
{
    public interface IKeyboardInput
    {
        /// <summary>
        /// Returns keys pressed on keyboard
        /// </summary>
        Keys[] GetInputKey();
    }
}
