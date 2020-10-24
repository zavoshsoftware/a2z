namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V08 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Product_Id", newName: "ParentId");
            RenameIndex(table: "dbo.Products", name: "IX_Product_Id", newName: "IX_ParentId");
            AddColumn("dbo.Products", "HasExtra", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "HasExtra");
            RenameIndex(table: "dbo.Products", name: "IX_ParentId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.Products", name: "ParentId", newName: "Product_Id");
        }
    }
}
