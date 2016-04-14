using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Sobits.Story.Logic.Repo.Impl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.ByteCode.Castle;

namespace Sobits.Story.Logic.Repo
{
    public class RepoFactory
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

        public RepoFactory()
        {
            FillMap();
        }

        #region public methods

        /// <summary>
        /// Create new instance of repository by its interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetRepo<T>()
            where T : IRepo
        {
            Type type = typeof(T);

            if (RepoMap.ContainsKey(type))
            {
                return (T)Activator.CreateInstance(RepoMap[type], new Object[] { GetSession() });
            }
            else
            {
                throw new ArgumentException(String.Format("Interface {0} was not found in RepoMap", type.FullName));
            }
        }
        #endregion public methods

        #region protected properties

        /// <summary>
        /// Map of repository interfaces and implementations
        /// </summary>
        protected virtual Dictionary<Type, Type> RepoMap
        {
            get
            {
                return _repoMap;
            }
        }

        #endregion protected properties

        #region private fields

        private readonly Dictionary<Type, Type> _repoMap = new Dictionary<Type, Type>();
        private static ISessionFactory SessionFactory = CreateSessionFactory();

        #endregion private fields

        #region private methods

        /// <summary>
        /// Filling repository map
        /// </summary>
        private void FillMap()
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

        private ISession GetSession()
        {
            //здесь мы получим объект ISession
            //throw new NotImplementedException("!");
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            //string connStrHome = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=46.0.13.2)(PORT=1578))(CONNECT_DATA=(SID=orcl)));User Id=dbq;Password=dbq";

            FluentConfiguration config = Fluently.Configure()
             .Database(OracleDataClientConfiguration.Oracle10
                                                    .ConnectionString(c => c.FromConnectionStringWithKey(LOCAL_CONNECTION_STRING))
                                                    //.ConnectionString(connStrHome)
                                                    .ShowSql())
             .Mappings(m =>
             {
                 m.FluentMappings.Conventions.Setup(x => x.Add(AutoImport.Never()));
                 m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
             });

             var sessionFactory = config.BuildSessionFactory();

             //SchemaUpdate update = new SchemaUpdate(config.BuildConfiguration());
             //update.Execute(true, true);

             return sessionFactory;
        }

        #endregion

        #region private fields

        private const String LOCAL_CONNECTION_STRING = "LocalDBConnectionString";

        #endregion
    }
}
