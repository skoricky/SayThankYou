using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using NLog;


namespace VoteWeb
{
    /// <summary>
    /// Corporate values
    /// </summary>
    public enum Value { Unknown = -1, TeamSpirit = 0, Innovation = 1, Commitment = 2, Responsibility = 3 };

    public struct Employee
    {
        public string Account;
        public string Department;
        public string EmployeeName;
        public string ImageUrl;
        public bool WelcomeNoShowCheck;
        public string SelectedLanguage;
        public Dictionary<string, int> currentVotes;
        public Dictionary<string, int> maxVotes;
    }

    /// <summary>
    /// Employee Vote structure
    /// </summary>
    public struct EmployeeVote
    {
        public DateTime Date;
        public string AccountFrom;
        public string AccountTo;
        public Value CorporateValue;
        public string Comment;
    }

    /// <summary>
    /// Corporate Value's state for Employee
    /// </summary>
    public class StatValue
    {
        public string EmployeeName { get; set; }
        public string Value { get; set; }
        public int ValueCount { get; set; }
    }

    public interface IVoteDataStrategy
    {
        int GetCurrentVoteCountByAccount(string account, string corporateValue);
        string GetDepartment(string account);
        string GetEmployeeName(string account);
        string GetImageUrl(string account);
        string GetSelectedLanguage(string account);
        bool GetWelcomeNoShowCheck(string account);
        int GetMaxVoteCountByAccount(string account, string corporateValue);
        void SaveVote(EmployeeVote vote);
        void SaveOptions(Employee employee);
    }

    public class VoteDataDebug : IVoteDataStrategy
    {
        private List<Employee> _employeeList = new List<Employee>();
        private List<EmployeeVote> _voteList = new List<EmployeeVote>();

        private void FillDebugData()
        {
            Employee employee = new Employee();
            employee.Account = "BA001";
            employee.Department = "Marketing Department";
            employee.EmployeeName = "Harly Quinn";
            employee.ImageUrl = "~/img/Debug/HarlyQuinn.jpg";
            employee.WelcomeNoShowCheck = false;
            employee.SelectedLanguage = "";
            employee.currentVotes = new Dictionary<string, int>();
            employee.currentVotes.Add("TeamSpirit", 4);
            employee.currentVotes.Add("Innovation", 10);
            employee.currentVotes.Add("Commitment", 5);
            employee.currentVotes.Add("Responsibility", 1);
            employee.maxVotes = new Dictionary<string, int>();
            employee.maxVotes.Add("TeamSpirit", 10);
            employee.maxVotes.Add("Innovation", 10);
            employee.maxVotes.Add("Commitment", 10);
            employee.maxVotes.Add("Responsibility", 10);
            _employeeList.Add(employee);

            employee = new Employee();
            employee.Account = "BA000";
            employee.Department = "IT Department";
            employee.EmployeeName = "Dead Pool";
            employee.ImageUrl = "~/img/Debug/DeadPool.jpg";
            employee.WelcomeNoShowCheck = false;
            employee.SelectedLanguage = "ENG";
            employee.currentVotes = new Dictionary<string, int>();
            employee.currentVotes.Add("TeamSpirit", 5);
            employee.currentVotes.Add("Innovation", 5);
            employee.currentVotes.Add("Commitment", 5);
            employee.currentVotes.Add("Responsibility", 1);
            employee.maxVotes = new Dictionary<string, int>();
            employee.maxVotes.Add("TeamSpirit", 10);
            employee.maxVotes.Add("Innovation", 10);
            employee.maxVotes.Add("Commitment", -1);
            employee.maxVotes.Add("Responsibility", 10);
            _employeeList.Add(employee);
        }

        public VoteDataDebug()
        {
            FillDebugData();
        }
        
