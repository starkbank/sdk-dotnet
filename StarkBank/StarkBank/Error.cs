using System;
using System.Collections.Generic;


namespace StarkBank.Error
{
    public class ErrorElement : Exception
    {
        public readonly string Code;
        new public readonly string Message;

        public ErrorElement(string code, string message) : base(code + ": " + message)
        {
            Code = code;
            Message = message;
        }
    }

    public class InputErrors : Exception
    {
        public readonly List<ErrorElement> Errors;

        public InputErrors(string content) : base(content)
        {
            dynamic json = Utils.Json.Decode(content);
            dynamic errors = json.errors;

            string code;
            string message;
            Errors = new List<ErrorElement>();
            foreach (dynamic error in errors)
            {
                code = error.code;
                message = error.message;
                Errors.Add(new ErrorElement(code, message));
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
        public InvalidSignatureError(string message) : base(message)
        {
        }
    }
}
