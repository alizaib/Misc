using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoDefinitions;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly ApiClientGrpc _apiClientGrpc;

        public MoviesController(ApiClientGrpc apiClientGrpc)
        {
            _apiClientGrpc = apiClientGrpc;
        }

        [HttpGet("ListAll")]
        public async Task<ActionResult<showListResponse>> ListAll()
        {
            return await _apiClientGrpc.GetAll();
        }
    }
}
