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
	/// Логика взаимодействия для RegisterWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
	{
		public UserRegistration _form;
		public RegisterWindow(UserRegistration form)
		{
			InitializeComponent();
			_form = form;
		}


		private void RegisterBut_OnClick(object sender, RoutedEventArgs e)
		{
			var userData = new UserDataApi();
			if (LoginBox.Text != string.Empty && PasswdBox.Password != string.Empty && PasswdConfirmationBox.Password != string.Empty &&
				PasswdBox.Password==PasswdConfirmationBox.Password)
			{
				_form.UserName = LoginBox.Text;
				_form.Password = PasswdBox.Password;
				_form.ConfirmPassword = PasswdConfirmationBox.Password;
				userData.Register(_form);
				Close();
			}
			else
			{ 
				MessageBox.Show("Все поля должны быть заполнены, пароли должны совпадать", "Warning", MessageBoxButton.OK);
			}
		}

		private void CancelBut_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
