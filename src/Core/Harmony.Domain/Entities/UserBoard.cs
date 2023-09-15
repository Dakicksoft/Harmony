﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Domain.Entities
{
    /// <summary>
    /// Class to represent M 2 M relationship between users and boards
    /// (intermediate table)
    /// </summary>
    public class UserBoard
    {
        public string UserId { get; set; }
        public Board Board { get; set; }
        public Guid BoardId { get; set; }
    }
}
