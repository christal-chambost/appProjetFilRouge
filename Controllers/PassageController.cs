using Microsoft.AspNetCore.Mvc;
using AppProjetFilRouge.Domain;

namespace AppProjetFilRouge.Controllers
{

	public class PassageController : Controller
	{
		IQuizzRepository repository;

		public PassageController(IQuizzRepository repo)
		{
			this.repository = repo;
		}

		[HttpGet]
		[Route("/Passage/{id}/{questionId?}")]
		public IActionResult Index(int id, int? questionId)
		{
			var data = repository.GetPassageData(id, questionId);
			return View(data);
		}

		[HttpPost]
		[Route("/Passage/{id}/{questionId}")]
		public IActionResult Index(int id, int questionId, IFormCollection input)
		{
			var data = repository.GetPassageData(id, questionId);
			//Pour chaque réponse on va regarde dans la form collection si c'est check ou pas.
			var responseIds = data.Reponses.Where(reponseId => input.ContainsKey(reponseId.Id.ToString())).Select(a => a.Id);

			// TODO Sauvegarde en base
			// myRepo.SaveAnswers(id, questionId, reponsesChoisies);

			// Redirect vers la questions suivante :
			if (data.NextQuestionId == null)
			{
				return Content("Résultats");
			}

			return RedirectToAction("Index", new { id, questionId = data.NextQuestionId });
		}
	}
}
