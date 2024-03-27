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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactDesktop.Models;
using ContactDesktop.Data;
using System.Collections.ObjectModel;

namespace ContactDesktop
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<Contact> Data { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			var api = new ContactDataApi();
			Loaded += async (sender, e) =>
			{
				var data = await api.GetContacts();
				Data = new ObservableCollection<Contact>(data);
				ContactListBox.ItemsSource = Data;
			};
			if(AuthSession.User == null)
			{
				EditBut.Visibility = Visibility.Hidden;
				CreateBut.Visibility = Visibility.Hidden;
				DeleteBut.Visibility = Visibility.Hidden;
			}
		}

		private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var contact = ContactListBox.SelectedItem as Contact;

			if (contact != null)
			{
				MainTextBlock.Text = $"Фамилия: {contact.Surname}\n" +
					$"Имя: {contact.Name}\nОтчество: {contact.Midname}\nТелефон: {contact.Phone}\n" +
					$"Адрес: {contact.Address}\nОписание: {contact.Description}";
			}
			else
			{ 
				MainTextBlock.Text = string.Empty ;
			}
		}

		private void CreateBut_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new CreationWindow(Data);
			//window.InitializeComponent();
			window.Show();
			//window.Closed += (obj, k) => ContactListBox.UpdateLayout();
		}

		private async void DeleteBut_OnClick(object sender, RoutedEventArgs e)
		{
			var c = ContactListBox.SelectedItem as Contact;
			var api = new ContactDataApi();
			await api.Remove(c.Id);
			Data.Remove(c);
			ContactListBox.UpdateLayout();
		}

		private void EditBut_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new EditionWindow(Data ,ContactListBox.SelectedItem as Contact);
			window.Show();
		}

		private void AccountBut_OnClick(object sender, RoutedEventArgs e)
		{
			var authWindow = new AuthWindow();
			authWindow.Show();
			authWindow.Closed += (obj, k) =>
			{
				if(AuthSession.User != null)
				{
					CreateBut.Visibility = Visibility.Visible;
					if(AuthSession.User.Role == "admin")
					{
						EditBut.Visibility = Visibility.Visible;
						DeleteBut.Visibility = Visibility.Visible;
					}
				}
				else
				{
					CreateBut.Visibility = Visibility.Hidden;
					EditBut.Visibility = Visibility.Hidden;
					DeleteBut.Visibility = Visibility.Hidden;
				}
			};
		}
	}
}
