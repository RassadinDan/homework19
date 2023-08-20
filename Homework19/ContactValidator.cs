﻿using FluentValidation;
using Homework19.Models;

namespace Homework19
{
	public class ContactValidator: AbstractValidator<Contact>
	{
		public ContactValidator()
		{
			RuleFor(c => c.Surname)./*Custom().*/NotEmpty();
			RuleFor(c => c.Name).NotEmpty();
			RuleFor(c => c.Midname).NotEmpty();
			RuleFor(c => c.Phone).NotEmpty();
			RuleFor(c => c.Address).NotEmpty();
			RuleFor(c => c.Description).NotEmpty();
		}
	}
}
