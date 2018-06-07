using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary {
	public class UsersController {  //make sure to mark as PUBLIC.  This is another project.  
									//it's not good practice to have a collection return a method. If there's no entries it won't return anything anyway

		SqlConnection conn = null; //this means all our methods can use SqlConnection
		SqlCommand cmd = new SqlCommand();

		private void SetupCommand(SqlConnection conn, string sql) {
			cmd.Connection = conn;
			cmd.CommandText = sql;
			cmd.Parameters.Clear();
		}

		public IEnumerable<User> List() {
			string sql = "select * from [User]";
			//cmd.Connection = conn; 
			//cmd.CommandText = sql;
			//cmd.Parameters.Clear();
			SetupCommand(conn, sql);  
			SqlDataReader reader = cmd.ExecuteReader();
			List<User> users = new List<User>();
			//users.Add(new Iser(reader));
			while (reader.Read()) {
				User user = new User(reader);
				users.Add(user);
			}
			reader.Close();
			return users;
		}
		public User Get(int id) {
			string sql = "select * from [User] where Id = @id";
			//cmd.Connection = conn;   //the setup command replaces this
			//cmd.CommandText = sql;
			//cmd.Parameters.Clear();
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@id", id)); 
			SqlDataReader reader = cmd.ExecuteReader();
			if(reader.HasRows == false) {
				reader.Close();
				return null;
			}
			reader.Read();
			User user = new User(reader);
			reader.Close();
			return user;
			
		}
		public bool Create(User user) {
			string sql = "insert into [User] " +
				" (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) " +
				" VALUES " +
				" (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin) ";
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
			return (recsAffected == 1);
		}
		public bool Change(User user) {
			string sql = "update [User] set " +
							" Username = @Username, " +
							" Password = @Password, " +
							" Firstname = @Firstname, " +
							" Lastname = @Lastname, " +
							" Phone = @Phone, " +
							" Email = @Email, " +
							" IsReviewer = @IsReviewer, " +
							" IsAdmin = @IsAdmin, " +
							" Active = @Active " +
							" where Id = @id;";

			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@id", user.Id));
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));
			int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
			return (recsAffected == 1);
		}
		public bool Remove(User user) {
			string sql = "delete from [user] where id = @id;";
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@id", user.Id));

			int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
			return (recsAffected == 1);
		}

		private SqlConnection CreateAndOpenConnection(string server, string database) { //this is creating and opening our connection to the server
			string connStr = $"server={server};database={database};trusted_connection=true;"; //the STRING and STATEMENT require a semi colon
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if(conn.State != System.Data.ConnectionState.Open) { //Always put in an exception to test if the connection opened
				throw new ApplicationException("Sql Connection did not open");
			}
			return conn; 
		}
		public void CloseConnection() {
			if(conn != null && conn.State == System.Data.ConnectionState.Open) { 
				conn.Close();
			}
		}
		public UsersController(string server, string database) {  //this constructor is passing the server and database from Program, through the connection on line 27
			conn = CreateAndOpenConnection(server, database); {

			}
		}
		public UsersController() {

		}
	}
}
