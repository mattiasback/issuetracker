using System;
using System.Collections.Generic;

namespace IssueTracker.Core.Services.IssueService
{
    public interface IIssueService
    {
        /// <summary>
        /// Add an issue with the given title.
        /// </summary>
        /// <param name="title">Title of the issue.</param>
        /// <returns>A unique identifier for the added issue.</returns>
        Guid AddIssue(string title);

        /// <summary>
        /// Remove an issue completely.
        /// </summary>
        /// <param name="issueId">Unique identifier of the issue.</param>
        void RemoveIssue(Guid issueId);

        /// <summary>
        /// Set the state of an issue, with an optional comment.
        /// </summary>
        /// <param name="issueId">Unique identifier of the issue.</param>
        /// <param name="state">State which the issue shall transition to.</param>
        /// <param name="comment">Optional comment.</param>
        void SetIssueState(Guid issueId, IssueStateDto state, string comment = null);

        /// <summary>
        /// Assign a user to an issue.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <param name="issueId">Unique identifier of the issue.</param>
        void AssignUser(Guid userId, Guid issueId);

        /// <summary>
        /// Add a comment to an issue.
        /// </summary>
        /// <param name="issueId">Unique identifier of the issue.</param>
        /// <param name="comment">The comment to be added.</param>
        void AddIssueComment(Guid issueId, string comment);

        /// <summary>
        /// Retrieve a collection of issues, optionally filtered by a set of parameters.
        /// If all filter parameters are null, all issues will be returned.
        /// </summary>
        /// <param name="state">Optional state filter.</param>
        /// <param name="userId">Optional user ID filter.</param>
        /// <param name="startDate">Optional start date filter.</param>
        /// <param name="endDate">Optional end date filter.</param>
        /// <returns>Returns a list of issues based on the selected filtering.
        /// An empty list is returned if no issues found.</returns>
        IList<IssueDto> GetIssues(IssueStateDto? state = null, Guid? userId = null, 
            DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Get a specific issue matching the unique identifier provided.
        /// </summary>
        /// <param name="issueId">Unique identifier of the issue.</param>
        /// <returns>Returns the specified issue.</returns>
        IssueDto GetIssue(Guid issueId);
    }
}
