using EllipticCurve;


namespace StarkBank
{
    public static class Key
    {
        public static (string privateKey, string publicKey) Create()
        {
            PrivateKey privateKey = new PrivateKey();
            PublicKey publicKey = privateKey.publicKey();

            string privateKeyPem = privateKey.toPem();
            string publicKeyPem = publicKey.toPem();

            return (privateKey: privateKeyPem, publicKey: publicKeyPem);
        }

        public static (string privateKey, string publicKey) Create(string path)
        {
            (string privateKeyPem, string publicKeyPem) = Key.Create();

            System.IO.Directory.CreateDirectory(path);

            System.IO.File.WriteAllText(System.IO.Path.Combine(path, "privateKey.pem"), privateKeyPem);
            System.IO.File.WriteAllText(System.IO.Path.Combine(path, "publicKey.pem"), publicKeyPem);

            return (privateKey: privateKeyPem, publicKey: publicKeyPem);
        }
    }
}
