using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QATester.Model
{
    public class Questions
	{
		public Questions()
		{
			WrongAnswer = new List<string>();
		}

		public int Id { get; set; }
		public string Question { get; set; }
		public string RightAnswer { get; set; }
		public List<string> WrongAnswer { get; set; }
	}
}
