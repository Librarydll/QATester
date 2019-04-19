using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QATester.Helpers;
using QATester.Model;
using System.Windows;

namespace QATester.ViewModels
{
	class ShellViewModel :Screen 
	{
		private readonly string testPath = @"C:\Users\Komil\Pictures\opo.tst";
		private List<Questions> questionList = new List<Questions>();
		private SDClass<Questions> deserialization = null;
		private List<int> _randNumbers = new List<int>();
		public int CurrentQuestion { get; set; } = 0; 
		protected override void OnActivate()
		{
			deserialization = new SDClass<Questions>();
			//if(InformationTransfer.Args.Length>0)
			//questionList = deserialization.DeserializeXml(InformationTransfer.Args[0]);
			questionList = deserialization.DeserializeXml(testPath);
			questionList.Shuffle();
		}



	}
}
