using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.DataEngine.Repo.Impl;
using Sobits.Story.Logic.BLL;
using System.Collections;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class VotingRepo : AbstractRepo<Voting>, IVotingRepo
    {
        public VotingRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public List<VotingDM> GetActualVoting(Int32 charterID)
        {
            OracleConnection conn = new OracleConnection(Configuration.DbLocalConnectionString);
            OracleCommand command = new OracleCommand("DBQ.PCD_GET_ACTUAL_VOTING", conn);

            command.CommandType = CommandType.StoredProcedure;

            // Установка выходного параметра
            var outputParameter = new OracleParameter("@return_data", OracleDbType.RefCursor);
            outputParameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(outputParameter);

            // Установка входных параметров
            var parm = new OracleParameter("@charterID", OracleDbType.Int32);
            parm.Direction = ParameterDirection.Input;
            parm.Value = charterID;
            command.Parameters.Add(parm);

            // Создаем дата адаптер
            OracleDataAdapter da = new OracleDataAdapter(command);

            // Создаем сет куда поместим возвращаемый результат
            DataSet ds = new DataSet();

            conn.Open();

            // Выполняем хранимую процедуру и получаем данные
            da.Fill(ds);

            conn.Close();

            List<VotingDM> result = new List<VotingDM>();

            if (ds.Tables == null || ds.Tables.Count == 0) { return result; }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                VotingDM votingDM = new VotingDM();
                votingDM.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
                votingDM.FirstName = dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : String.Empty;
                votingDM.SecondName = dr["SecondName"] != DBNull.Value ? dr["SecondName"].ToString() : String.Empty;
                votingDM.LastName = dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : String.Empty;
                votingDM.IsAnonymous = dr["IsAnonymous"] != DBNull.Value ? Convert.ToBoolean(dr["IsAnonymous"]) : true;
                votingDM.DateVote = dr["DateVote"] != DBNull.Value ? Convert.ToDateTime(dr["DateVote"]) : new DateTime();
                votingDM.IPAddressID = dr["IPAddressID"] != DBNull.Value ? Convert.ToInt32(dr["IPAddressID"]) : 0;
                votingDM.NameOrganization = dr["NameOrganization"] != DBNull.Value ? dr["NameOrganization"].ToString() : String.Empty;
                votingDM.MOOrganizationID = dr["MOOrganizationID"] != DBNull.Value ? Convert.ToInt32(dr["MOOrganizationID"]) : 0;
                votingDM.SMOOrganizationID = dr["SMOOrganizationID"] != DBNull.Value ? Convert.ToInt32(dr["SMOOrganizationID"]) : 0;

                result.Add(votingDM);
            }

            return result;
        }
    }
}