        public int GetCurrentVoteCountByAccount(string account, string corporateValue)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].currentVotes[corporateValue];
                }
            }

            return 0;
        }

        public string GetDepartment(string account)
        {
            for(int i = 0; i < _employeeList.Count; i++)
            {
                if(_employeeList[i].Account == account)
                {
                    return _employeeList[i].Department;
                }
            }

            return "";
        }

        public string GetEmployeeName(string account)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].EmployeeName;
                }
            }

            return "";
        }

        public string GetImageUrl(string account)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].ImageUrl;
                }
            }

            return "";
        }

        public string GetSelectedLanguage(string account)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].SelectedLanguage;
                }
            }

            return "";
        }

        public bool GetWelcomeNoShowCheck(string account)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].WelcomeNoShowCheck;
                }
            }

            return false;
        }

        public int GetMaxVoteCountByAccount(string account, string corporateValue)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Account == account)
                {
                    return _employeeList[i].maxVotes[corporateValue];
                }
            }

            return 0;
        }

        public void SaveVote(EmployeeVote vote)
        {
            _voteList.Add(vote);
        }

        public void SaveOptions(Employee employee)
        {

        }
    }

    public class VoteDataSharepoint : IVoteDataStrategy
    {
        const int SHAREPOINT_CODE_DEPARTMENT = 9;
        const int SHAREPOINT_CODE_EMPLOYEE_NAME = 1;
        const int SHAREPOINT_CODE_IMAGE_URL = 28;

        /// <summary>
        /// Returns open connection to database
        /// </summary>
        /// <returns>Open connection to database</returns>
        private SqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["SharepointConStr"].ToString();
            SqlConnection connect = new SqlConnection(connString);
            connect.Open();
            return connect;
        }

        /// <summary>
        /// Returns sql-script
        /// </summary>
        /// <param name="code">Code of user data item</param>
        /// <param name="account">Account name</param>
        /// <returns>Sql script</returns>
        private string GetSqlUserDataByCode(int code, string account)
        {
            string sql = @"
            SELECT Value
            FROM 
            (
	            SELECT pref.value('(text())[1]', 'nvarchar(255)') AS Account,
		            pref2.value('(text())[1]', 'nvarchar(255)') AS Value
	            FROM
		            [dbo].[UserData] 
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar21') AS Account(pref)
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar" + code.ToString() + @"') AS Value(pref2)
	            WHERE tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54'
            ) t
            WHERE Account = '" + account + "'";
            // ToDo: add parameters -
            return sql;
        }

        /// <summary>
        /// Extracts user data by code in database and account name
        /// </summary>
        /// <param name="code">Code of user data item</param>
        /// <param name="account">Account name</param>
        /// <returns>User data</returns>
        private string GetUserData(int code, string account)
        {
            SqlConnection connect = GetConnection();

            try
            {
                string sql = GetSqlUserDataByCode(code, account);

                SqlCommand command = new SqlCommand(sql, connect);
                string value = command.ExecuteScalar().ToString();
                
                return value;
            }
            finally
            {
                connect.Close();
            }
        }

        /// <summary>
        /// Returns current vote count by account name
        /// </summary>
        /// <param name="account">Account name</param>
        /// <param name="corporateValue">Corporate Value</param>
        /// <returns>Current vote count by account name</returns>
        public int GetCurrentVoteCountByAccount(string account, string corporateValue)
        {
            SqlConnection connect = GetConnection();
            try
            {
                int voteCount = 0;
                using (SqlCommand command = new SqlCommand("SELECT dbo.fn_SGI_GetVotesInMonth(@AccountName, @Value)", connect))
                {
                    command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = account;
                    command.Parameters.Add("@Value", SqlDbType.VarChar).Value = corporateValue;
                    voteCount = (Int32)command.ExecuteScalar();
                }
                            
                return voteCount;
            }
            finally
            {
                connect.Close();
            }
        }

        public string GetDepartment(string account)
        {
            return GetUserData(SHAREPOINT_CODE_DEPARTMENT, account);
        }

        public string GetEmployeeName(string account)
        {
            return GetUserData(SHAREPOINT_CODE_EMPLOYEE_NAME, account);
        }

        public string GetImageUrl(string account)
        {
            return GetUserData(SHAREPOINT_CODE_IMAGE_URL, account);
        }

        public string GetSelectedLanguage(string account)
        {
            return "";
        }

        public bool GetWelcomeNoShowCheck(string account)
        {
            return false;
        }

        /// <summary>
        /// Returns max vote count by account name
        /// </summary>
        /// <param name="account">Account name</param>
        /// <param name="corporateValue">Corporate Value</param>
        /// <returns>Max vote count by account name</returns>
        public int GetMaxVoteCountByAccount(string account, string corporateValue)
        {
            SqlConnection connect = GetConnection();
            try
            {
                int voteCount = 0;
                using (SqlCommand command = new SqlCommand("SELECT dbo.fn_SGI_GetMaxVotes(@AccountName)", connect))
                {
                    command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = account;
                    voteCount = (Int32)command.ExecuteScalar();
                }
                
                return voteCount;
            }
            finally
            {
                connect.Close();
            }
        }
        
        /// <summary>
        /// Saves vote data to Database
        /// </summary>
        /// <param name="vote">Vote structure</param>
        public void SaveVote(EmployeeVote vote)
        {
            SqlConnection connect = GetConnection();
            try
            {
                using (SqlCommand command = new SqlCommand("proc_SGI_AddVote", connect))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@AccountFrom", SqlDbType.VarChar).Value = vote.AccountFrom;
                    command.Parameters.Add("@AccountTo", SqlDbType.VarChar).Value = vote.AccountTo;
                    command.Parameters.Add("@Value", SqlDbType.VarChar).Value = vote.CorporateValue.ToString();
                    command.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = vote.Comment;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connect.Close();
            }
        }

        public void SaveOptions(Employee employee)
        {

        }
    }


    /// <summary>
    /// ProgramClasses
    /// </summary>
    public class ProgramClasses
    {
        public const string ORG_CHART_LINK = @"http://rswdapp00/dep/staff/Org%20Chart%20IT/Home.aspx";
        public const bool TEST_MODE = true;

        


        public class EmployeeVoteData
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            /// <summary>
            /// Returns open connection to database
            /// </summary>
            /// <returns>Open connection to database</returns>
            private SqlConnection GetConnection()
            {
                string connString = ConfigurationManager.ConnectionStrings["ConStringName"].ToString();
                SqlConnection connect = new SqlConnection(connString);
                try
                {
                    logger.Info("Открываем соединение с БД");
                    connect.Open();
                }
                catch
                {
                    logger.Error("Cоединение с БД закончилось с ошибкой");
                    throw;
                }
                return connect;
            }

            /// <summary>
            /// Returns sql-script
            /// </summary>
            /// <param name="code">Code of user data item</param>
            /// <param name="account">Account name</param>
            /// <returns>Sql script</returns>
            private string GetSqlUserDataByCode(int code, string account)
            {
                string sql = @"
            SELECT Value
            FROM 
            (
	            SELECT pref.value('(text())[1]', 'nvarchar(255)') AS Account,
		            pref2.value('(text())[1]', 'nvarchar(255)') AS Value
	            FROM
		            [dbo].[UserData] 
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar21') AS Account(pref)
			            CROSS APPLY
		            tp_ColumnSet.nodes('/nvarchar" + code.ToString() + @"') AS Value(pref2)
	            WHERE tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54'
            ) t
            WHERE Account = '" + account + "'";
                // ToDo: add parameters -
                return sql;
            }   
         
            /// <summary>
            /// Returns current vote count by account name
            /// </summary>
            /// <param name="accountName">Account name</param>
            /// <returns>Current vote count by account name</returns>
            public int GetCurrentVoteCountByAccount(string accountName, string CorporateValue)
            {
                SqlConnection connect = GetConnection();
                try
                {
                    int voteCount = 0;
                    try
                    {
                        logger.Info("Запрос в БД на получение количества отданных голосов");
                        using (SqlCommand command = new SqlCommand("SELECT dbo.fn_SGI_GetVotesInMonth(@AccountName, @Value)", connect))
                        {
                            command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = accountName;
                            command.Parameters.Add("@Value", SqlDbType.VarChar).Value = CorporateValue;
                            voteCount = (Int32)command.ExecuteScalar();
                        }
                    }
                    catch
                    {
                        logger.Error("Запрос в БД на получение получение количества отданных голосов произошел с ошибкой");
                    }
                    return voteCount;
                }

                finally
                {
                    connect.Close();
                }
            }

            /// <summary>
            /// Returns max vote count by account name
            /// </summary>
            /// <param name="accountName">Account name</param>
            /// <returns>Max vote count by account name</returns>
            public int GetMaxVoteCountByAccount(string accountName, string CorporateValue)
            {
                SqlConnection connect = GetConnection();
                try
                {
                    int voteCount = 0;
                    try
                    {
                        logger.Info("Запрос в БД на получение максимального количества возможных голосов");
                        using (SqlCommand command = new SqlCommand("SELECT dbo.fn_SGI_GetMaxVotes(@AccountName)", connect))
                        {
                            command.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = accountName;
                            voteCount = (Int32)command.ExecuteScalar();
                        }
                    }
                    catch
                    {
                        logger.Error("Запрос в БД на получение максимального количества возможных голосов произошел с ошибкой");
                    }
                    return voteCount;
                }
                finally
                {
                    connect.Close();
                }
            }

            /// <summary>
            /// Returns Users List by Corporate values type
            /// </summary>
            /// <param name="Value">Corporate Value</param>
            /// <returns>Users List by Corporate values type</returns>
            public List<StatValue> GetStatByValue(string Value)
            {
                List<StatValue> UsersList = new List<StatValue>();
                SqlConnection connect = GetConnection();
                try
                {
                    try
                    {
                        logger.Info("GetUsersListByValue.GetUp");
                        //    using (SqlCommand command = new SqlCommand("SELECT user, sumValue FROM dbo.view_stat WHERE value = @Value", connect))
                        string sql = @"  SELECT SGI_Votes.Value AS value, t.value AS [user], COUNT(*) AS sumValue
                          FROM SGI_Votes LEFT JOIN 
  	                        (	SELECT pref.value('(text())[1]', 'nvarchar(255)') AS Account,
			                        pref2.value('(text())[1]', 'nvarchar(255)') AS Value
		                        FROM
			                        [dbo].[UserData] 
				                        CROSS APPLY
			                        tp_ColumnSet.nodes('/nvarchar21') AS Account(pref)
				                        CROSS APPLY
			                        tp_ColumnSet.nodes('/nvarchar1') AS Value(pref2)
		                        WHERE tp_ListId = 'F239CEB0-A683-411B-9378-D2EF15A85D54'
	                        ) t ON SGI_Votes.AcountTo = t.Account
                          WHERE SGI_Votes.Value = @Value
                          GROUP BY SGI_Votes.Value, t.value
                          ORDER BY SGI_Votes.Value, COUNT(*) DESC
                        ";

                        using (SqlCommand command = new SqlCommand(sql, connect))
                        {
                            command.Parameters.Add("@Value", SqlDbType.VarChar).Value = Value;
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read() && UsersList.Count <= 10)
                            {
                                string valueName = reader["user"].ToString();
                                int sumValue = (int)reader["sumValue"];
                                UsersList.Add(new StatValue() { EmployeeName = valueName, ValueCount = sumValue });
                            }
                        }
                    }
                    catch
                    {
                        logger.Error("GetUsersListByValue.NoGood");
                    }
                    return UsersList;
                }
                finally
                {
                    connect.Close();
                }

            }

            /// <summary>
            /// Extracts user data by code in database and account name
            /// </summary>
            /// <param name="code">Code of user data item</param>
            /// <param name="account">Account name</param>
            /// <returns>User data</returns>
            public string GetUserData(int code, string account)
            {
                SqlConnection connect = GetConnection();

                try
                {
                    string sql = GetSqlUserDataByCode(code, account);

                    SqlCommand command = new SqlCommand(sql, connect);
                    string value = "";
                    try
                    {
                        logger.Info("Запрос в БД на получение информации о пользователе (code " + code.ToString() + "; account " + account + ")");
                        value = command.ExecuteScalar().ToString();
                    }
                    catch
                    {
                        logger.Error("Запрос в БД на получение информации о пользователе произошел с ошибкой");
                    }
                    return value;
                }

                finally
                {
                    connect.Close();
                }
            }

            /// <summary>
            /// Returns current account (baXXXXXX)
            /// </summary>
            /// <returns>Current account</returns>
            public string GetCurrentAccount()
            {
                const int LOGIN_LEN = 8;
                try
                {
                    logger.Info("Запрашиваем в БД информацию о текущем профиле");

                    string login = "";
                    int fullLoginLen = 0;
                    if (HttpContext.Current != null)
                    {
                        login = HttpContext.Current.User.Identity.Name;
                        fullLoginLen = HttpContext.Current.User.Identity.Name.Length;
                        if (fullLoginLen > LOGIN_LEN)
                        {
                            login = login.Remove(0, fullLoginLen - LOGIN_LEN);
                        }
                    }

                    return login;
                }
                catch
                {
                    logger.Error("Запрос в БД на получение информации о текущем профиле произошел с ошибкой");
                    return "";
                }
            }

        }        
    }
}