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
using ContactDesktop.Auth.Dto;

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
			if(AuthSession.User == null)
			{
				UsernameBlock.Text = string.Empty;
				LogoutBut.Visibility = Visibility.Hidden;
				LogInBut.Visibility = Visibility.Visible;
			}
			else
			{
				UsernameBlock.Text = AuthSession.User.UserName;
				UserRoleBlock.Text = AuthSession.User.Role;
				LogInBut.Visibility = Visibility.Hidden;
				LogoutBut.Visibility = Visibility.Visible;
			}
		}

		private void RegisterBut_OnClick(object sender, RoutedEventArgs e)
		{
			var form = new RegistrationRequest();
			var registerWindow = new RegisterWindow(form);
			registerWindow.Show();
		}

		private void LogInBut_OnClick(object sender, RoutedEventArgs e) 
		{
			var loginWindow = new LoginWindow();
			loginWindow.Closed +=(s, args) =>
			{
				Dispatcher.Invoke(() =>
				{
					UsernameBlock.Text = AuthSession.User.UserName;
					UserRoleBlock.Text = AuthSession.User.Role;
					LogInBut.Visibility = Visibility.Hidden;
					LogoutBut.Visibility = Visibility.Visible;
				});
			};
			loginWindow.Show();
		}

		private void LogoutBut_OnClick(object sender, RoutedEventArgs e)
		{
			AuthSession.ClearSession();
			UsernameBlock.Text = string.Empty;
			UserRoleBlock.Text = string.Empty;
			LogoutBut.Visibility = Visibility.Hidden;
			LogInBut.Visibility = Visibility.Visible;
		}
	}
}
