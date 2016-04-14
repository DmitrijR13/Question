﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.DataEngine.Repo.Impl;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class BFileRepo : AbstractRepo<BFile>, IBFileRepo
    {
        public BFileRepo(DataEngine.IDataSession session)
            : base(session)
        { }
    }
}
