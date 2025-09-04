using Microsoft.EntityFrameworkCore.Migrations;

namespace PromptTesting.Infrastructure.Persistence.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Prompts",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: false, defaultValue: ""),
                Version = table.Column<string>(nullable: false, defaultValue: "1"),
                DataPoint = table.Column<string>(nullable: false, defaultValue: ""),
                Status = table.Column<int>(nullable: false),
                BaseContext = table.Column<string>(nullable: false, defaultValue: ""),
                LastAccuracy = table.Column<decimal>(type:"decimal(5,2)", nullable: true),
                LastRunAt = table.Column<DateTime>(nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Prompts", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "TestRuns",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                PromptId = table.Column<Guid>(nullable: false),
                ScopeTeam = table.Column<string>(nullable: false),
                ScopeBrokingSegment = table.Column<string>(nullable: false),
                ScopeGlobalLineOfBusiness = table.Column<string>(nullable: false),
                ScopeProduct = table.Column<string>(nullable: false),
                Status = table.Column<int>(nullable: false),
                Accuracy = table.Column<decimal>(type:"decimal(5,2)", nullable: true),
                StartedAt = table.Column<DateTime>(nullable: false),
                CompletedAt = table.Column<DateTime>(nullable: true),
                FailureReason = table.Column<string>(nullable: true),
                UserId = table.Column<string>(nullable: false),
                ContextSnapshot = table.Column<string>(nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_TestRuns", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "TestResultMetrics",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                TestRunId = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: false),
                Value = table.Column<decimal>(type:"decimal(8,4)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_TestResultMetrics", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "ScopeValidationRules",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Team = table.Column<string>(nullable: false),
                BrokingSegment = table.Column<string>(nullable: false),
                GlobalLineOfBusiness = table.Column<string>(nullable: false),
                Product = table.Column<string>(nullable: false),
                IsValid = table.Column<bool>(nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ScopeValidationRules", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "TestResultMetrics");
        migrationBuilder.DropTable(name: "TestRuns");
        migrationBuilder.DropTable(name: "Prompts");
        migrationBuilder.DropTable(name: "ScopeValidationRules");
    }
}