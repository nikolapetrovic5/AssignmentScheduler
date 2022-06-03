using FluentMigrator;

namespace AssignmentScheduler.Migrations;

[Migration(2022052501)]
public class _2022052501 : Migration
{
    public override void Up()
    {
        if (!Schema.Table("Assignment").Exists())
            Create.Table("Assignment").
                WithColumn("Id").AsGuid().PrimaryKey().WithDefaultValue(Guid.NewGuid()).
                WithColumn("Title").AsString().
                WithColumn("Description").AsString().
                WithColumn("CreatedDate").AsDateTime2().
                WithColumn("StartDate").AsDateTime2().
                WithColumn("EndDate").AsDateTime2().
                WithColumn("UserId").AsGuid().
                WithColumn("Status").AsString();
    }

    public override void Down()
    {
        if (Schema.Table("Assignment").Exists())
            Delete.Table("Assignment");
    }
}
