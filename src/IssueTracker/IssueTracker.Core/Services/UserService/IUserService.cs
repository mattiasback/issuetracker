using System;
using System.Collections.Generic;

namespace IssueTracker.Core.Services.UserService
{
    interface IUserService
    {
        /// <summary>
        /// Add a user by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns a unique identifier for the created user.</returns>
        Guid AddUser(string name);

        /// <summary>
        /// Remove a user completely and remove from any issues this user is currently assigned to.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        void RemoveUser(Guid userId);

        /// <summary>
        /// Retrieve a collection of all added users in the system.
        /// </summary>
        /// <returns>A collection of all users.
        /// An empty list is returned if no users found.</returns>
        IList<UserDto> GetUsers();

        /// <summary>
        /// Get a specific user matching the unique identifier provided.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns the specified user.</returns>
        UserDto GetUser(Guid userId);
    }
}
