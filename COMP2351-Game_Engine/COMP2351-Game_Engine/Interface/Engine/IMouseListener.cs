using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2351_Game_Engine
{
    interface IMouseListener
    {
        void OnNewMouseInput(object sender, IMouseInput args);
    }
}
