using ApiDaSprint04.Models;
using ApiDaSprint04.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDaSprint04.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _service = new PessoaService();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.ObterTodas());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            var resultado = _service.Adicionar(pessoa);
            if (resultado == "Pessoa adicionada com sucesso!")
                return Ok(new { mensagem = resultado });
            else
                return BadRequest(new { erro = resultado });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Pessoa pessoa)
        {
            var resultado = _service.Autenticar(pessoa.Email, pessoa.Senha);
            if (resultado == "Login realizado com sucesso!")
                return Ok(new { mensagem = resultado });
            else
                return Unauthorized(new { erro = resultado });
        }
    }
}