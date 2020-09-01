using System;
using StarkBank;


namespace StarkBankTests
{
    public static class TestUser
    {
        public static Project SetDefault()
        {
            Project project = new Project(
                "sandbox",
                Environment.GetEnvironmentVariable("SANDBOX_ID"), // "9999999999999999",
                Environment.GetEnvironmentVariable("SANDBOX_PRIVATE_KEY") // "-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIBEcEJZLk/DyuXVsEjz0w4vrE7plPXhQxODvcG1Jc0WToAcGBSuBBAAK\noUQDQgAE6t4OGx1XYktOzH/7HV6FBukxq0Xs2As6oeN6re1Ttso2fwrh5BJXDq75\nmSYHeclthCRgU8zl6H1lFQ4BKZ5RCQ==\n-----END EC PRIVATE KEY-----\n"
            );
            Settings.User = project;
            return project;
        }

        public static Organization GetOrganization(string workspaceID = null)
        {
            Organization organization = new Organization(
                environment: "sandbox",
                id: Environment.GetEnvironmentVariable("SANDBOX_ORGANIZATION_ID"), // "9999999999999999",
                privateKey: Environment.GetEnvironmentVariable("SANDBOX_ORGANIZATION_PRIVATE_KEY"), // "-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIBEcEJZLk/DyuXVsEjz0w4vrE7plPXhQxODvcG1Jc0WToAcGBSuBBAAK\noUQDQgAE6t4OGx1XYktOzH/7HV6FBukxq0Xs2As6oeN6re1Ttso2fwrh5BJXDq75\nmSYHeclthCRgU8zl6H1lFQ4BKZ5RCQ==\n-----END EC PRIVATE KEY-----\n"
                workspaceID: workspaceID
            );
            return organization;
        }
    }
}
