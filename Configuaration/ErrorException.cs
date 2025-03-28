﻿using apicampusjob.Enums;

namespace apicampusjob.Configuaration
{
    public class ErrorException : Exception
    {
        public ErrorCode Code { get; set; }
        public string Message { get; set; }

        public ErrorException(ErrorCode code = ErrorCode.SUCCESS, string message = "")
        {
            Code = code;
            Message = message;
        }
    }
}
