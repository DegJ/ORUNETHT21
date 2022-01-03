namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_user_to_book : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "SavedByUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Books", "SavedByUserId");
            AddForeignKey("dbo.Books", "SavedByUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "SavedByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "SavedByUserId" });
            DropColumn("dbo.Books", "SavedByUserId");
        }
    }
}
