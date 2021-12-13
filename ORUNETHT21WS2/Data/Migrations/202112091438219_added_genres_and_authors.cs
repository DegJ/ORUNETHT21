namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_genres_and_authors : DbMigration
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
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Book_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Book_Id);
            
            AddColumn("dbo.Books", "AuthoredBy_Id", c => c.Int(nullable: true));
            CreateIndex("dbo.Books", "AuthoredBy_Id");
            AddForeignKey("dbo.Books", "AuthoredBy_Id", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthoredBy_Id", "dbo.Authors");
            DropForeignKey("dbo.GenreBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.GenreBooks", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreBooks", new[] { "Book_Id" });
            DropIndex("dbo.GenreBooks", new[] { "Genre_Id" });
            DropIndex("dbo.Books", new[] { "AuthoredBy_Id" });
            DropColumn("dbo.Books", "AuthoredBy_Id");
            DropTable("dbo.GenreBooks");
            DropTable("dbo.Genres");
            DropTable("dbo.Authors");
        }
    }
}
