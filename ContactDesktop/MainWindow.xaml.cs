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

namespace ContactDesktop
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<Contact> data { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			var api = new ContactDataApi();
			data = api.GetContacts().ToList<Contact>();

			ContactListBox.ItemsSource = data;
		}

		private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var contact = ContactListBox.SelectedItem as Contact;

			if (contact != null)
			{
				//текст текстблока не выводится, он почему-то не отображается//
				MainTextBlock.Text = $"Фамилия: {contact.Surname}\n" +
					$"Имя: {contact.Name}\nОтчество: {contact.Midname}\nТелефон: {contact.Phone}\n" +
					$"Адрес: {contact.Address}\nОписание: {contact.Description}";
				//ContactListBox.Items.Refresh();
			}
			else
			{ 
				MainTextBlock.Text = string.Empty ;
			}
		}

		private void CreateBut_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new CreationWindow(data);
			window.InitializeComponent();
			window.Show();

		}

		private void DeleteBut_OnClick(object sender, RoutedEventArgs e)
		{
			var c = ContactListBox.SelectedItem as Contact;
			data.Remove(c);
			ContactListBox.Items.Refresh();
			var api = new ContactDataApi();
			api.Remove(c.Id-1);
		}

		private void EditBut_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new EditionWindow(ContactListBox.SelectedItem as Contact);
			window.Show();
		}

		private void AccountBut_OnClick(object sender, RoutedEventArgs e)
		{

		}
	}
}
