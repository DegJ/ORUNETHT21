namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_rating_to_book : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Rating");
        }
    }
}
