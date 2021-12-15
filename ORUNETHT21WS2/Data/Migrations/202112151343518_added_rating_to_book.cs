namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_rating_to_book : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Rating");
        }
    }
}
