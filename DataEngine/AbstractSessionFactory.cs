using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Security.Principal;

using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using Sobits.DataEngine.BLL;
using Sobits.DataEngine.Listeners;
using Sobits.DataEngine.BLL.Initialization;


namespace Sobits.DataEngine
{
    /// <summary>
    /// Abstract session factory. Object for manage data sessions. Must be singleton
    /// </summary>
    public abstract class AbstractSessionFactory
    {
        #region constructors
        
        /// <summary>
        /// Default constructor
        /// </summary>
        protected AbstractSessionFactory()
        {
            Initialize();
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// Open anonymus session
        /// </summary>
        public IDataSession OpenSession()
        {
            IDataSession session = CreateDataSession();

            lock (_lock)
            {
                _sessions[CurrentThreadID] = session;
            }

            return session;
        }

        /// <summary>
        /// Open personalized session
        /// </summary>
        /// <param name="user"></param>
        public IDataSession OpenSession(IPrincipal user)
        {
            IDataSession session = CreateDataSession(user);
            
            lock (_lock)
            {
                _sessions[CurrentThreadID] = session;
            }

            return session;
        }

        /// <summary>
        /// close session and dispose clear context
        /// </summary>
        public void CloseSession()
        {
            GetSession().Dispose();

            lock (_lock)
            {
                _sessions.Remove(CurrentThreadID);
            }
        }

        /// <summary>
        /// Get session from sessions dictionary. Every worker thread with DB must have it
        /// </summary>
        /// <returns></returns>
        protected internal IDataSession GetSession()
        {
            IDataSession session = null;

            if (!_sessions.TryGetValue(CurrentThreadID, out session))
            {
                throw new InvalidOperationException("DataSession has not been started for this thread");
            }

            return session;
        }

        #endregion public methods 

        #region protected methods

        /// <summary>
        /// Creating anonymus data session
        /// </summary>
        /// <returns></returns>
        protected virtual IDataSession CreateDataSession()
        {
            return new DataSession(_nhFactory.OpenSession());
        }

        /// <summary>
        /// Creating personalized data session
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual IDataSession CreateDataSession(IPrincipal user)
        {
            return new DataSession(user, _nhFactory.OpenSession());
        }

        /// <summary>
        /// Initializing session factory
        /// </summary>
        protected virtual void Initialize()
        {
            FluentConfiguration config = Fluently.Configure()
              //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(LOCAL_CONNECTION_STRING))
              //.Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey(LOCAL_CONNECTION_STRING))
              .Database(PostgreSQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey(LOCAL_CONNECTION_STRING))
                  //.ProxyFactoryFactory<NHibernate.ByteCode.Castle.ProxyFactoryFactory>()
                  .ShowSql())
                .ExposeConfiguration(x =>
                {
                    x.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[] { new SaveEventListener() };
                    x.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[] { new SaveEventListener() };
                    x.EventListeners.DeleteEventListeners = new NHibernate.Event.IDeleteEventListener[] { new DeleteEventListener() };
                })
                .Mappings(m => { m.FluentMappings.AddFromAssembly(GetMappingsAssembly()); });

            _nhFactory = config.BuildSessionFactory();

            if (Configuration.UseSchemaUpdate)
            {
                SchemaUpdate update = new SchemaUpdate(config.BuildConfiguration());
                update.Execute(true, true);
            }
        }
        
        /// <summary>
        /// Get assemblie, that include mappings
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetMappingsAssembly();
        
        #endregion protected methods

        #region protected properties

        /// <summary>
        /// NHibernate session factory
        /// </summary>
        protected NHibernate.ISessionFactory NHFactory
        {
            get
            {
                return _nhFactory;
            }
        }            

        #endregion

        #region private properties

        /// <summary>
        /// ID of current thread (System.Threading.ManagedThreadId)
        /// </summary>
        private Int32 CurrentThreadID
        {
            get
            {
                return System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
        }

        #endregion private properties
        
        #region private fields

        private NHibernate.ISessionFactory _nhFactory;
        private readonly Dictionary<Int32, IDataSession> _sessions = new Dictionary<Int32, IDataSession>();
        private const String LOCAL_CONNECTION_STRING = "LocalDBConnectionString";
        private readonly Object _lock = new Object();

        #endregion private fields
    }
}
