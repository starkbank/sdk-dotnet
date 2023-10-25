﻿using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using StarkCore;


namespace StarkBankTests
{
    public class BoletoTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGetGetPdfAndDelete()
        {
            List<Boleto> boletos = Boleto.Create(new List<Boleto>() {Example()});
            Boleto boleto = boletos.First();
            TestUtils.Log(boleto);
            Assert.NotNull(boletos.First().ID);
            Boleto getBoleto = Boleto.Get(id: boleto.ID);
            Assert.Equal(getBoleto.ID, boleto.ID);
            byte[] pdf = Boleto.Pdf(id: boleto.ID);
            Assert.True(pdf.Length > 0);
            System.IO.File.WriteAllBytes("boleto.pdf", pdf);
            byte[] bookletPdf = Boleto.Pdf(id: boleto.ID, layout: "booklet");
            Assert.True(bookletPdf.Length > 0);
            System.IO.File.WriteAllBytes("boleto-booklet.pdf", bookletPdf);
            Boleto deleteBoleto = Boleto.Delete(id: boleto.ID);
            Assert.Equal(deleteBoleto.ID, boleto.ID);
            TestUtils.Log(boleto);
        }

        [Fact]
        public void Query()
        {
            List<Boleto> boletos = Boleto.Query(limit: 101, status: "paid").ToList();
            Assert.Equal(101, boletos.Count);
            Assert.True(boletos.First().ID != boletos.Last().ID);
            foreach (Boleto boleto in boletos)
            {
                TestUtils.Log(boleto);
                Assert.NotNull(boleto.ID);
                Assert.Equal("paid", boleto.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Boleto> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Boleto.Page(limit: 5, cursor: cursor);
                foreach (Boleto entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (page == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }

        internal static Boleto Example()
        {
            return new Boleto(
                amount: 1000000,
                due: DateTime.Today.Date.AddDays(10),
                name: "Random Company",
                streetLine1: "Rua ABC",
                streetLine2: "Ap 123",
                district: "Jardim Paulista",
                city: "São Paulo",
                stateCode: "SP",
                zipCode: "01234-567",
                taxID: "012.345.678-90",
                overdueLimit: 10,
                receiverName: "Random Receiver",
                receiverTaxID: "123.456.789-09",
                fine: 0.00,
                interest: 0.00,
                tags: new List<string> { "custom", "tags" },
                descriptions: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"text", "product A"},
                        {"amount", 123}
                    },
                    new Dictionary<string, object> {
                        {"text", "product B"},
                        {"amount", 456}
                    },
                    new Dictionary<string, object> {
                        { "text", "product C"},
                        { "amount", 789}
                    }
                },
                discounts: new List<Dictionary<string, object>>() {
                    new Dictionary<string, object> {
                        {"percentage", 5},
                        {"date", DateTime.Today.Date.AddDays(1)}
                    },
                    new Dictionary<string, object> {
                        {"percentage", 3.5},
                        {"date", DateTime.Today.Date.AddDays(2)}
                    }
                }
            );
        }
    }
}
