using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

namespace DataSourceGraphApiTest.GraphQL.ResolverClients.AppInsightsResolverClientTests;

public class ResolveEvents : AppInsightsResolverClientTestsBase
{
    
    [Theory]
    [MemberData(nameof(EventPathTestData))]
    public async Task ConstructCorrectPathWhenGettingEventsResult(
        string eventType,
        string eventId, 
        string path,
        TimeSpan span)
    {
        await ResolverClient.ResolveEvents(AppId, eventType, eventId, span);

        ClientNameCaptor.Should().BeEquivalentTo(ClientName);
        RequestCaptor.RequestUri.Should().NotBeNull();
        RequestCaptor.RequestUri!.PathAndQuery.Should().BeEquivalentTo(BasePath + path);
    }

    private static IEnumerable<object[]> EventPathTestData()
    {
        const string eventType = "AnEventType";
        const string eventId = "AnEventId";
        yield return new object[]
        {
            eventType,
            eventId,
            $"{AppId}/events/{eventType}/{eventId}",
            null!
        };

        const string secondEventType = "ADifferentEventType";
        const string secondEventId = "BestEventId";
        var timeSpan = TimeSpan.MaxValue;

        yield return new object[]
        {
            secondEventType,
            secondEventId,
            $"{AppId}/events/{secondEventType}/{secondEventId}?timespan=" +
            $"P{timeSpan.Days}DT" +
            $"{timeSpan.Hours}H" +
            $"{timeSpan.Minutes}M" +
            $"{timeSpan.Seconds}." +
            $"{timeSpan.Milliseconds}" +
            $"{timeSpan.Microseconds()}" +
            $"{timeSpan.Nanoseconds() / 100}S",
            timeSpan
        };
    }
}