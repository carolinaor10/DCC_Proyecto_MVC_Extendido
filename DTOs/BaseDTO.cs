﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    //Clase padre de los DTOs
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }    
    }
}