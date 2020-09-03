﻿using DiscordBugBot.Models;
using System.Collections.Generic;

namespace DiscordBugBot.Data
{
    public interface IDataStore
    {
        void CreateIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void CreateCategory(IssueCategory category);
        void UpdateCategory(IssueCategory category);
        Issue GetIssueById(long id);
        Issue GetIssueByCategory(IssueCategory category, int number);
        IEnumerable<Issue> GetIssues(ulong guild, IssueCategory category = null, IssueStatus? status = null, ulong? assignee = null, ulong? author = null);
        IEnumerable<Issue> GetOpenIssues(ulong guild, IssueCategory category = null, ulong? assignee = null, ulong? author = null);
        IssueCategory GetCategory(ulong guild, string name);
        IEnumerable<IssueCategory> GetCategories(ulong guild);
        GuildOptions GetOptions(ulong guild);
        void CreateOptions(GuildOptions options);
        void UpdateOptions(GuildOptions options);
        IEnumerable<Proposal> GetProposals(ulong guild, ulong channel, ulong message);
        void CreateProposal(Proposal proposal);
        void UpdateProposals(IEnumerable<Proposal> proposals);
    }
}