using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	public class WorkoutPlanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
