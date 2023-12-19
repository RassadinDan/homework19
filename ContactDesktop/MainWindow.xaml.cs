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
		public List<Contact> data {  get; set; }
		public MainWindow()
		{
			InitializeComponent();
			var api = new ContactDataApi();
			data = api.GetContacts().ToList<Contact>();

			foreach(Contact contact in data)
			{
				ContactListBox.Items.Add(contact);
			}
		}

		private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{
			var contact = ContactListBox.SelectedItem as Contact;
			MainArea.Text = $"Фамилия: {contact.Surname}\n" +
				$"Имя: {contact.Name}\nОтчество: {contact.Midname}\nТелефон: {contact.Phone}\n" +
				$"Адрес: {contact.Address}\nОписание: {contact.Description}";
		}
	}
}
