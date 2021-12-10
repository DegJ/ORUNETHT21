namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expose_authoredbyid_inmodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "AuthoredBy_Id", newName: "AuthoredById");
            RenameIndex(table: "dbo.Books", name: "IX_AuthoredBy_Id", newName: "IX_AuthoredById");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Books", name: "IX_AuthoredById", newName: "IX_AuthoredBy_Id");
            RenameColumn(table: "dbo.Books", name: "AuthoredById", newName: "AuthoredBy_Id");
        }
    }
}
