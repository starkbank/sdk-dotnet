using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class WorkspaceTest
    {
        public readonly Organization organization = TestUser.GetOrganization();

        [Fact]
        public void QueryAndGet()
        {
            List<Workspace> workspaces = Workspace.Query(user: organization).ToList();
            foreach (Workspace workspace in workspaces)
            {
                Console.WriteLine(workspace);
                Assert.NotNull(workspace.ID);
                Assert.NotNull(workspace.Username);
                Assert.NotNull(workspace.Name);
                Workspace getWorkspace = Workspace.Get(workspace.ID, user: Organization.Replace(organization, workspace.ID));
                Assert.Equal(workspace.ID, getWorkspace.ID);
            }
        }

        [Fact]
        public void CreateAndUpdate()
        {
            Workspace workspace = Example();
            workspace = Workspace.Create(username: workspace.Username, name: workspace.Name, allowedTaxIds: workspace.AllowedTaxIds, user: organization);
            Assert.NotNull(workspace.ID);
            Assert.NotNull(workspace.Username);
            Assert.NotNull(workspace.Name);
            TestUtils.Log(workspace);

            string name = "New name test";
            string username = Guid.NewGuid().ToString();
            List<string> allowedTaxIds = new List<string>(new string[] { "964.480.450-31", "20.018.183/0001-80" });
            Workspace updatedWorkspace = Workspace.Update(
                id: workspace.ID,
                name: name,
                username: username,
                allowedTaxIds: allowedTaxIds,
                user: Organization.Replace(organization, workspace.ID)
            );
            Assert.Equal(name, updatedWorkspace.Name);
            Assert.Equal(username, updatedWorkspace.Username);
            Assert.Equal(allowedTaxIds, updatedWorkspace.AllowedTaxIds);
        }

        internal static Workspace Example()
        {
            string guid = Guid.NewGuid().ToString();
            return new Workspace(
                username: "starkv2-" + guid,
                name: "Stark V2: " + guid,
                allowedTaxIds: new List<string>(new string[] { "964.480.450-31", "263.122.860-02" })
            );
        }
    }
}
