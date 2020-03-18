using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface IKeyboardListener
    {
        void OnNewKeyboardInput(object sender, IKeyboardInput args);
    }
}