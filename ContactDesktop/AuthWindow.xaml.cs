using ContactDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactDesktop
{
	/// <summary>
	/// Логика взаимодействия для AuthWindow.xaml
	/// </summary>
	public partial class AuthWindow : Window
	{
		public AuthWindow()
		{
			InitializeComponent();
		}

		private void RegisterBut_OnClick(object sender, RoutedEventArgs e)
		{
			var form = new UserRegistration();
			var registerWindow = new RegisterWindow(form);
			registerWindow.ShowDialog();
		}

		private void LogInBut_OnClick(object sender, RoutedEventArgs e) 
		{
			var form = new UserLogin();
			var loginWindow = new LoginWindow(form);
			loginWindow.ShowDialog();
		}
	}
}
