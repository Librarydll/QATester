using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QATester.Helpers;
using QATester.Model;
using System.Windows.Controls;
using QATester.MyClasses;
using System.Collections;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using System.IO;

namespace QATester.ViewModels
{
	class ShellViewModel : Screen
	{
		int m = 1,
			s = 5;
		DispatcherTimer timer = null;
		IWindowManager window = new WindowManager();
		#region Field
		private readonly string testPath = @"C:\Users\Komil\Pictures\w.tst";
		private List<Questions> questionList = new List<Questions>(); //container where all data will be hold
		private QuestionSerialization deserialization = null;   //my class
		private List<int> _randNumbers = new List<int>(); //random nubmber in order to select first n random question
		private BindableCollection<string> _data = new BindableCollection<string>() { null, null, null, null, null, null }; //elements in XAML
		private int[] selectedAnswer; //held which answers will be selected
		private BindableCollection<bool> _radioButtonIsChecked = new BindableCollection<bool>() { false, false, false, false, false }; //which answer will be selected
		private BindableCollection<Visibility> _extraAnswerVisibility = new BindableCollection<Visibility>() { Visibility.Collapsed, Visibility.Collapsed };
		private string _timerXAML;
		//private Brush brush;
		#endregion
		#region Property
		public int[] RightAnswerCount { get; set; }
		public int CurrentQuestion { get; set; } = 0;
		public string TimerXAML
		{
			get { return _timerXAML; }
			set { _timerXAML = value; NotifyOfPropertyChange(() => TimerXAML); }
		}
		public BindableCollection<bool> RadioButtonIsChecked
		{
			get { return _radioButtonIsChecked; }
			set { _radioButtonIsChecked = value; NotifyOfPropertyChange(() => RadioButtonIsChecked); }
		}
		public BindableCollection<Visibility> ExtraAnswerVisibility
		{
			get { return _extraAnswerVisibility; }
			set { _extraAnswerVisibility = value; NotifyOfPropertyChange(() => ExtraAnswerVisibility); }
		}
		public BindableCollection<string> Data
		{
			get { return _data; }
			set { _data = value; NotifyOfPropertyChange(() => Data); }
		}
		#endregion

		private string _title;

		public string Title
		{
			get { return _title; }
			set { _title = value; NotifyOfPropertyChange(() => Title); }
		}

		private SolidColorBrush _foregroundTimer = Brushes.Black;

		public SolidColorBrush ForegroundTimer  
		{
			get { return _foregroundTimer; }
			set { _foregroundTimer =value; NotifyOfPropertyChange(() => ForegroundTimer); }
		}

		protected override void OnActivate()
		{
			deserialization = new QuestionSerialization();
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(Timer_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();
			Title = "Test";
			if(InformationTransfer.Args.Length>0)
			{
				questionList = deserialization.Deserialize(InformationTransfer.Args[0]);
				Title = Path.GetFileNameWithoutExtension(InformationTransfer.Args[0]);
			}
			questionList = deserialization.Deserialize(testPath);
			questionList.Shuffle();
			RightAnswerCount = new int[questionList.Count];
			for (int i = 0; i < questionList.Count; i++)
			{
				RightAnswerCount[i] = -1;
			}
			selectedAnswer = Enumerable.Repeat(-1, questionList.Count).ToArray();
			ShowCurrentQuestion();
		}

		private void ShowCurrentQuestion()
		{
			if (CurrentQuestion == questionList.Count||questionList.Count==0)
				return;
			int wrongAnswerCount = questionList[CurrentQuestion].WrongAnswer.Count;//Сюда и входит правильный ответ значит минимум 2
			HideSomeAnswer(wrongAnswerCount);
			_data[0] = questionList[CurrentQuestion].Question;
			for (int i = 0; i < wrongAnswerCount; i++)
			{
				_data[i + 1] = questionList[CurrentQuestion].WrongAnswer[i];
			}
			if (selectedAnswer[CurrentQuestion] != -1)
				RadioButtonIsChecked[selectedAnswer[CurrentQuestion]] = true;
			else
			{
				ClearAnswer();
			}

		}
		public void SelectClick()
		{
			if (!RadioButtonIsChecked.All(i => i == false))
				selectedAnswer[CurrentQuestion] = RadioButtonIsChecked.IndexOf(RadioButtonIsChecked.Where(i => i == true).FirstOrDefault());
			if (IsSelectedAnswerRight())
				RightAnswerCount[CurrentQuestion] = selectedAnswer[CurrentQuestion];
			CurrentQuestion++;
			if (CurrentQuestion == questionList.Count)
				CurrentQuestion--;
			ShowCurrentQuestion();
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
			if (CurrentQuestion == questionList.Count)
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
		public bool IsSelectedAnswerRight()
		{
			//Здесю _data является тексблоками (их 6 штук с учетом вопросника)
			//Далее первый индекс selectedAnswer это какой радиобаттон был нажал в данном воросе, а +1 озночает что мы исключаем
			//Из коллекции _data первый элемент то есть вопрос
			//и сравниваем из десализированной коллекции данного вопроса правильный ответ
			if (_data[selectedAnswer[CurrentQuestion] + 1] == questionList[CurrentQuestion].RightAnswer)
				return true;
			return false;
		}
		private void HideSomeAnswer(int count)
		{
			if (count == 3)
			{
				ExtraAnswerVisibility[0] = Visibility.Collapsed;
				ExtraAnswerVisibility[1] = Visibility.Collapsed;
			}
			if (count == 4)
			{
				ExtraAnswerVisibility[0] = Visibility.Visible;
				ExtraAnswerVisibility[1] = Visibility.Collapsed;
			}
			if (count >= 5)
			{
				ExtraAnswerVisibility[0] = Visibility.Visible;
				ExtraAnswerVisibility[1] = Visibility.Visible;
			}
		}
		private void Timer_Tick(object sender, EventArgs target)
		{
			if (m == 0)
			{
				ForegroundTimer = Brushes.Red;
			}
			bool check = false;
			if (m == 0 && s == 1)
			{
				check = true;
				TimerXAML = "00:00";
				timer.Stop();
			}
			s--;
			if (s == 0)
			{
				m--;
				s = 59;
			}
			if (m >= 10 && s >= 10)
				TimerXAML = m.ToString() + " : " + s.ToString();
			if (m >= 10 && s <= 10)
				TimerXAML = m.ToString() + " : " + "0" + s.ToString();
			if (m < 10 && s > 10)
				TimerXAML = "0" + m.ToString() + " : " + s.ToString();
			if (m < 10 && s < 10)
				TimerXAML = "0" + m.ToString() + ":" + "0" + s.ToString();		
			if(check)
				TimerXAML = "00:00";
		}

		public void FinishTest()
		{
			MessageBoxResult message = MessageBox.Show("Do you really want to finish test?", "Finish", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if(message==MessageBoxResult.Yes)
			{
				InformationTransfer.QuestionList = questionList;
				InformationTransfer.SelectedAnswer = selectedAnswer;
				InformationTransfer.RightAnswer = RightAnswerCount;
				window.ShowWindow(new ResultViewModel());	
				TryClose();
			}
		}
	}
}
