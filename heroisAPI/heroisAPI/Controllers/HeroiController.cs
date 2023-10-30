using FluentResults;
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
            Result resultado = _servico.CriarHeroi(heroi);

            if (resultado.IsSuccess)
            {
                return StatusCode(201);
            }
            else if (resultado.Errors.Count == 1 && resultado.Errors[0].Message == "Herói não encontrado!")
            {
                return NotFound(resultado.Errors[0].Message);
            }
            else if (resultado.IsFailed)
            {
                return StatusCode(400, resultado.Reasons);
            }
            else
            {
                return StatusCode(500, resultado.Reasons);
            }


        }

        [HttpPut("AtualizarHeroi")]
        public ActionResult AtualizarHeroi(HeroiDTO heroi)
        {
            Result resultado = _servico.AtualizarHeroi(heroi);

            if (resultado.IsSuccess)
            {
                return StatusCode(201);
            }
            else if (resultado.Errors.Count == 1 && resultado.Errors[0].Message == "Herói não encontrado!")
            {
                return NotFound(resultado.Errors[0].Message);
            }
            else if (resultado.IsFailed)
            {
                return StatusCode(400, resultado.Reasons);
            }
            else
            {
                return StatusCode(500, resultado.Reasons);
            }

        }


        [HttpDelete("RemoverHeroi")]
        public ActionResult RemoverHeroi([FromQuery] int idHeroi)
        {
            Result resultado = _servico.ExcluirHeroi(idHeroi);

            if (resultado.IsSuccess)
            {
                return StatusCode(201);
            }
            else if (resultado.Errors.Count == 1 && resultado.Errors[0].Message == "Herói não encontrado!")
            {
                return NotFound(resultado.Errors[0].Message);
            }
            else if (resultado.IsFailed)
            {
                return StatusCode(400, resultado.Reasons);
            }
            else
            {
                return StatusCode(500, resultado.Reasons);
            }

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


    }
}