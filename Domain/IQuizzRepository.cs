using AppProjetFilRouge.Models;

namespace AppProjetFilRouge.Domain
{
	public interface IQuizzRepository
	{
		PassageViewModel GetPassageData(int quizzId, int? questionId);
	}
}