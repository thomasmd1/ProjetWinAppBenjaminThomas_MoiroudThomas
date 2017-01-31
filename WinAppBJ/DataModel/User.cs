﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class User
    {
        public String Name { get; set; }
        public String PictureUri { get; set; }

        public User(String n)
        {
            this.Name = n;
            this.PictureUri = "http://lorempixel.com/64/64/people/";
        }
    }
}
