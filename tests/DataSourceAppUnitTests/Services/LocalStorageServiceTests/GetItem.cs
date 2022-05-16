using System;
using System.Text.Json;
using System.Threading.Tasks;
using Bunit;
using DataSourceApp.Exceptions;
using FluentAssertions;
using Xunit;

namespace DataSourceAppUnitTests.Services.LocalStorageServiceTests;

public class GetItem : LocalStorageServiceTestsBase
{
        
    [Fact]
    public async Task ReturnsValue_GivenKey()
    {
        Context.JSInterop.Setup<string>("localStorage.getItem", "jwtToken")
            .SetResult(JsonSerializer.Serialize(new { jwtToken = "FAKE-JWT-TOKEN" }));
        
        var result = await Sut.GetItem<string>("jwtToken");
        
        result.Should().Be("FAKE-JWT-TOKEN");
    }
    
    [Fact]
    public async Task Invokes_JSRuntime()
    {
        var getItemInvocationHandler = Context.JSInterop.Setup<string>("localStorage.getItem", "jwtToken")
            .SetResult(JsonSerializer.Serialize(new { jwtToken = ""}));
        
        await Sut.GetItem<string>("jwtToken");

        getItemInvocationHandler.VerifyInvoke("localStorage.getItem", 1);
    }

    public class ThrowsException : LocalStorageServiceTestsBase
    {
        [Fact]
        public async Task When_HasNoValue()
        {
            Context.JSInterop.Setup<string>("localStorage.getItem", _ => true)
                .SetResult(JsonSerializer.Serialize(new { jwtToken = ""}));
        
            Func<Task> act = async() => await Sut.GetItem<string>("notExisting");

            await act
                .Should()
                .ThrowAsync<LocalStorageItemNotFoundException>();
        }

        [Fact]
        public async Task When_JsonHasNoValue()
        {
            Context.JSInterop.Setup<string>("localStorage.getItem", _ => true)
                .SetResult(null!);
        
            Func<Task> act = async() => await Sut.GetItem<string>("notExisting");

            await act
                .Should()
                .ThrowAsync<LocalStorageHasNoItems>();
        }
    }
}