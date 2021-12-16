namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "UserIdThatCreated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "UserIdThatCreated");
        }
    }
}
