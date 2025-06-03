using Microsoft.EntityFrameworkCore;
using RBDProject.Models;

namespace RBDProject.Controllers
{
    public class RBDGrupoesControllers
    {
        private readonly BdrbdContext _context;

        public RBDGrupoesControllers()
        {
            _context = new BdrbdContext();
        }

        //TRAER TODAS LOS GRUPOS
        public async Task<List<RbdGrupo>> GetAll()
        {
            return await _context.RbdGrupos.ToListAsync();
        }

        //BUSCAR UN ELEMENTO POR ID
        public async Task<RbdGrupo> GetById(int id)
        {
            return await _context.RbdGrupos.FirstOrDefaultAsync(g => g.CodGrup.Equals(id));
        }

        public async Task Update(RbdGrupo grupo)
        {
            var result = await _context.RbdGrupos.FirstOrDefaultAsync(g => g.CodGrup == grupo.CodGrup);

            _context.RbdGrupos.Update(grupo);
            await _context.SaveChangesAsync();
        }

        public async Task AddGrupo(RbdGrupo grupo)
        {
            await _context.RbdGrupos.AddAsync(grupo);
            await _context.SaveChangesAsync();
        }
    }
}
