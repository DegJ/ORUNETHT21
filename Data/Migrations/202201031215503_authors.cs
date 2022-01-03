namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "AuthoredById", c => c.Int());
            CreateIndex("dbo.Books", "AuthoredById");
            AddForeignKey("dbo.Books", "AuthoredById", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthoredById", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthoredById" });
            DropColumn("dbo.Books", "AuthoredById");
            DropTable("dbo.Authors");
        }
    }
}
