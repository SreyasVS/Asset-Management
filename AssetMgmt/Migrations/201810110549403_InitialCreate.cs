namespace AssetMgmt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetManagement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        AssetID = c.Int(nullable: false),
                        modelNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Asset", t => t.AssetID, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.AssetID);
            
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AssetName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        DesignationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Designation", t => t.DesignationID, cascadeDelete: true)
                .Index(t => t.DesignationID);
            
            CreateTable(
                "dbo.Designation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DesignationID", "dbo.Designation");
            DropForeignKey("dbo.AssetManagement", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.AssetManagement", "AssetID", "dbo.Asset");
            DropIndex("dbo.Employee", new[] { "DesignationID" });
            DropIndex("dbo.AssetManagement", new[] { "AssetID" });
            DropIndex("dbo.AssetManagement", new[] { "EmployeeID" });
            DropTable("dbo.Designation");
            DropTable("dbo.Employee");
            DropTable("dbo.Asset");
            DropTable("dbo.AssetManagement");
        }
    }
}
