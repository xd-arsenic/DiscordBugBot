﻿// <auto-generated />
using System;
using DiscordBugBot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiscordBugBot.Migrations
{
    [DbContext(typeof(BugBotDataContext))]
    partial class BugBotDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("DiscordBugBot.Models.GuildApprovedIssueChannel", b =>
                {
                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChannelId");

                    b.HasIndex("GuildId");

                    b.ToTable("IssueChannels");
                });

            modelBuilder.Entity("DiscordBugBot.Models.GuildOptions", b =>
                {
                    b.Property<ulong>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GithubRepository")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("LoggingChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinApprovalVotes")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ModeratorRoleId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong?>("TrackerChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("VoterRoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GuildOptions");
                });

            modelBuilder.Entity("DiscordBugBot.Models.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("Assignee")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("Author")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GithubIssueNumber")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastUpdatedTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("LogMessageId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GuildId", "Number")
                        .IsUnique();

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("DiscordBugBot.Models.IssueCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Archived")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmojiIcon")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("NextNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prefix")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GuildId", "Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DiscordBugBot.Models.Proposal", b =>
                {
                    b.Property<ulong>("MessageId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ApprovalVotes")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("MessageId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("DiscordBugBot.Models.GuildApprovedIssueChannel", b =>
                {
                    b.HasOne("DiscordBugBot.Models.GuildOptions", "Guild")
                        .WithMany("AllowedChannels")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordBugBot.Models.Issue", b =>
                {
                    b.HasOne("DiscordBugBot.Models.IssueCategory", "Category")
                        .WithMany("Issues")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordBugBot.Models.IssueCategory", b =>
                {
                    b.HasOne("DiscordBugBot.Models.GuildOptions", "Guild")
                        .WithMany("IssueCategories")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordBugBot.Models.Proposal", b =>
                {
                    b.HasOne("DiscordBugBot.Models.IssueCategory", "Category")
                        .WithMany("Proposals")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}