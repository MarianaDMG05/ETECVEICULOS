using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EtecVeiculos.Api.Models; // Certifique-se de ajustar o namespace para os seus modelos

using EtecVeiculos.Api.Data;

namespace EtecVeiculos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarcaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Marca
        [HttpGet]
        public async Task<ActionResult<List<Marca>>> Get()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return Ok(marcas);
        }

        // GET: api/Marca/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marca>> Get(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return NotFound("Marca não encontrada");

            return Ok(marca);
        }

        // POST: api/Marca
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(marca);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = marca.Id }, marca);
            }
            return BadRequest("Verifique os dados informados!");
        }

        // PUT: api/Marca/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Edit(int id, Marca marca)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!_context.Marcas.Any(q => q.Id == id))
                        return NotFound("Marca não encontrada!");

                    if (id != marca.Id)
                        return BadRequest("Verifique os dados informados!");

                    _context.Entry(marca).State = EntityState.Modified;
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

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var marca = await _context.Marcas.FirstOrDefaultAsync(q => q.Id == id);
                if (marca == null)
                    return NotFound("Marca não encontrada");

                _context.Marcas.Remove(marca);
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
