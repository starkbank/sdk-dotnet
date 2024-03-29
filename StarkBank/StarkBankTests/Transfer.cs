﻿using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class TransferTest
    {
        public readonly User user = TestUser.SetDefaultProject();

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
            TestUtils.Log(transfer);
            foreach (Transfer.Rule rule in transfer.Rules)
            {
                TestUtils.Log(rule);
            }
        }

        [Fact]
        public void Query()
        {
            List<Transfer> transfers = Transfer.Query(limit: 101, status: "success").ToList();
            Assert.True(transfers.Count <= 101);
            Assert.True(transfers.First().ID != transfers.Last().ID);
            foreach (Transfer transfer in transfers)
            {
                TestUtils.Log(transfer);
                Assert.NotNull(transfer.ID);
                Assert.Equal("success", transfer.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Transfer> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Transfer.Page(limit: 5, cursor: cursor);
                foreach (Transfer entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<Transfer> transfers = Transfer.Query(limit: 10).ToList();
            List<String> transfersIdsExpected = new List<string>();
            Assert.Equal(10, transfers.Count);
            Assert.True(transfers.First().ID != transfers.Last().ID);
            foreach (Transfer transaction in transfers)
            {
                Assert.NotNull(transaction.ID);
                transfersIdsExpected.Add(transaction.ID);
            }

            List<Transfer> transfersResult = Transfer.Query(limit:10, ids: transfersIdsExpected).ToList();
            List<String> transfersIdsResult = new List<string>();
            Assert.Equal(10, transfers.Count);
            Assert.True(transfers.First().ID != transfers.Last().ID);
            foreach (Transfer transaction in transfersResult)
            {
                Assert.NotNull(transaction.ID);
                transfersIdsResult.Add(transaction.ID);
            }

            transfersIdsExpected.Sort();
            transfersIdsResult.Sort();
            Assert.Equal(transfersIdsExpected, transfersIdsResult);
        }
        internal static Transfer Example(bool schedule = true)
        {
            DateTime? scheduled = null;
            if (schedule) {
                scheduled = DateTime.Now.AddDays(1);
            }
            return new Transfer(
                amount: new Random().Next(1, 1000),
                name: "João",
                taxID: "01234567890",
                bankCode: "18236120",
                branchCode: "0001",
                accountNumber: "10000-0",
                accountType: "checking",
                externalID: Guid.NewGuid().ToString(),
                scheduled: scheduled,
                description: "Good description",
                rules: new List<Transfer.Rule>() {
                    new Transfer.Rule(
                        key: "resendingLimit",
                        value: 5
                    )
                }
            );
        }
    }
}
