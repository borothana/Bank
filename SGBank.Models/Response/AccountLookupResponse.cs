﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SGBank.Models.Response
{
    public class AccountLookupResponse:Response
    {
        public Account Account { get; set; }
    }
}
