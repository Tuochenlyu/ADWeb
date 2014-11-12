﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Configuration;

namespace ADWeb.Domain.ActiveDirectory
{
    /// <summary>
    /// Fields that can be used when searching for users. 
    /// </summary>
    public enum SearchField
    {
        DisplayName = 1,
        FirstName,
        LastName,
        Department
    }

    /// <summary>
    /// This class will contain the methods that can be used to create and edit
    /// objects in active directory.
    /// </summary>
    public class ADDomain
    {
        public string ServerName { get; set; }
        public string ServiceUser { get; set; }
        public string ServicePassword { get; set; }

        public ADDomain()
        {
            ServerName = WebConfigurationManager.AppSettings["server_name"];
            ServiceUser = WebConfigurationManager.AppSettings["service_user"];
            ServicePassword = WebConfigurationManager.AppSettings["service_password"];
        }

        public ADUser GetUserByID(string userId)
        {
            using(PrincipalContext context = new PrincipalContext(ContextType.Domain, ServerName, null, ContextOptions.Negotiate, ServiceUser, ServicePassword))
            {
                ADUser user = ADUser.FindByIdentity(context, userId);
                //user.GetUserGroups();
                return user;
            }
        }

        public void UpdateUser(ADUser updatedUser)
        {
            using(PrincipalContext context = new PrincipalContext(ContextType.Domain, ServerName, null, ContextOptions.Negotiate, ServiceUser, ServicePassword))
            {
                using(ADUser user = ADUser.FindByIdentity(context, updatedUser.SamAccountName))
                {
                    if(user != null)
                    {
                        user.GivenName = updatedUser.GivenName;
                        user.Surname = updatedUser.Surname;
                        user.DisplayName = updatedUser.DisplayName;

                        user.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Searches users using the display name value of all users in 
        /// the domain.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<ADUser> QuickSearch(string searchString)
        {
            List<ADUser> users = new List<ADUser>();

            using(PrincipalContext context = new PrincipalContext(ContextType.Domain, ServerName, null, ContextOptions.Negotiate, ServiceUser, ServicePassword))
            {
                ADUser userFilter = new ADUser(context)
                {
                    DisplayName = "*" + searchString + "*"
                };

                using(PrincipalSearcher searcher = new PrincipalSearcher(userFilter))
                {
                    ((DirectorySearcher)searcher.GetUnderlyingSearcher()).PageSize = 1000;
                    var searchResults = searcher.FindAll().ToList();

                    foreach(Principal user in searchResults)
                    {
                        ADUser usr = user as ADUser;
                        users.Add(usr);
                    }
                }
            }

            return users.OrderBy(u => u.Surname).ToList();
        }

        /// <summary>
        /// Gets all the users in the domain.
        /// </summary>
        /// <returns></returns>
        public List<ADUser> GetAllUsers()
        {
            List<ADUser> users = new List<ADUser>();
            using(PrincipalContext context = new PrincipalContext(ContextType.Domain, ServerName, null, ContextOptions.Negotiate, ServiceUser, ServicePassword))
            {
                ADUser userFilter = new ADUser(context);

                using(PrincipalSearcher searcher = new PrincipalSearcher(userFilter))
                {
                    ((DirectorySearcher)searcher.GetUnderlyingSearcher()).PageSize = 1000;
                    var searchResults = searcher.FindAll().ToList();

                    foreach(Principal user in searchResults)
                    {
                        ADUser usr = user as ADUser;
                        users.Add(usr);
                    }
                }
            }

            return users;
        }

    }
}
