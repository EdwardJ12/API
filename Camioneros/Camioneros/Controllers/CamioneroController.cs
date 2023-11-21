using Camioneros.Data.Repositorio;
using Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Camioneros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamioneroController : ControllerBase
    {
        private readonly InterfaceCamionero _RepositorioCamionero;

        public CamioneroController(InterfaceCamionero repositorioCamionero)
        {
            _RepositorioCamionero = repositorioCamionero;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCamionero()
        {
            return Ok(await _RepositorioCamionero.GetAllCamionero());
        }

        [HttpGet("{Id_camionero}")]
        public async Task<IActionResult> GetCamioneroDetails(int Id_camionero)
        {
            return Ok(await _RepositorioCamionero.GetCamionero(Id_camionero));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCamionero([FromBody] Camionero camionero)
        {
            if (camionero == null)
            
                return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _RepositorioCamionero.InsertCamionero(camionero);

                return Created("created", created);
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCamionero([FromBody] Camionero camionero)
        {
            if (camionero == null)

                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _RepositorioCamionero.UpdateCamionero(camionero);

            return NoContent(); 

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCamionero(int id)
        {
            await _RepositorioCamionero.DeleteCamionero(new Camionero { id = id });

            return NoContent();
        }

    }
}
