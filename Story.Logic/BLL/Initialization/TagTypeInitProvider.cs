using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.BLL.Initialization;
using Sobits.Story.Logic.Repo;

namespace Sobits.Story.Logic.BLL.Initialization
{
    /// <summary>
    /// Data initialization provider for TagType
    /// </summary>
    internal class TagTypeInitProvider : AbstractDataInitProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagTypeInitProvider"/> class.
        /// </summary>
        /// <param name="repoFactory">The repo factory.</param>
        /// <param name="session">The session.</param>
        public TagTypeInitProvider(RepoFactory repoFactory, IDataSession session)
            : base(repoFactory, session)
        { }

        /// <summary>
        /// Initialize database (create common Tag Types with null Company)
        /// </summary>
        public override void Init()
        {
            //var repo = GetRepo<ITagTypeRepo>();
            //IEnumerable<TagType> tagTypes = repo.GetAll().ToArray();

            //if (tagTypes.Where(x => x.Company == null && x.Name == "Category").Count() == 0)
            //{
            //    TagType tagType = new TagType("Category", true, true, true);
            //    repo.Save(tagType);
            //}

            //if (tagTypes.Where(x => x.Company == null && x.Name == "Country").Count() == 0)
            //{
            //    TagType tagType = new TagType("Country", true, true, true);
            //    repo.Save(tagType);
            //}

            //if (tagTypes.Where(x => x.Company == null && x.Name == "Location").Count() == 0)
            //{
            //    TagType tagType = new TagType("Location", true, true, true);
            //    repo.Save(tagType);
            //}

            //if (tagTypes.Where(x => x.Company == null && x.Name == "ContentType").Count() == 0)
            //{
            //    TagType tagType = new TagType("ContentType", true, false, true);
            //    repo.Save(tagType);
            //}

        }
    }
}
