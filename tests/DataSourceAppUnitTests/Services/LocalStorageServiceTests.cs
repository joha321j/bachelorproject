using System;
using System.Text.Json;
using System.Threading.Tasks;
using DataSourceApp.Services;
using FluentAssertions;
using Xunit;
using Bunit;
using DataSourceApp.Exceptions;

namespace DataSourceAppUnitTests.Services;

public class LocalStorageServiceTests : IDisposable
{
    private readonly LocalStorageService _sut;
    private readonly TestContext _context;

    protected LocalStorageServiceTests()
    {
        _context = new TestContext();

        _sut = new LocalStorageService(_context.JSInterop.JSRuntime);

    }

    public class GetItem : LocalStorageServiceTests
    {
        
        [Fact]
        public async Task ReturnsValue_GivenKey()
        {
            _context.JSInterop.Setup<string>("localStorage.getItem", "jwtToken")
                .SetResult(JsonSerializer.Serialize(new { jwtToken = "FAKE-JWT-TOKEN" }));
        
            var result = await _sut.GetItem<string>("jwtToken");
        
            result.Should().Be("FAKE-JWT-TOKEN");
        }
    
        [Fact]
        public async Task Invokes_JSRuntime()
        {
            var getItemInvocationHandler = _context.JSInterop.Setup<string>("localStorage.getItem", "jwtToken")
                .SetResult(JsonSerializer.Serialize(new { jwtToken = ""}));
        
            await _sut.GetItem<string>("jwtToken");

            getItemInvocationHandler.VerifyInvoke("localStorage.getItem", 1);
        }

        public class ThrowsException : GetItem
        {
            [Fact]
            public async Task When_HasNoValue()
            {
                _context.JSInterop.Setup<string>("localStorage.getItem", _ => true)
                    .SetResult(JsonSerializer.Serialize(new { jwtToken = ""}));
        
                Func<Task> act = async() => await _sut.GetItem<string>("notExisting");

                await act
                    .Should()
                    .ThrowAsync<LocalStorageItemNotFoundException>();
            }

            [Fact]
            public async Task When_JsonHasNoValue()
            {
                _context.JSInterop.Setup<string>("localStorage.getItem", _ => true)
                    .SetResult(null!);
        
                Func<Task> act = async() => await _sut.GetItem<string>("notExisting");

                await act
                    .Should()
                    .ThrowAsync<LocalStorageHasNoItems>();
            }
        }
    }

    public class SetItem : LocalStorageServiceTests
    {
        [Fact]
        public async Task Invokes_JSRuntime()
        {
            var setItemInvocationHandler = _context.JSInterop.SetupVoid("localStorage.setItem", "TestKey", "\"TestValue\"")
                .SetVoidResult();
        
            await _sut.SetItem("TestKey", "TestValue");
        
            setItemInvocationHandler.VerifyInvoke("localStorage.setItem", 1);
        }
    }

    public class RemoveItem : LocalStorageServiceTests
    {
        [Fact]
        public async Task Invokes_JSRuntime()
        {
            var removeItemInvocationHandler = _context.JSInterop.SetupVoid("localStorage.removeItem", "TestKeyToRemove")
                .SetVoidResult();
        
            await _sut.RemoveItem("TestKeyToRemove");
        
            removeItemInvocationHandler.VerifyInvoke("localStorage.removeItem", 1);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}