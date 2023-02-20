using Microsoft.AspNetCore.Identity;

namespace AppProjetFilRouge.Data.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public DateTime ABirthDate { get; set; }

		public string HandleBy { get; set; } = null!;

	}
}
