using ApiApplication.Commands;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProtoDefinitions;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimesController : ControllerBase
    {
        private readonly IShowtimesRepository _showtimesRepo;
        private readonly IMapper _mapper;
        private readonly ApiClientGrpc _apiClientGrpc;

        public ShowTimesController(IShowtimesRepository showtimesRepo,
                                   IMapper mapper,
                                   ApiClientGrpc apiClientGrpc)
        {
            _showtimesRepo = showtimesRepo;
            _mapper = mapper;
            _apiClientGrpc = apiClientGrpc;
        }
        
        [HttpPost("create")]        
        public async Task<ActionResult<ShowtimeResponse>> Create(CreateShowTimeCommand command, CancellationToken cancellationToken)
        {
            var movie = await _apiClientGrpc.GetById(command.ImdbId);
            var movieEntity = _mapper.Map<showResponse, MovieEntity>(movie);

            var showTimeEntity = new ShowtimeEntity();
            showTimeEntity.Movie = movieEntity;
            showTimeEntity.SessionDate = command.SessionDate;
            showTimeEntity.AuditoriumId = command.AuditoriumId;

            showTimeEntity = await _showtimesRepo.CreateShowtime(showTimeEntity, cancellationToken);
            var showTimeResponse = _mapper.Map<ShowtimeEntity, ShowtimeResponse>(showTimeEntity);

            return Ok(showTimeResponse);
        }

        [HttpGet("{showTimeId:int}")]
        public async Task<ActionResult<ShowtimeResponse>> Get([FromRoute]int showTimeId, CancellationToken cancellation)
        {
            var showTime = await _showtimesRepo.GetWithMoviesByIdAsync(showTimeId, cancellation);
            var showTimeResponse = _mapper.Map<ShowtimeEntity, ShowtimeResponse>(showTime);

            return Ok(showTimeResponse);
        }
    }
}
