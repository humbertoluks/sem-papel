using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interfaces;

namespace Repository
{
  public class GuiaRepository : IGuiaRepository
  {
    private readonly DataContext _context;
    public GuiaRepository(DataContext context)
    {
      _context = context; 
    }
    public async void Delete(int id)
    {
      var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == id);

      if (guia != null)
        _context.Guias.Remove(guia);
    }
    public async Task<IEnumerable<Guia>> All()
    {
      return await _context.Guias
        .Include(t => t.GuiaTipo)
        .Include(s => s.GuiaStatus)
        .Include(sc => sc.GuiaStatusCheckIns)
        .AsNoTracking()
        .ToListAsync();
    }

    public Task<Guia> GetByIdAsync(int id)
    {
      return _context.Guias.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
    }

    public void Save(Guia entity)
    {
      _context.Guias.Add(entity);
    }
  }
}