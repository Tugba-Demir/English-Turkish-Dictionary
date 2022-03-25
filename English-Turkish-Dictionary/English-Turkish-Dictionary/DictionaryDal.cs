using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace English_Turkish_Dictionary
{
    public class DictionaryDal
    {

        SqlConnection _connection = new SqlConnection(@"Data Source=DESKTOP-M55G9OV\SQLEXPRESS08;Initial Catalog=Dictionary;Integrated Security=True");

        #region ControlConnection
        public void ControlConnection()
        {
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        #endregion

        #region GetAll
        public List<Dictionary> GetAll()
        {
            ControlConnection();
            SqlCommand command = new SqlCommand("select * from DictionaryTable", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Dictionary> words = new List<Dictionary>();

            while (reader.Read())
            {
                Dictionary word = new Dictionary
                {
                    Id = reader["Id"].ToString(),
                    English = reader["English"].ToString(),
                    Turkish = reader["Turkish"].ToString()
                };
                words.Add(word);
            }
            _connection.Close();
            return words;
        }
        #endregion

        #region Add
        public void Add(Dictionary word)
        {
            ControlConnection();
            SqlCommand command = new SqlCommand("Insert into DictionaryTable values(@Id, @English, @Turkish)", _connection);
            command.Parameters.AddWithValue("@Id", word.Id);
            command.Parameters.AddWithValue("@English", word.English);
            command.Parameters.AddWithValue("@Turkish", word.Turkish);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        #endregion

        #region Update
        public void Update(Dictionary _word)
        {
            ControlConnection();
            SqlCommand command = new SqlCommand("Update DictionaryTable set Id=@id, English=@english, Turkish=@turkish where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", _word.Id);
            command.Parameters.AddWithValue("@english", _word.English);
            command.Parameters.AddWithValue("@turkish", _word.Turkish);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        #endregion

        #region Remove
        public void Remove(Dictionary _word)
        {
            ControlConnection();
            SqlCommand command = new SqlCommand("Delete from DictionaryTable where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", _word.Id);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        #endregion

        public List<Dictionary> EnglishWords()
        {
            ControlConnection();
            SqlCommand command = new SqlCommand("Select * from DictionaryTable", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Dictionary> words = new List<Dictionary>();

            while (reader.Read())
            {
                Dictionary word = new Dictionary
                {
                    English = reader["English"].ToString(),
                    Turkish = reader["Turkish"].ToString()
                };
                words.Add(word);
            }
            _connection.Close();
            return words;
        }
    }
}
