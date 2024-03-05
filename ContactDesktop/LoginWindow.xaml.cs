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

namespace ContactDesktop
{
	/// <summary>
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public UserLogin _form;
		public string _username {  get; set; }
		public LoginWindow(UserLogin form)
		{
			InitializeComponent();
			_form = form;
		}

		public async void LogInBut_OnClick(object sender, RoutedEventArgs e)
		{
			_form.UserName = LoginBox.Text;
			_form.Password= PasswdBox.Password;
			var userData = new UserDataApi();
			var r = await userData.Login(_form);
			_username = r;
			Close();
		}

		public void CancelBut_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
