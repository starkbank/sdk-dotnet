using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class WorkspaceTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void QueryAndGet()
        {
            List<Workspace> workspaces = Workspace.Query().ToList();
            foreach (Workspace workspace in workspaces)
            {
                Console.WriteLine(workspace);
                Assert.NotNull(workspace.ID);
                Assert.NotNull(workspace.Username);
                Assert.NotNull(workspace.Name);
                Assert.Equal(workspace.ID, Workspace.Get(workspace.ID).ID);
            }
        }
    }
}
