﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DTOs
{
    public class PeopleCreateDTO
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
    }
}
