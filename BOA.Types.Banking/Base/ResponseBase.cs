﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class ResponseBase
    {

        public bool IsSuccess;

        public string ErrorMessage;

        public Object DataContract { get; set; } //value
    }
}
