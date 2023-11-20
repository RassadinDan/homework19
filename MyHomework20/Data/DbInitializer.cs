using Microsoft.EntityFrameworkCore;
using MyHomework20.DataContext;

namespace MyHomework20.Data
{
    public class DbInitializer
	{
		public static void Initialize(ContactDBContext context)
		{
			context.Database.EnsureCreated();

			if (context.Contacts.Any()) return;

			using (var trans =  context.Database.BeginTransaction()) 
			{
				foreach (var contact in context.Contacts) 
				{
					context.Contacts.Add(contact);
				}

				context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Contacts] ON");
				context.SaveChanges();
				context.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Contacts] OFF");
				trans.Commit();
			}
		}
	}
}
