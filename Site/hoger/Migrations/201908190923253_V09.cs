namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V09 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "ParentId", newName: "Product_Id");
            RenameIndex(table: "dbo.Products", name: "IX_ParentId", newName: "IX_Product_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_Product_Id", newName: "IX_ParentId");
            RenameColumn(table: "dbo.Products", name: "Product_Id", newName: "ParentId");
        }
    }
}
