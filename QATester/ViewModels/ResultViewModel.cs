using Caliburn.Micro;
using QATester.Helpers;
using QATester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private BindableCollection<Visibility> _isVisible= new BindableCollection<Visibility>() { Visibility.Visible,Visibility.Collapsed};

        public BindableCollection<Visibility> IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; NotifyOfPropertyChange(() => IsVisible); }
        }


        private BindableCollection<string> _resultItem = new BindableCollection<string> ();

        public BindableCollection<string> ResultItem
        {
            get { return _resultItem; }
            set { _resultItem = value; NotifyOfPropertyChange(() => ResultItem); }
        }
        public void Report()
        {
			
            IsVisible[0] = Visibility.Collapsed;
            IsVisible[1] = Visibility.Visible;
            Task.Run(() =>
            {
				int i = 1, j = 0;
                foreach (var item in InformationTransfer.QuestionList)
                {
                    ResultItem.Add(i + ")" + item.Question);
                    foreach (var answ in item.WrongAnswer)
                    {
                        ResultItem.Add(answ);
                    }
					if(selectedAnswer[j]!=-1)
					ResultItem.Add("Your choice is " + item.WrongAnswer[selectedAnswer[j]]);
					else
					ResultItem.Add("Your didn't choose ");
					ResultItem.Add("Right asnwer is " + item.WrongAnswer.Where(m => m == questionList[j].RightAnswer).FirstOrDefault());
                    ResultItem.Add("");
					j++;
					i++;
				}
			});
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
			RightAnswerCount = temp.ToString() + " "+ "Right answers";
		}

        private void GetQuestionTolistBox()
        {
          
        }
	}

	
}
