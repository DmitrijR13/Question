using NHibernate.SqlTypes;
using NHibernate.Type;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.Extensions
{
    [Serializable]
    public class Int64ArrayType : ImmutableType
    {
        public Int64ArrayType()
            : base(SqlTypeFactory.Int64)
        {
        }

        public override object Get(System.Data.IDataReader rs, string name)
        {
            return rs[name];
        }

        public override object Get(System.Data.IDataReader rs, int index)
        {
            return rs[index];
        }

        public override void Set(System.Data.IDbCommand cmd, object value, int index)
        {
            OracleCommand orclCmd = (OracleCommand)cmd;
            orclCmd.Parameters[index].OracleDbType = OracleDbType.Int64;
            orclCmd.Parameters[index].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            orclCmd.Parameters[index].Value = value;
        }

        public override string ToString(object val)
        {
            return String.Join(";", (object[])val);
        }

        public override string Name
        {
            get { return "Int64ArrayType"; }
        }

        public override Type ReturnedClass
        {
            get { return typeof(long[]); }
        }

        public override object FromStringValue(string xml)
        {
            string[] stringElements = xml.Split(';');
            long[] array = new long[stringElements.Length];

            for (int i = 0; i < stringElements.Length; i++)
            {
                array[i] = Convert.ToInt64(stringElements[i]);
            }

            return array;
        }
    }
}
