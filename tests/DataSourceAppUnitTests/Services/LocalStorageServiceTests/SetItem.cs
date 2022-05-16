using System.Threading.Tasks;
using Bunit;
using Xunit;

namespace DataSourceAppUnitTests.Services.LocalStorageServiceTests;

public class SetItem : LocalStorageServiceTestsBase
{
    [Fact]
    public async Task Invokes_JSRuntime()
    {
        var setItemInvocationHandler = Context.JSInterop.SetupVoid("localStorage.setItem", "TestKey", "\"TestValue\"")
            .SetVoidResult();
        
        await Sut.SetItem("TestKey", "TestValue");
        
        setItemInvocationHandler.VerifyInvoke("localStorage.setItem", 1);
    }
}