using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QATester.Helpers;
using QATester.Model;
using System.Windows.Controls;

namespace QATester.ViewModels
{
	class ShellViewModel : Screen
	{
		private readonly string testPath = @"C:\Users\Komil\Pictures\opo.tst";
		private List<Questions> questionList = new List<Questions>(); //container where all data will be hold
		private SDClass<Questions> deserialization = null;   //my class
		private List<int> _randNumbers = new List<int>(); //random nubmber in order to select first n random question
		private BindableCollection<string> _data = new BindableCollection<string>() { null,null,null,null,null,null }; //elements in XAML
		private int[] selectedAnswer; //held which answers will be selected
		private BindableCollection<bool> _radioButtonIsChecked = new BindableCollection<bool>() {false,false,false,false,false }; //which answer will be selected

		public BindableCollection<bool> RadioButtonIsChecked
		{
			get { return _radioButtonIsChecked; }
			set { _radioButtonIsChecked = value; }
		}
		public BindableCollection<string> Data
		{
			get { return _data; }
			set { _data = value; NotifyOfPropertyChange(() => Data); }
		}
		public int CurrentQuestion { get; set; } = 0;

		protected override void OnActivate()
		{
			deserialization = new SDClass<Questions>();
			//if(InformationTransfer.Args.Length>0)
			//questionList = deserialization.DeserializeXml(InformationTransfer.Args[0]);
			var _temp = deserialization.DeserializeXml(testPath);//for test
			_temp.Shuffle();
			questionList = _temp.GetRange(0, 10);
			selectedAnswer = Enumerable.Repeat(-1, questionList.Count).ToArray();
			ShowCurrentQuestion();
		}

		private void ShowCurrentQuestion()
		{
			_data[0] = questionList[CurrentQuestion].Question;
			_data[1] = questionList[CurrentQuestion].FirstAnswer;
			_data[2] = questionList[CurrentQuestion].SecondAnswer;
			_data[3] = questionList[CurrentQuestion].ThirdAnswer;
			_data[4] = questionList[CurrentQuestion].ForthAnswer;
			_data[5] = questionList[CurrentQuestion].FifthAnswer;
			if (selectedAnswer[CurrentQuestion] != -1)
				RadioButtonIsChecked[selectedAnswer[CurrentQuestion]] = true;
	
		}

		public void SelectClick()
		{
			CurrentQuestion++;
			if (CurrentQuestion == questionList.Count - 1)
				CurrentQuestion--;
			ShowCurrentQuestion();
			selectedAnswer[CurrentQuestion] = RadioButtonIsChecked.IndexOf(RadioButtonIsChecked.Where(i => i == true).FirstOrDefault());

		}
		public void PreviousClick()
		{
			CurrentQuestion--;
			if (CurrentQuestion <= 0)
				CurrentQuestion = 0;
			ShowCurrentQuestion();
		}
		public void NextClick()
		{
			CurrentQuestion++;
			if (CurrentQuestion == questionList.Count - 1)
				CurrentQuestion--;
			ShowCurrentQuestion();
		}
		public void ClearAnswer()
		{
			for (int i = 0; i < RadioButtonIsChecked.Count; i++)
			{
				RadioButtonIsChecked[i] = false;
			}
		}

	}
}
