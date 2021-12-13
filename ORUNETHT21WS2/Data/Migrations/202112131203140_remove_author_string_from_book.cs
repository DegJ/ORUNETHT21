namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_author_string_from_book : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Author", c => c.String());
        }
    }
}
