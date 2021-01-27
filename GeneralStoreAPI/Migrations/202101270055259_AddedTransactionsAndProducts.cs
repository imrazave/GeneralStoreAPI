namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionsAndProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        SKU = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Cost = c.Double(nullable: false),
                        NumberInInventory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SKU);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        ProductSKU = c.String(maxLength: 128),
                        ItemCount = c.Int(nullable: false),
                        DateOfTransaction = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductSKU)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductSKU);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ProductSKU", "dbo.Products");
            DropForeignKey("dbo.Transactions", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "ProductSKU" });
            DropIndex("dbo.Transactions", new[] { "CustomerID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Products");
        }
    }
}
