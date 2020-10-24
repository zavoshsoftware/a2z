namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextTypes", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextTypes", "UrlParam");
        }
    }
}
