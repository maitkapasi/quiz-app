﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions
{
    public class UniqueExerciseNotAvailableException : Exception
    {
        public UniqueExerciseNotAvailableException(string message) : base(message)
        {

        }        
    }
}
