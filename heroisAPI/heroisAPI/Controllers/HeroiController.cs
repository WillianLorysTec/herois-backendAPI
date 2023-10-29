using Interfaces.Servico;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace heroisAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroiController : ControllerBase
    {
        private readonly IFluxoHeroiServico _servico;

        public HeroiController(IFluxoHeroiServico servico)
        {
            _servico = servico;
        }

        [HttpPost("CriarHeroi")]
        public ActionResult CriarHeroi([FromBody] HeroiDTO heroi)
        {
            _servico.CriarHeroi(heroi);

            return Ok();
        }

        [HttpPost("RemoverHeroi")]
        public ActionResult RemoverHeroi([FromQuery] int idHeroi)
        {
            _servico.ExcluirHeroi(idHeroi);

            return Ok();
        }


        [HttpGet("ListarHerois")]
        public ActionResult ListarHerois()
        {
            List<Heroi> heroi = _servico.ListarHerois();

            return Ok(heroi);

        }

        [HttpGet("ListarHeroiID")]
        public ActionResult ListarHeroiPorId([FromQuery] int idHeroi)
        {
            HeroiViewModel heroi = _servico.ListarHeroiPorId(idHeroi);

            return Ok(heroi);

        }

        [HttpGet("ListarPoderes")]
        public ActionResult ListarPoderes()
        {
            return Ok(_servico.ListarPoderes());
        }

        [HttpPut("AtualizarHeroi")]
        public ActionResult AtualizarHeroi(HeroiDTO heroi)
        {
            _servico.AtualizarHeroi(heroi);
            return Ok();
        }

    }
}