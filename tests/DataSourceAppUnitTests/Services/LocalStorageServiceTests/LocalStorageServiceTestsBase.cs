using System;
using Bunit;
using DataSourceApp.Services;

namespace DataSourceAppUnitTests.Services.LocalStorageServiceTests;

public abstract class LocalStorageServiceTestsBase : IDisposable
{
    protected readonly LocalStorageService Sut;
    protected readonly TestContext Context;

    protected LocalStorageServiceTestsBase()
    {
        Context = new TestContext();

        Sut = new LocalStorageService(Context.JSInterop.JSRuntime);

    }

    public void Dispose()
    {
        Context.Dispose();
    }
}