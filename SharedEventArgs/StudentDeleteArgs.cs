﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEventArgs
{
    public class StudentDeleteArgs : System.EventArgs
    {
        public int Index { get; }

        public StudentDeleteArgs(int index)
        {
            Index = index;
        }
    }
}
