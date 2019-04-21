using Caliburn.Micro;
using QATester.ViewModels;
using System;
using System.Collections.Generic;
using QATester.Helpers;
using System.Windows;

namespace QATester
{
	public class Bootstrapper :BootstrapperBase
	{
		public Bootstrapper()
		{
			Initialize();
		}
		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			base.OnStartup(sender, e);
			InformationTransfer.Args = e.Args;
			DisplayRootViewFor<ShellViewModel>();
		}


	}
}
