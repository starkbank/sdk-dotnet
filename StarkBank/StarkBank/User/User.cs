﻿using StarkBank.Utils;


namespace StarkBank
{
    public abstract class User : Resource
    {
        public static User Default;

        public string Pem { get; }
        public string Environment { get; }
        readonly private string Kind;

        internal User(string environment, string id, string privateKey, string kind) : base(id)
        {
            Pem = Checks.CheckPrivateKey(privateKey);
            Environment = Checks.CheckEnvironment(environment);
            Kind = kind;
        }

        internal string AccessId()
        {
            return Kind + "/" + ID;
        }

        internal EllipticCurve.PrivateKey PrivateKey()
        {
            return EllipticCurve.PrivateKey.fromPem(Pem);
        }
    }
}
