﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3351_Game_Engine
{
    public interface IMouseListener
    {
        void OnNewMouseInput(object sender, IMouseInput args);
    }
}
