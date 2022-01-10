using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System;

namespace work1
{
    public  class mysql
    {
        public void Initialize()
        {
            Debug.WriteLine("DataBase Initialize");
            string ip = Environment.GetEnvironmentVariable("S_Ip");
            string db = Environment.GetEnvironmentVariable("S_Database");
            string uid = Environment.GetEnvironmentVariable("S_Uid");
            string pwd = Environment.GetEnvironmentVariable("S_Pwd");
            string port = Environment.GetEnvironmentVariable("S_Port");
            string connectionPath = $"Server="+ ip + ";Database=" + db + ";Uid=" + uid + ";Pwd=" + pwd + ";Port=" + port;
            App.connection = new MySqlConnection(connectionPath);
        }
        // Create MySqlCommand
        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, App.connection);
            return command;
        }


        // DataBase Connection
        public bool OpenMySqlConnection()
        {
            try
            {
                App.connection.Open();
                Debug.WriteLine("okok");
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Debug.WriteLine("Unable to Connect to Server");
                        break;
                    case 1045:
                        Debug.WriteLine("Please check your ID or PassWord");
                        break;
                }
                return false;
            }
        }



        public bool CloseMySqlConnection()
        {
            try
            {
                App.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }


        // Queyr Executer(Insert, Delete, Update ...)
        public void MySqlQueryExecuter(string userQuery)
        {
            string query = userQuery;

            if (OpenMySqlConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, App.connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    Debug.WriteLine("값 저장 성공");
                    App.DataSaveResult = true;
                }
                else
                {
                    Debug.WriteLine("값 저장 실패");
                    App.DataSaveResult = false;
                }

                CloseMySqlConnection();
            }
        }
        public List<double> GetData(string tableName, int type)
        {
            string query = "SELECT * FROM" + " " + tableName + " WHERE type=" + type;
            List<double> element = new List<double>();
            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    element.Add(Convert.ToDouble(dataReader["value"]));
                }

                // 추가된 코드
                if (element != null)
                {
                    for (int i = 0; i < element.Count; i++)
                    {
                        Debug.WriteLine(element[i]);
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return element;
            }
            else
            {
                return null;
            }
        }


        public List<string>[] Select(string tableName, int columnCnt, string email, string pw)
        {
            string query = "SELECT * FROM" + " " + tableName;

            List<string>[] element = new List<string>[columnCnt];

            for (int index = 0; index < element.Length; index++)
            {
                element[index] = new List<string>();
            }

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    element[0].Add(dataReader["email"].ToString());
                    element[1].Add(dataReader["password"].ToString());
                }

                // 추가된 코드
                if (element != null)
                {
                    for (int i = 0; i < element[0].Count; i++)
                    {
                        if (email == element[0][i])
                        {
                            Debug.WriteLine("adf");
                            foreach (var item in element)
                            {
                                Debug.WriteLine(item[0]);
                            }
                            Debug.WriteLine("zxcv");
                            for (int j = 0; j < element[1].Count; j++)
                            {
                                string passwordHash = BCrypt.Net.BCrypt.HashPassword(pw, BCrypt.Net.BCrypt.GenerateSalt(10));
                                Debug.WriteLine("passwordHash " + passwordHash);
                                string testhash = "$2a$10$mA9OZCceskSWFHkWKW9XTOcZ/AvixAwds/ry/vTDYT2eHcqcQDL3u";
                                // 라라벨 bcrypt $2y$로 시작해서 적용안되는거 강제수정
                                string output = "$2a$" + element[1][i].Substring(4);

                                bool verifyPassword = BCrypt.Net.BCrypt.Verify(pw, output);
                                Debug.WriteLine(verifyPassword);
                                if (verifyPassword == true)
                                {
                                    App.DataSearchResult = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return element;
            }
            else
            {
                return null;
            }
        }
    }
}
