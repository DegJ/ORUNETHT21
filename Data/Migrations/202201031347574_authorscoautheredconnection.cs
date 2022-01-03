namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authorscoautheredconnection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorBookCoAuthoreds",
                c => new
                    {
                        AuthorId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.AuthorId, t.BookId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorBookCoAuthoreds", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.AuthorBookCoAuthoreds", "BookId", "dbo.Books");
            DropIndex("dbo.AuthorBookCoAuthoreds", new[] { "BookId" });
            DropIndex("dbo.AuthorBookCoAuthoreds", new[] { "AuthorId" });
            DropTable("dbo.AuthorBookCoAuthoreds");
        }
    }
}
