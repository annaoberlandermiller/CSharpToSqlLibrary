using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary { //we added this from another solution. We had to change the namespace to SqlLibrary so we could use this.
					   //We could use a using statement instead
					   //This is a COPY, or a CLONE of this class.  Any changes we make will not affect the origin class. 

    public class User {//mark the class as public if the data is going to public
    
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public bool IsReviewer { get; set; }
		public bool IsAdmin { get; set; }
		public bool Active { get; set; }
		
		public User(SqlDataReader reader) {
			Id = reader.GetInt32(reader.GetOrdinal("Id"));
			Username = reader.GetString(reader.GetOrdinal("Username"));
			Password = reader.GetString(reader.GetOrdinal("Password"));
			Firstname = reader.GetString(reader.GetOrdinal("Firstname"));
			Lastname = reader.GetString(reader.GetOrdinal("Lastname"));
			Phone = reader.GetString(reader.GetOrdinal("Phone"));
			Email = reader.GetString(reader.GetOrdinal("Email"));
			IsReviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
			IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
			Active = reader.GetBoolean(reader.GetOrdinal("Active"));
		}
		public User() { }
	}
}
