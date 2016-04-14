using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;

using Sobits.WebEngine;
using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic;
using Sobits.Story.Logic.BLL;
using System.Web.Mvc;


namespace Questionnaire.WebSite.Core
{
    public class FileSession
    {
        #region constructors

        internal FileSession(HttpSessionStateBase session)
        {
            _session = session;
        }

        internal FileSession(HttpSessionState session)
        {
            _session = new HttpSessionStateWrapper(session);
        }

        #endregion constructors

        #region public properties
                
        #endregion public properties

        #region public methods

        public FilePathResult GetDataFile(String name)
        {
            return (FilePathResult)_session[name];
        }

        public void SetDataFile(FilePathResult file, String name)
        {
            _session.Add(name, file);
        }

        public void RemoveDataFile(String name)
        {
            _session.Remove(name);
        }

        #endregion public methods

        #region protected methods
        
        #endregion protected methods

        #region protected properties

        #endregion protected properties

        #region private fields

        private readonly HttpSessionStateBase _session;

        #endregion private fields
    }
}