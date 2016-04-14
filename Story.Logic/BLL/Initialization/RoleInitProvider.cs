using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.BLL.Initialization;
using Sobits.Story.Logic.Repo;

namespace Sobits.Story.Logic.BLL.Initialization
{
    /// <summary>
    /// Data initialization provider for Roles
    /// </summary>
    internal class RoleInitProvider : AbstractDataInitProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleInitProvider"/> class.
        /// </summary>
        /// <param name="repoFactory">The repo factory.</param>
        /// <param name="session">The session.</param>
        public RoleInitProvider(RepoFactory repoFactory, IDataSession session)
            : base(repoFactory, session)
        { }

        ///// <summary>
        ///// Initialize database
        ///// </summary>
        public override void Init()
        {
            //var repo = GetRepo<IRoleRepo>();
            //IEnumerable<Role> roles = repo.GetAll().ToArray();

            //if (roles.Where(x => x.Handle == "Admin").Count() == 0)
            //{
            //    Role role = new Role("Admin", "Admin");
            //    repo.Save(role);
            //}

            //if (roles.Where(x => x.Handle == "CompanyUser").Count() == 0)
            //{
            //    Role role = new Role("CompanyUser", "CompanyUser");
            //    repo.Save(role);
            //}
        }
    }
}
