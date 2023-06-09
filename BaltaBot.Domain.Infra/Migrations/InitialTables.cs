﻿using FluentMigrator;

namespace BaltaBot.Domain.Infra.Migrations
{
    [Migration(1)]
    public class InitialTables : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("Person")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("DiscordId").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();

            Create.Index()
                .OnTable("Person")
                .OnColumn("DiscordId");

            Create.Table("Premium")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("PersonId").AsString().NotNullable()
                .WithColumn("StartedAt").AsDateTime().NotNullable()
                .WithColumn("ClosedAt").AsDateTime().NotNullable();

            Create.Index()
                .OnTable("Premium")
                .OnColumn("PersonId");

            Create.Index()
               .OnTable("Premium")
               .OnColumn("ClosedAt");
        }
    }
}
