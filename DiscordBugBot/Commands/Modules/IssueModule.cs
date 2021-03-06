﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBugBot.Data;
using DiscordBugBot.Helpers;
using DiscordBugBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBugBot.Commands.Modules
{
    [Group("issue")]
    public class IssueModule : ModuleBase<SocketCommandContext>
    {
        public BugBotDataContext DataStore { get; set; }
        public IssueHelper IssueHelper { get; set; }

        [VoterRequired]
        [Command]
        [Summary("Gets an issue")]
        public async Task Get(string number)
        {
            Issue issue = await ((IQueryable<Issue>)DataStore.Issues).SingleOrDefaultAsync(i => i.GuildId == Context.Guild.Id && i.Number == number);
            if (issue is null)
            {
                throw new CommandExecutionException("That issue does not exist");
            }
            await Context.Channel.SendMessageAsync(embed: IssueHelper.GenerateInlineIssueEmbed(issue));
        }

        [ModeratorRequired]
        [Command("create")]
        [Summary("Create an issue")]
        public async Task Create(string category, string title, [Remainder] string description)
        {
            GuildOptions options = await DataStore.GuildOptions.Include(o => o.IssueCategories).SingleOrDefaultAsync(o => o.Id == Context.Guild.Id);

            if (options is null)
            {
                throw new CommandExecutionException("Please set up the server before trying to create issues. Run the `setup` command to get started.");
            }

            var categoryObj = options.IssueCategories.SingleOrDefault(c => c.Name == category);

            if (categoryObj is null)
            {
                throw new CommandExecutionException("Invalid category name specified.");
            }

            Proposal proposal = new Proposal
            {
                CategoryId = categoryObj.Id,
                ChannelId = Context.Channel.Id,
                GuildId = Context.Guild.Id,
                MessageId = Context.Message.Id,
                Status = ProposalStatus.Approved
            };


            DataStore.Add(proposal);
            await DataStore.SaveChangesAsync();

            await IssueHelper.CreateIssue(proposal, Context.Channel, Context.Message, options, title: title, description: description);
        }

        [ModeratorRequired]
        [Command("update")]
        [Summary("Updates one or more fields on an issue")]
        public async Task Update(string number, IssueUpdateArgs args)
        {
            Issue issue = await ((IQueryable<Issue>)DataStore.Issues).SingleOrDefaultAsync(i => i.GuildId == Context.Guild.Id && i.Number == number);
            if (issue is null)
            {
                throw new CommandExecutionException("That issue does not exist");
            }

            var (category, changed) = await IssueHelper.UpdateIssue(issue, args);
            await Context.Channel.SendMessageAsync(embed: IssueHelper.GenerateInlineIssueEmbed(issue, category: category));
        }
    }
}
