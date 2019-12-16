using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace COMP2351_Game_Engine
{
    class KeyboardHandler : EventArgs, IKeyboardInput
    {
        // list of Keys
        Keys[] _inputKey;

        /// <summary>
        /// class constructor
        /// </summary>
        public KeyboardHandler()
        {
        }

        /// <summary>
        /// Returns keys pressed on keyboard
        /// </summary>
        public Keys[] GetInputKey()
        {
            // get the state of keys
            KeyboardState keyboardState = Keyboard.GetState();
            // add the pressed keys to the list _inputKeys
            _inputKey = keyboardState.GetPressedKeys();
            //return _inputKeys
            return _inputKey;
        }
    }
}