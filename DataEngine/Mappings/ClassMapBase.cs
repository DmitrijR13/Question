using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

using Sobits.DataEngine;

namespace Sobits.DataEngine.Mappings
{
    /// <summary>
    /// Base NH mapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ClassMapBase<T> : ClassMap<T>
        where T : IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ClassMapBase()
        {
            Id(x => x.ID).Not.Nullable();
            LazyLoad();
            Cache.IncludeNonLazy().ReadWrite();
        }
    }
}
