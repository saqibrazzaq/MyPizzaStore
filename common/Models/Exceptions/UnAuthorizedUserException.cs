﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Models.Exceptions
{
    public class UnAuthorizedUserException : Exception
    {
        public UnAuthorizedUserException(string message) : base(message)
        {

        }
    }
}
