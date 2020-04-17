using EllipticCurve;


namespace StarkBank
{
    public static class Key
    {
        /// <summary>
        /// Generate a new key pair
        /// <br/>
        /// Generates a secp256k1 ECDSA private/public key pair to be used in the API authentications
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>path[string]: path to save the keys.pem files.No files will be saved if this parameter isn't provided</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>private and public key pems</item>
        /// </list>
        /// </summary>
        public static (string privateKey, string publicKey) Create()
        {
            PrivateKey privateKey = new PrivateKey();
            PublicKey publicKey = privateKey.publicKey();

            string privateKeyPem = privateKey.toPem();
            string publicKeyPem = publicKey.toPem();

            return (privateKey: privateKeyPem, publicKey: publicKeyPem);
        }

        /// <summary>
        /// Generate a new key pair
        /// <br/>
        /// Generates a secp256k1 ECDSA private/public key pair to be used in the API authentications
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>path[string]: path to save the keys.pem files.No files will be saved if this parameter isn't provided</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>private and public key pems</item>
        /// </list>
        /// </summary>
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
