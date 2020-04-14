using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class EventTest
    {
        public readonly User user = TestUser.SetDefault();
        public readonly string Content = "{\"event\": {\"log\": {\"transfer\": {\"status\": \"processing\", \"updated\": \"2020-04-03T13:20:33.485644+00:00\", \"fee\": 160, \"name\": \"Lawrence James\", \"accountNumber\": \"10000-0\", \"id\": \"5107489032896512\", \"tags\": [], \"taxId\": \"91.642.017/0001-06\", \"created\": \"2020-04-03T13:20:32.530367+00:00\", \"amount\": 2, \"transactionIds\": [\"6547649079541760\"], \"bankCode\": \"01\", \"branchCode\": \"0001\"}, \"errors\": [], \"type\": \"sending\", \"id\": \"5648419829841920\", \"created\": \"2020-04-03T13:20:33.164373+00:00\"}, \"subscription\": \"transfer\", \"id\": \"6234355449987072\", \"created\": \"2020-04-03T13:20:40.784479+00:00\"}}";
        public readonly string GoodSignature = "MEYCIQCmFCAn2Z+6qEHmf8paI08Ee5ZJ9+KvLWSS3ddp8+RF3AIhALlK7ltfRvMCXhjS7cy8SPlcSlpQtjBxmhN6ClFC0Tv6";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void QueryGetUpdateAndDelete()
        {
            List<Event> events = Event.Query(limit: 101, isDelivered: false).ToList();
            Assert.Equal(101, events.Count);
            Assert.True(events.First().ID != events.Last().ID);
            foreach (Event eventObject in events)
            {
                Assert.NotNull(eventObject.ID);
                Assert.False(eventObject.IsDelivered);
            }
            Event undeliveredEvent = events.First();
            Event getEvent = Event.Get(undeliveredEvent.ID);
            Assert.Equal(getEvent.ID, undeliveredEvent.ID);
            Event updateEvent = Event.Update(id: getEvent.ID, isDelivered: true);
            Assert.Equal(getEvent.ID, updateEvent.ID);
            Assert.True(updateEvent.IsDelivered);
            Event deleteEvent = Event.Delete(getEvent.ID);
            Assert.Equal(getEvent.ID, deleteEvent.ID);
        }

        [Fact]
        public void ParseWithRightSignature()
        {
            Event parsedEvent = Event.Parse(Content, GoodSignature);
            Assert.NotNull(parsedEvent.ID);
            Assert.NotNull(parsedEvent.Log);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                Event parsedEvent = Event.Parse(Content, BadSignature);
            } catch (StarkBank.Error.InvalidSignatureError) {
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }
    }
}
