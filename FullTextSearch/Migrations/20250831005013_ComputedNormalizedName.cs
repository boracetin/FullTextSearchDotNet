using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullTextSearch.Migrations
{
    /// <inheritdoc />
    public partial class ComputedNormalizedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop full-text index outside transaction
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.fulltext_indexes WHERE object_id = OBJECT_ID('dbo.Users'))
                    DROP FULLTEXT INDEX ON dbo.Users;
            ", suppressTransaction: true);

            // Step 2: Drop default constraint, drop column, add computed column inside transaction
            migrationBuilder.Sql(@"
                DECLARE @constraint NVARCHAR(128);
                SELECT @constraint = dc.name FROM sys.default_constraints dc
                INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
                WHERE OBJECT_NAME(dc.parent_object_id) = 'Users' AND c.name = 'NormalizedName';
                IF @constraint IS NOT NULL
                    EXEC('ALTER TABLE [Users] DROP CONSTRAINT [' + @constraint + ']');
                ALTER TABLE [Users] DROP COLUMN [NormalizedName];
                ALTER TABLE [Users] ADD [NormalizedName] AS (
                    UPPER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([Name], 'ç', 'c'), 'Ç', 'C'), 'ş', 's'), 'Ş', 'S'), 'ğ', 'g'), 'Ğ', 'G'), 'ü', 'u'), 'Ü', 'U'), 'ö', 'o'), 'Ö', 'O'), 'ı', 'i'), 'İ', 'I'), 'â', 'a'), 'î', 'i'))
                ) PERSISTED;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE [Users] DROP COLUMN [NormalizedName];
                ALTER TABLE [Users] ADD [NormalizedName] NVARCHAR(MAX) NOT NULL DEFAULT '';
            ");
        }
    }
}
