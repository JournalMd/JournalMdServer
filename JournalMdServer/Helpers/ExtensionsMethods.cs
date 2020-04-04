using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Helpers
{
    public static class ExtensionsMethods
    {
        // Get JWT "Username" (=id) from context
        public static long GetAuthenticatedId(this ControllerBase controllerBase)
        {
            if (!controllerBase.User.Identity.IsAuthenticated)
                throw new AppException("User is not authenticated!");

            string username = controllerBase.User.Identity.Name;
            if (!long.TryParse(username, out long id))
                throw new AppException("Could not resolve " + username + " to id");

            return id;
        }
    }
}
