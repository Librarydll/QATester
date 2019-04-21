using QATester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QATester.Helpers
{
	public static class InformationTransfer
	{
		public static string[] Args { get; set; }
		public static List<Questions> QuestionList { get; set; }
		public static int [] SelectedAnswer { get; set; }
		public static int [] RightAnswer { get; set; }
	}
}
