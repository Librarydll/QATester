using Caliburn.Micro;
using QATester.Helpers;
using QATester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QATester.ViewModels
{
	public class ResultViewModel :Screen
	{
		List<Questions> questionList =null;
		List<int> selectedAnswer = null;
		List<int> rightAnswerCount = null;
		protected override void OnActivate()
		{
			questionList = InformationTransfer.QuestionList;
			selectedAnswer = InformationTransfer.SelectedAnswer.ToList();
			rightAnswerCount = InformationTransfer.RightAnswer.ToList();
			GetCountRightAnswer();
		}

		private string _rightAnswerCount;

		public string RightAnswerCount
		{
			get { return _rightAnswerCount; }
			set { _rightAnswerCount = value; NotifyOfPropertyChange(() => RightAnswerCount); }
		}

		private void GetCountRightAnswer()
		{
			var temp = rightAnswerCount.Where(i => i >= 0).Count();
			RightAnswerCount = temp.ToString();
		}
	}

	
}
