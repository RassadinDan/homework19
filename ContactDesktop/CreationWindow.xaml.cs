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
	/// Логика взаимодействия для CreationWindow.xaml
	/// </summary>
	public partial class CreationWindow : Window
	{
		public List<Contact> _list;
		public CreationWindow(List<Contact> list)
		{
			InitializeComponent();
			_list = list;
		}

		public void CreateBut_OnClick(object sender, RoutedEventArgs e)
		{
			Contact contact = new Contact();
			contact.Surname = SurnameTextBox.Text;
			contact.Name = NameTextBox.Text;
			contact.Midname = MidnameTextBox.Text;
			contact.Phone = PhoneTextBox.Text;
			contact.Address = AddressTextBox.Text;
			contact.Description = DescriotionTextBox.Text;
			_list.Add(contact);
			var api = new ContactDataApi();
			api.AddContact(contact);
			Close();
		}

		public void CancelBut_OnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
