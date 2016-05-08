namespace AutomatedTellerMachine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Transactions", "CheckingAccountId");
            AddForeignKey("dbo.Transactions", "CheckingAccountId", "dbo.CheckingAccounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CheckingAccountId", "dbo.CheckingAccounts");
            DropIndex("dbo.Transactions", new[] { "CheckingAccountId" });
        }
    }
}
