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
        public void Create()
        {
            Workspace workspace = Example();
            workspace = Workspace.Create(username: workspace.Username, name: workspace.Name, user: organization);
            Assert.NotNull(workspace.ID);
            Assert.NotNull(workspace.Username);
            Assert.NotNull(workspace.Name);
            TestUtils.Log(workspace);
        }

        internal static Workspace Example()
        {
            string guid = Guid.NewGuid().ToString();
            return new Workspace(
                username: "starkv2-" + guid,
                name: "Stark V2: " + guid
            );
        }
    }
}
