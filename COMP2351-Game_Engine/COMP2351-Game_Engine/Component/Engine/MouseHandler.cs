using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace COMP2351_Game_Engine
{
    class MouseHandler : EventArgs, IMouseInput
    {
        // list to store the mouse posiition
        private int[] mouseVal;

        /// <summary>
        /// class constructor
        /// </summary>
        public MouseHandler()
        {
        }

        /// <summary>
        /// Method to return the mouse location
        /// </summary>
        /// <returns></returns>
        public int[] GetMouseVal()
        {
            // get the mouse state
            MouseState mouseState = Mouse.GetState();
            // check if the mouse button is press
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // set the mouse val to the position of the mouse
                mouseVal = new int[] {mouseState.X, mouseState.Y};
            }
            //return the mouseVal
            return mouseVal;
        }
    }
}
