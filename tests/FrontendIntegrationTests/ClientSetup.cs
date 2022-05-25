using System;
using ApplicationCore.Models;
using DataSourceGraphApi.GraphQL.ResolverClients;
using FluentAssertions.Common;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace DataSourceGraphApiIntegrationTests;

public abstract class ClientSetup : Setup
{
    protected ClientSetup(Func<IServiceCollection, IServiceCollection> collection) : base(collection)
    {
        var mock = new Mock<DataSource>();
    }
}