using StarkBank.Utils;


namespace StarkBank
{
    public abstract class User : Resource
    {
        public string Pem { get; }
        public string Environment { get; }

        internal User(string environment, string id, string privateKey) : base(id)
        {
            Pem = Checks.CheckPrivateKey(privateKey);
            Environment = Checks.CheckEnvironment(environment);
        }

        internal EllipticCurve.PrivateKey PrivateKey()
        {
            return EllipticCurve.PrivateKey.fromPem(Pem);
        }

        internal abstract string AccessId();
    }
}
