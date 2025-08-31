using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullTextSearch.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTextCatalogAndIndexManual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'FT_INDEX_UserCatalog')
            BEGIN
                CREATE FULLTEXT CATALOG FT_INDEX_UserCatalog AS DEFAULT;
            END
        ", suppressTransaction: true);

        migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.fulltext_indexes WHERE object_id = OBJECT_ID('dbo.Users'))
            BEGIN
                CREATE FULLTEXT INDEX ON dbo.Users(NormalizedName) KEY INDEX PK_Users ON FT_INDEX_UserCatalog;
            END
        ", suppressTransaction: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.fulltext_indexes WHERE object_id = OBJECT_ID('dbo.Users'))
            BEGIN
                DROP FULLTEXT INDEX ON dbo.Users;
            END
        ", suppressTransaction: true);

        migrationBuilder.Sql(@"
            IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = 'FT_INDEX_UserCatalog')
            BEGIN
                DROP FULLTEXT CATALOG FT_INDEX_UserCatalog;
            END
        ", suppressTransaction: true);

        }
    }
}
