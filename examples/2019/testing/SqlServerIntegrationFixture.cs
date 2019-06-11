// A reusable xUnit fixture that starts a real SQL Server container,
// runs DbUp migrations, and provides a connection string to tests.
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DbUp;
using System.Reflection;

public class SqlServerIntegrationFixture : IAsyncLifetime
{
    private IContainer _container = null!;
    public string ConnectionString { get; private set; } = "";

    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SA_PASSWORD", "Test_Pass123!")
            .WithPortBinding(1433, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();

        await _container.StartAsync();

        var port = _container.GetMappedPublicPort(1433);
        ConnectionString = $"Server=localhost,{port};Database=TestDb;" +
                           "User Id=sa;Password=Test_Pass123!;TrustServerCertificate=True";

        // Run all migrations from the assembly's embedded scripts
        EnsureDatabase.For.SqlDatabase(ConnectionString);
        var upgrader = DeployChanges.To
            .SqlDatabase(ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
            throw new InvalidOperationException("Migration failed", result.Error);
    }

    public async Task DisposeAsync() => await _container.DisposeAsync();
}
