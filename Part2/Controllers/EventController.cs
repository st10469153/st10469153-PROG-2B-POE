using RaceDayApplicationApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RaceDayApplicationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		public EventController(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
	}
}
