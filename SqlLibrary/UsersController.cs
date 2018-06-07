using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary {
	public class UsersController {  //make sure to mark as PUBLIC.  This is another project.  
									//it's not good practice to have a collection return a method. If there's no entries it won't return anything anyway
		public IEnumerable<User> List() {
			return new List<User>();
		}
		public User Get(int id) {
			return null;
		}
		public bool Create(User user) {
			return false;
		}
		public bool Change(User user) {
			return false;
		}
		public bool Remove(User user) {
			return false;
		}
	}
}
