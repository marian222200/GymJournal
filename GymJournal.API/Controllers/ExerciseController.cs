using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	public class ExerciseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
