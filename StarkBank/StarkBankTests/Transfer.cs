using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransferTest
    {
        public readonly User user = TestUser.SetDefault();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<Transfer> transfers = Transfer.Create(new List<Transfer>() { Example() });
            Transfer transfer = transfers.First();
            Assert.NotNull(transfers.First().ID);
            Transfer getTransfer = Transfer.Get(id: transfer.ID);
            Assert.Equal(getTransfer.ID, transfer.ID);
            byte[] pdf = Transfer.Pdf(id: transfer.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("transfer.pdf", pdf);
            Transfer deleteTransfer = Transfer.Delete(id: transfer.ID);
            Assert.True(deleteTransfer.Status == "canceled");
            Console.WriteLine(transfer);
        }

        [Fact]
        public void Query()
        {
            List<Transfer> transfers = Transfer.Query(limit: 101, status: "success").ToList();
            Assert.True(transfers.Count <= 101);
            Assert.True(transfers.First().ID != transfers.Last().ID);
            foreach (Transfer transfer in transfers)
            {
                Console.WriteLine(transfer);
                Assert.NotNull(transfer.ID);
                Assert.Equal("success", transfer.Status);
            }
        }

        private Transfer Example()
        {
            return new Transfer(
                amount: new Random().Next(1, 1000),
                name: "João",
                taxID: "01234567890",
                bankCode: "01",
                branchCode: "0001",
                accountNumber: "10000-0",
                scheduled: DateTime.Today.Date.AddDays(1)
            );
        }
    }
}
