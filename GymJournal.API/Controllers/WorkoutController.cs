using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	public class WorkoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
