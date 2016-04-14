using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.DataEngine.BLL.Initialization;

using Sobits.Story.Logic.Repo.Impl;
using Sobits.Story.Logic.BLL.Initialization;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Repository factory
    /// </summary>
    public class RepoFactory : AbstractRepoFactory
    {
        #region singleton

        /// <summary>
        /// Instance of Repository Factory. Singleton.
        /// </summary>
        public static RepoFactory Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new RepoFactory();
                    }

                    return _instance;
                }
            }
        }

        private static RepoFactory _instance;
        private static readonly Object _locker = new Object();

        #endregion singleton

        #region constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        private RepoFactory()
            : base(SessionFactory.Instance)
        {
        }

        #endregion constructors

        #region protected methods

        /// <summary>
        /// Filling repository map
        /// </summary>
        protected override void FillMap()
        {
            RepoMap[typeof(IUserRepo)] = typeof(UserRepo);
            RepoMap[typeof(IRoleRepo)] = typeof(RoleRepo);
            RepoMap[typeof(IUserRoleRepo)] = typeof(UserRoleRepo);
            RepoMap[typeof(IPermissionRepo)] = typeof(PermissionRepo);
            RepoMap[typeof(IPermissionRoleRepo)] = typeof(PermissionRoleRepo);
            RepoMap[typeof(IQuestionRepo)] = typeof(QuestionRepo);
            RepoMap[typeof(IAnswerRepo)] = typeof(AnswerRepo);
            RepoMap[typeof(IAnswerNextQuestionRepo)] = typeof(AnswerNextQuestionRepo);
            RepoMap[typeof(IListAnswerRepo)] = typeof(ListAnswerRepo);
            RepoMap[typeof(ICharterRepo)] = typeof(CharterRepo);
            RepoMap[typeof(IVotingRepo)] = typeof(VotingRepo);
            RepoMap[typeof(IVotingQuestionRepo)] = typeof(VotingQuestionRepo);
            RepoMap[typeof(ITempVotingQuestionRepo)] = typeof(TempVotingQuestionRepo);
            RepoMap[typeof(IIPAddressRepo)] = typeof(IPAddressRepo);
            RepoMap[typeof(IBFileRepo)] = typeof(BFileRepo);
            RepoMap[typeof(IMOOrganizationRepo)] = typeof(MOOrganizationRepo);
            RepoMap[typeof(ISMOOrganizationRepo)] = typeof(SMOOrganizationRepo);

            RepoMap[typeof(IComplaintRepo)] = typeof(ComplaintRepo);
            RepoMap[typeof(ITypeCategoryComplaintRepo)] = typeof(TypeCategoryComplaintRepo);
            RepoMap[typeof(ITypeDealComplaintRepo)] = typeof(TypeDealComplaintRepo);
            RepoMap[typeof(ITypeStateComplaintRepo)] = typeof(TypeStateComplaintRepo);
        }

        /// <summary>
        /// Data initialization providers
        /// </summary>
        /// <returns></returns>
        protected override IDataInitProvider[] GetInitProviders(DataEngine.IDataSession session)
        {
            List<IDataInitProvider> result = new List<IDataInitProvider>(base.GetInitProviders(session));
            IDataSession dataSession = (IDataSession)session;

            result.Add(new RoleInitProvider(this, dataSession));
            result.Add(new UserInitProvider(this, dataSession));
            result.Add(new TagTypeInitProvider(this, dataSession));

            return result.ToArray();
        }

        #endregion protected methods
    }
}
