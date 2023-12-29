using System;
using StarkBank.Utils;
using System.Collections.Generic;


namespace StarkBank.Error
{
    public class StarkBankError : Exception {
        public StarkBankError(string message) : base(message) { }
    }

    public class ErrorElement : StarkBankError
    {
        public readonly string Code;
        new public readonly string Message;

        public ErrorElement(string code, string message) : base(code + ": " + message)
        {
            Code = code;
            Message = message;
        }
    }

    public class InputErrors : StarkBankError
    {
        public readonly List<ErrorElement> Errors;

        public InputErrors(string content) : base(content)
        {
            dynamic json = StarkCore.Utils.Json.Decode(content);
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

    public class InternalServerError : StarkBankError
    {
        public InternalServerError(string message = "Houston, we have a problem.") : base(message)
        {
        }
    }

    public class UnknownError : StarkBankError
    {
        public UnknownError(string message) : base("Unknown exception encountered: " + message)
        {
        }
    }

    public class InvalidSignatureError : StarkBankError
    {
        public InvalidSignatureError(string message) : base(message)
        {
        }
    }
}
