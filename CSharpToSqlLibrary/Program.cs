using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlLibrary;

namespace CSharpToSqlLibrary {
	class Program {
		static void Main(string[] args) {

			UsersController UserCtrl = new UsersController(@"STUDENT02\SQLEXPRESS", "prssql");

			IEnumerable<User> users = UserCtrl.List();
			foreach(User user1 in users) {
				Console.WriteLine($"{user1.Firstname} {user1.Lastname}");
			}

			User user = UserCtrl.Get(3);
			if (user == null) {
				Console.WriteLine("User not found");
			} else {
				Console.WriteLine($"{user.Firstname} {user.Lastname}");
			}

			User newUser = new User();
			newUser.Username = "newUser4567";
			newUser.Password = "PaSsWoRd";
			newUser.Firstname = "New";
			newUser.Lastname = "User";
			newUser.Phone = "666-666-6666";
			newUser.Email = "newuser@newuser.com";
			newUser.IsReviewer = true;
			newUser.IsAdmin = false;

			//bool success = UserCtrl.Create(newUser);

			

			user = UserCtrl.Get(8);
			user.Username = "Lisa"; //Username is what we're updating.  Lisa is the NEW username
			bool success = UserCtrl.Change(user);

			//UserCtrl.CloseConnection();

			user = UserCtrl.Get(25);
			success = UserCtrl.Remove(user);

			UserCtrl.CloseConnection();
		}
	}
}
