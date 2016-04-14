using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using NHibernate;
using Sobits.Story.Logic.BLL;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class VotingQuestionRepo : BaseRepo<VotingQuestion>, IVotingQuestionRepo
    {
        public VotingQuestionRepo(ISession session)
            : base(session)
        { }

        public IQueryable<VotingQuestion> GetByQuestionVoting(Int32 questionID, Int32 votingID)
        {
            return GetAll().Where(x => !x.Question.IsDelete &&
                                       x.Question.ID == questionID &&
                                       x.Voting.ID == votingID);
        }

        public void CopyFromTemp(Int32 votingID)
        {
            Context.CreateSQLQuery("begin DBQ.PCD_COPY_TEMP_VOTING(:votingid); end;")
                .SetInt32("votingid", votingID)
                .ExecuteUpdate();
        }

        public List<QuestionAnswerDM> GetQuestionAnswerByCharter(Int32 charterID)
        {
            return GetAll().Where(x => !x.Question.IsDelete &&
                                       x.Question.Charter.ID == charterID)
                           .Select(x => new QuestionAnswerDM()
                           {
                               VotingID = x.Voting.ID,
                               AnswerID = x.Answer.ID,
                               AnswerValue = x.Answer.Value,
                               AnswerNumberSequence = x.Answer.NumberSequence,
                               QuestionID = x.Question.ID,
                               QuestionValue = x.Question.Value,
                               QuestionNumberSequence = x.Question.NumberSequence
                           })
                           .ToList();
        }

        public List<StatisticDM> BuildStatistic(List<Int32> answerIDs, Int32 charterID)
        {
            List<Int32> questionIDs = new List<Int32>();
            List<Answer> answers = new List<Answer>();

            for (Int32 i = 0; i < answerIDs.Count(); i++)
            {
                Answer answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(answerIDs[i]);
                answers.Add(answer);
                if (!questionIDs.Contains(answer.Question.ID)) { questionIDs.Add(answer.Question.ID); }
            }

            List<VotingQuestion> votingQuestion = GetAll().Where(x => answerIDs.Contains(x.Answer.ID)).ToList();

            List<TempStat> tempStat = votingQuestion.GroupBy(x => new 
                                                    { 
                                                        VotingID = x.Voting.ID 
                                                    })
                                                    .Select(x => new TempStat()
                                                    {
                                                        VotingID = x.Key.VotingID,
                                                        Count = x.Count()
                                                    })
                                                    .ToList();

            tempStat = tempStat.Where(x => x.Count == questionIDs.Count()).ToList();

            List<StatisticDM> result = GetAll().ToList()
                                               .Where(x => tempStat.Select(t => t.VotingID)
                                                                   .Contains(x.Voting.ID))
                                               .Select(x => new StatisticDM()
                                               {
                                                   VotingID = x.Voting.ID,
                                                   DateVote = x.Voting.DateVote,
                                                   IPAddressID = x.Voting.IPAddress != null ? x.Voting.IPAddress.ID : 0,
                                                   NameOrganization = x.Voting.IPAddress != null ? x.Voting.IPAddress.NameOrganization : String.Empty,
                                                   QuestionID = x.Question.ID,
                                                   TextQuestion = x.Question.Value,
                                                   NumberSequenceQuestion = x.Question.NumberSequence,
                                                   AnswerID = x.Answer.ID,
                                                   TextAnswer = x.Answer.Value,
                                                   NumberSequenceAnswer = x.Answer.NumberSequence
                                               })
                                               .ToList();

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    StatisticDM statisticDM = new StatisticDM();
            //    statisticDM.VotingID = dr["votingID"] != DBNull.Value ? Convert.ToInt32(dr["votingID"]) : 0;
            //    statisticDM.DateVote = dr["dateVote"] != DBNull.Value ? Convert.ToDateTime(dr["dateVote"]) : new DateTime();
            //    statisticDM.IPAddressID = dr["ipAddressID"] != DBNull.Value ? Convert.ToInt32(dr["ipAddressID"]) : 0;
            //    statisticDM.NameOrganization = dr["nameOrganization"] != DBNull.Value ? dr["nameOrganization"].ToString() : String.Empty;
            //    statisticDM.QuestionID = dr["questionID"] != DBNull.Value ? Convert.ToInt32(dr["questionID"]) : 0;
            //    statisticDM.TextQuestion = dr["textQuestion"] != DBNull.Value ? dr["textQuestion"].ToString() : String.Empty;
            //    statisticDM.NumberSequenceQuestion = dr["numberSequenceQuestion"] != DBNull.Value ? Convert.ToInt32(dr["numberSequenceQuestion"]) : 0;
            //    statisticDM.AnswerID = dr["answerID"] != DBNull.Value ? Convert.ToInt32(dr["answerID"]) : 0;
            //    statisticDM.TextAnswer = dr["textAnswer"] != DBNull.Value ? dr["textAnswer"].ToString() : String.Empty;
            //    statisticDM.NumberSequenceAnswer = dr["numberSequenceAnswer"] != DBNull.Value ? Convert.ToInt32(dr["numberSequenceAnswer"]) : 0;

            //    result.Add(statisticDM);
            //}

            //OracleConnection conn = new OracleConnection(Configuration.DbLocalConnectionString);
            //OracleCommand command = new OracleCommand("DBQ.PCD_BUILD_STAT", conn);
                
            //command.CommandType = CommandType.StoredProcedure;

            //// Установка выходного параметра
            //var outputParameter = new OracleParameter("@return_data", OracleDbType.RefCursor);
            //outputParameter.Direction = ParameterDirection.Output;
            //command.Parameters.Add(outputParameter);

            //// Установка входных параметров
            //var parm = new OracleParameter("@answerIDs", OracleDbType.Clob);
            //parm.Direction = ParameterDirection.Input;
            //parm.Value = ToXML(answerIDs).OuterXml;
            //command.Parameters.Add(parm);

            //// Создаем дата адаптер
            //OracleDataAdapter da = new OracleDataAdapter(command);

            //// Создаем сет куда поместим возвращаемый результат
            //DataSet ds = new DataSet();

            //conn.Open();

            //// Выполняем хранимую процедуру и получаем данные
            //da.Fill(ds);
                
            //conn.Close();

            //List<StatisticDM> result = new List<StatisticDM>();

            //if (ds.Tables == null || ds.Tables.Count == 0) { return result; }

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    StatisticDM statisticDM = new StatisticDM();
            //    statisticDM.VotingID = dr["votingID"] != DBNull.Value ? Convert.ToInt32(dr["votingID"]) : 0;
            //    statisticDM.DateVote = dr["dateVote"] != DBNull.Value ? Convert.ToDateTime(dr["dateVote"]) : new DateTime();
            //    statisticDM.IPAddressID = dr["ipAddressID"] != DBNull.Value ? Convert.ToInt32(dr["ipAddressID"]) : 0;
            //    statisticDM.NameOrganization = dr["nameOrganization"] != DBNull.Value ? dr["nameOrganization"].ToString() : String.Empty;
            //    statisticDM.QuestionID = dr["questionID"] != DBNull.Value ? Convert.ToInt32(dr["questionID"]) : 0;
            //    statisticDM.TextQuestion = dr["textQuestion"] != DBNull.Value ? dr["textQuestion"].ToString() : String.Empty;
            //    statisticDM.NumberSequenceQuestion = dr["numberSequenceQuestion"] != DBNull.Value ? Convert.ToInt32(dr["numberSequenceQuestion"]) : 0;
            //    statisticDM.AnswerID = dr["answerID"] != DBNull.Value ? Convert.ToInt32(dr["answerID"]) : 0;
            //    statisticDM.TextAnswer = dr["textAnswer"] != DBNull.Value ? dr["textAnswer"].ToString() : String.Empty;
            //    statisticDM.NumberSequenceAnswer = dr["numberSequenceAnswer"] != DBNull.Value ? Convert.ToInt32(dr["numberSequenceAnswer"]) : 0;

            //    result.Add(statisticDM);
            //}

            return result;
        }

        private XmlDocument ToXML(List<Int32> answerIDs)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("ids");
            xdoc.AppendChild(root);

            foreach (Int32 item in answerIDs)
            {
                XmlElement xid = xdoc.CreateElement("id");
                xid.InnerText = item.ToString();

                root.AppendChild(xid);
            }

            return xdoc;
        }

        public class TempStat
        {
            public Int32 VotingID { get; set; }
            public Int32 Count { get; set; }
        }
    }
}
