using ContactDesktop.Data;
using ContactDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Логика взаимодействия для EditionWindow.xaml
	/// </summary>
	public partial class EditionWindow : Window
	{
		public Contact _contact;
		public ObservableCollection<Contact> _contacts;
		public EditionWindow(ObservableCollection<Contact> contacts, Contact contact)
		{
			_contact = contact;
			_contacts = contacts;
			InitializeComponent();
			SurnameTextBox.Text = contact.Surname;
			NameTextBox.Text = contact.Name;
			MidnameTextBox.Text = contact.Midname;
			PhoneTextBox.Text = contact.Phone;
			AddressTextBox.Text = contact.Address;
			DescriotionTextBox.Text = contact.Description;
		}

		public async void EditBut_OnClick(object sender, RoutedEventArgs e)
		{
			_contact.Surname = SurnameTextBox.Text;
			_contact.Name = NameTextBox.Text;
			_contact.Midname = MidnameTextBox.Text;
			_contact.Phone = PhoneTextBox.Text;
			_contact.Address = AddressTextBox.Text;
			_contact.Description = DescriotionTextBox.Text;
			var api = new ContactDataApi();
			await api.Update(_contact);
			var index = _contacts.IndexOf(_contact);
			_contacts.RemoveAt(index);
			_contacts.Insert(index, _contact);
			Close();
		}

		public void CancelBut_OnClick(object sender, RoutedEventArgs e) 
		{
			Close();
		}
	}
}
