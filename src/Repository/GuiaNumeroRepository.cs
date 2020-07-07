using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Domain.Models;
using Repository.Data;
using Repository.Interfaces;

namespace Repository
{
  public class GuiaNumeroRepository : IGuiaNumeroRepository
  {
    private readonly DataContext _context;
    public GuiaNumeroRepository(DataContext context)
    {
      _context = context; 
    }
    
    public async Task<int> GetLastGuiaIdAsync(string codigoPrestador)
    {
      var guideNumber = await _context.GuiaNumeros.FirstOrDefaultAsync(g => g.CodigoPrestador == codigoPrestador);
      
      if(guideNumber == null)
      {
          _context.GuiaNumeros.Add(guideNumber = new GuiaNumero{
            CodigoPrestador = codigoPrestador,
            Numero = 1
          });

          _context.SaveChanges();

    	    return guideNumber.Numero;
      } 

      guideNumber.Numero = ++guideNumber.Numero;
      _context.SaveChanges();
      
      return guideNumber.Numero;
    }
  }
}