using StarkBank;


namespace StarkBankTests
{
    public static class TestUser
    {
        public static Project SetDefault()
        {
            Project project = new Project(
                "sandbox",
                "5690398416568320",
                "-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIIoYWZ2OGwqX6n1EVvj1C1YvWHSGqqhZJzfsZZnk0SVgoAcGBSuBBAAK\noUQDQgAEGS1jWJXoK9RUk+qoNNFquO7X4JzRf5ZA5UDJUfPCbbKe5KwtrBKTJC1/\nvRGIpAM5gNsxdfKgmoXNriiuY4LEPQ==\n-----END EC PRIVATE KEY-----\n"
            );
            StarkBank.User.DefaultUser = project;
            return project;
        }
    }
}
