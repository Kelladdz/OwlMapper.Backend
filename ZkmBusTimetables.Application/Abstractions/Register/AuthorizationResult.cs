﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Application.Abstractions.Register
{
    public class AuthorizationResult
    {
        public AuthorizationResult() { }
        private AuthorizationResult(bool isAuthorized, string failureMessage)
        {
            IsAuthorized = isAuthorized;
            FailureMessage = failureMessage;
        }
        public bool IsAuthorized { get; }
        public string FailureMessage { get; set; }

        public static AuthorizationResult Fail()
        {
            return new AuthorizationResult(false, null);
        }

        public static AuthorizationResult Fail(string failureMessage)
        {
            return new AuthorizationResult(false, failureMessage);
        }

        public static AuthorizationResult Succeed()
        {
            return new AuthorizationResult(true, null);
        }
    }
}
