using System;
using System.Collections.Generic;


namespace StarkBank.Error
{
    public class Error : Exception
    {
        public readonly string Code;
        new public readonly string Message;

        public Error(string code, string message) : base(code + ": " + message)
        {
            Code = code;
            Message = message;
        }
    }

    public class InputErrors : Exception
    {
        public readonly List<Error> Errors;

        public InputErrors(string content) : base(content)
        {
            dynamic errors = Utils.Json.Decode(content);

            foreach (dynamic error in errors)
            {
                Errors.Add(new Error(error.code, error.message));
            }
        }
    }

    public class InternalServerError : Exception
    {
        public InternalServerError(string message = "Houston, we have a problem.") : base(message)
        {
        }
    }

    public class UnknownError : Exception
    {
        public UnknownError(string message) : base("Unknown exception encountered: " + message)
        {
        }
    }

    public class InvalidSignatureError : Exception
    {
    }
}
