using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Principal;
using System.Security.Cryptography;

using Sobits.DataEngine;
using Sobits.DataEngine.BLL.Initialization;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic.BLL.Initialization;

using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;

using NHibernate.Tool.hbm2ddl;

using Oracle.DataAccess.Client;

namespace Sobits.Story.Logic
{
    /// <summary>
    /// Session factory
    /// </summary>
    public class SessionFactory : AbstractSessionFactory
    {
        #region singleton

        /// <summary>
        /// Instance of SessionFactory (Singleton)
        /// </summary>
        public static SessionFactory Instance
        {
            get
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionFactory();
                    }
                }

                return _instance;
            }
        }

        private static SessionFactory _instance = null;
        private static readonly Object locker = new Object();

        #endregion singleton

        #region public methods

        /// <summary>
        /// Return DataSession for current thread
        /// </summary>
        /// <returns></returns>
        public IDataSession GetCurrentDataSession()
        {
            return (IDataSession)Instance.GetSession();
        }

        #endregion public methods

        #region static methods

        /// <summary>
        /// Returns current datasession
        /// </summary>
        /// <returns></returns>
        public static IDataSession GetCurrentSession()
        {
            return (IDataSession)Instance.GetSession();
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Get mappings container assembly
        /// </summary>
        /// <returns></returns>
        protected override Assembly GetMappingsAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Get mappings container assembly of
        /// </summary>
        /// <returns></returns>
        protected FluentMappingsContainer GetMappingsAssemblyOf(FluentMappingsContainer mappings, Predicate<Type> where)
        {
            if (where == null)
            {
                return mappings;
            }

            var mappingClasses = Assembly.GetExecutingAssembly()
                                         .GetExportedTypes()
                                         .Where(x => typeof(IMappingProvider).IsAssignableFrom(x) && where(x));

            foreach (var type in mappingClasses)
            {
                mappings.Add(type);
            }

            return mappings;
        }

        /// <summary>
        /// Creating anonymus data session
        /// </summary>
        /// <returns></returns>
        protected override DataEngine.IDataSession CreateDataSession()
        {
            try
            {
                return new DataSession(_nhLocalFactory.OpenSession());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Creating personalyzed data session
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override DataEngine.IDataSession CreateDataSession(IPrincipal user)
        {
            try
            {
                return new DataSession((User)user, _nhLocalFactory.OpenSession());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Initializing SessionFactory
        /// </summary>
        protected override void Initialize()
        {
            FluentConfiguration configLocal = Fluently.Configure()
              .Database(OracleDataClientConfiguration.Oracle10.ConnectionString(c => c.FromConnectionStringWithKey(LOCAL_CONNECTION_STRING))
                  .ShowSql())
              .Mappings(m =>
              {
                  m.FluentMappings.Conventions.Setup(x => x.Add(AutoImport.Never()));
                  GetMappingsAssemblyOf(m.FluentMappings, t => t.Namespace.StartsWith("Sobits.Story.Logic.Mappings"));
              });

            _nhLocalFactory = configLocal.BuildSessionFactory();
            
            if (Configuration.UseSchemaUpdate)
            {
                SchemaUpdate update = new SchemaUpdate(configLocal.BuildConfiguration());
                update.Execute(true, true);
            }
        }
        
        #endregion protected methods

        #region private fields

        private NHibernate.ISessionFactory _nhLocalFactory;
        private NHibernate.ISessionFactory _nhExternalFactory;
        private const String LOCAL_CONNECTION_STRING = "LocalDBConnectionString";
        private readonly Object _lock = new Object();

        #endregion private fields

        #region private methods
        
        #endregion private methods
    }
}
