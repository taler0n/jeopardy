﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player
    {
        public string Name { get; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
