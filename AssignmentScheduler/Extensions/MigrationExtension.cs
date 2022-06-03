using FluentMigrator.Runner;

namespace AssignmentScheduler.Api.Extensions;

public static class MigrationExtension
{
    public static IApplicationBuilder MigrateUp(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        if (runner == null)
            return app;

        try
        {
            runner.ListMigrations();
            runner.MigrateUp();
        }
        catch(Exception)
        {
        }

        return app;
    }
}
