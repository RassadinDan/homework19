namespace Homework19
{
    public class Account
    {
        public int Id;

        public string Surname;

        public string Name;

        public string Midname;

        public int Phone;

        public string Address;

        public string Description;

        public Account(int id, string surname, string name, string midname, int phone, string address, string description)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Midname = midname;
            Phone = phone;
            Address = address;
            Description = description;
        }
    }
}
