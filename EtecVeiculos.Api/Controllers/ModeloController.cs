using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EtecVeiculos.Api.Models; 
using EtecVeiculos.Api.Data;

namespace EtecVeiculos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModeloController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModeloController(AppDbContext _context)
        {
            _context = context;
        }

        // GET: api/Modelo
        [HttpGet]
        public async Task<ActionResult<List<Modelo>>> Get()
        {
            var modelos = await _context.Modelos.ToListAsync();
            return Ok(modelos);
        }

        // GET: api/Modelo/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Modelo>> Get(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound("Modelo não encontrado");

            return Ok(modelo);
        }

        // POST: api/Modelo
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(modelo);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = modelo.Id }, modelo);
            }
            return BadRequest("Verifique os dados informados!");
        }

        // PUT: api/Modelo/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Edit(int id, Modelo modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_context.Modelos.Any(q => q.Id == id))
                        return NotFound("Modelo não encontrado!");

                    if (id != modelo.Id)
                        return BadRequest("Verifique os dados informados!");

                    _context.Entry(modelo).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest($"Ocorreu um problema: {ex.Message}");
                }
            }
            return BadRequest("Verifique os dados informados!");
        }

        // DELETE: api/Modelo/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var modelo = await _context.Modelos.FirstOrDefaultAsync(q => q.Id == id);
                if (modelo == null)
                    return NotFound("Modelo não encontrado");

                _context.Modelos.Remove(modelo);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um problema: {ex.Message}");
            }
        }
    }
}
