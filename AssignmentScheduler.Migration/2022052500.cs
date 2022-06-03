using FluentMigrator;

namespace AssignmentScheduler.Migrations;

[Migration(2022052500)]
public class _2022052500 : Migration
{
    public override void Up()
    {
        if (!Schema.Table("User").Exists())
            Create.Table("User").
                WithColumn("Id").AsGuid().PrimaryKey().WithDefaultValue(Guid.NewGuid()).
                WithColumn("Email").AsString().
                WithColumn("Password").AsString().
                WithColumn("CreatedDate").AsDateTime2();
    }

    public override void Down()
    {
        if (Schema.Table("User").Exists())
            Delete.Table("User");
    }
}
