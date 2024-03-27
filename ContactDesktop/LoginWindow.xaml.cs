using ContactDesktop.Data;
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
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public string _username {  get; set; }
		public LoginWindow()
		{
			InitializeComponent();
		}

		public async void LogInBut_OnClick(object sender, RoutedEventArgs e)
		{
			var form = new LoginRequest();
			form.UserName = LoginBox.Text;
			form.Password= PasswdBox.Password;
			var userData = new UserDataApi();
			await userData.Login(form);
			Close();
		}

		public void CancelBut_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
