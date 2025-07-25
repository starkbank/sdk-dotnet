using Xunit;
using StarkBank;
using StarkCore;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkBankTests
{
    public class EventTest
    {
        public readonly StarkBank.User user = TestUser.SetDefaultProject();
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
                TestUtils.Log(eventObject);
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
            TestUtils.Log(getEvent);
        }

        [Fact]
        public void QueryAndAttempt()
        {
            List<Event> events = Event.Query(limit: 2, isDelivered: false).ToList();
            Assert.Equal(2, events.Count);
            foreach (Event eventObject in events)
            {
                List<Event.Attempt> attempts = Event.Attempt.Query(limit: 1, eventIds: new List<string> { eventObject.ID }).ToList();
                foreach (Event.Attempt attempt in attempts)
                {
                    Event.Attempt attemptGet = Event.Attempt.Get(attempt.ID);
                    Assert.Equal(attempt.ID, attemptGet.ID);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Event> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Event.Page(limit: 5, cursor: cursor);
                foreach (Event entity in page)
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
        public void ParseWithRightSignature()
        {
            Event parsedEvent = Event.Parse(Content, GoodSignature);
            Assert.NotNull(parsedEvent.ID);
            Assert.NotNull(parsedEvent.Log);
            TestUtils.Log(parsedEvent);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                Event parsedEvent = Event.Parse(Content, BadSignature);
            } catch (StarkCore.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                Event parsedEvent = Event.Parse(Content, "something is definitely wrong");
            }
            catch (StarkCore.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }
    }
}
