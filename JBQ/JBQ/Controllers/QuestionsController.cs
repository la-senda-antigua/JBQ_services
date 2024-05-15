using JBQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JBQ.Controllers
{
    public class QuestionsController : ApiController
    {
        [HttpGet]
        public List<Question> GetAllQuestions(string lang)
        {
            if (lang.ToLower() != "es" && lang.ToLower() != "en")
                return null;
                        

            var list = new List<Question>();            
            DataTable table = GetQuestions(lang.ToLower());

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Question()
                {
                    PK_Question_ID = (Int64)row["PK_Question_ID"],
                    AnswerText = (string) row["AnswerText"],
                    Points = (Int64) row["Points"],
                    QuestionText = (string)row["QuestionText"],
                    Reference = row["Reference"] == DBNull.Value ? string.Empty : (string)row["Reference"],
                    UniqueCharacterIndex = (Int64)row["UniqueCharacterIndex"]
                });
            }

            return list;
        }

        private DataTable GetQuestions(string lang)
        {
            //string connectionString = $@"Data Source=C:\Users\hugo.quinonez\Desktop\spanish.db";
            string connectionString = $@"Data Source={AppDomain.CurrentDomain.BaseDirectory}DB\{lang}.db";

            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM questions";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);                    
                    da.Fill(table);
                }

            }
            return table;
        }
    }
}
