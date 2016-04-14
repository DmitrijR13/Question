using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.BLL.Initialization;
using Sobits.Story.Logic.Repo;

namespace Sobits.Story.Logic.BLL.Initialization
{

    /// <summary>
    /// Data initialization provider for User
    /// </summary>
    public class UserInitProvider : AbstractDataInitProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInitProvider"/> class.
        /// </summary>
        /// <param name="repoFactory">The repo factory.</param>
        /// <param name="session">The session.</param>
        public UserInitProvider(RepoFactory repoFactory, IDataSession session)
            : base(repoFactory, session)
        { }

        /// <summary>
        /// Initialize database 
        /// </summary>
        public override void Init()
        {
            //var repo = GetRepo<IUserRepo>();

            //if (repo.GetByLogin("Admin") == null)
            //{
            //    Role role = GetRepo<IRoleRepo>().GetAll().Where(x => x.Name == "admin").FirstOrDefault();
            //    User user = new User(role, "admin", "admin", "admin", "admin@NextGenJobPost.com", false);
            //    repo.Save(user);
            //}
        }
    }
}
