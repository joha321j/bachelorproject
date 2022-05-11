using System.Threading.Tasks;
using Bunit;
using Xunit;

namespace DataSourceAppUnitTests.Services.LocalStorageServiceTests;

public class RemoveItem : LocalStorageServiceTestsBase
{
    [Fact]
    public async Task Invokes_JSRuntime()
    {
        var removeItemInvocationHandler = Context.JSInterop.SetupVoid("localStorage.removeItem", "TestKeyToRemove")
            .SetVoidResult();
        
        await Sut.RemoveItem("TestKeyToRemove");
        
        removeItemInvocationHandler.VerifyInvoke("localStorage.removeItem", 1);
    }
}