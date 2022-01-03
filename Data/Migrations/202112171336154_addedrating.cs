namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RatingForUser", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RatingForUser");
        }
    }
}
