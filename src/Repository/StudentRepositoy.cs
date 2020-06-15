using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Data;
using Repository.Interfaces;
using System.Linq;
using Domain.Models;

namespace Repository
{
  public class StudentRepository: IStudentRepository
  {
    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
      _context = context; 
    }
    public void Delete(int id)
    {
      var example = _context.ModelExamples.Find(id);
      _context.ModelExamples.Remove(example);
    }
    public async Task<IEnumerable<Student>> All()
    {
      return await Task.Run(() => _context.ModelExamples);
    }
    public async Task<Student>GetByIdAsync(int id)
    {
      return await _context.ModelExamples.FirstOrDefaultAsync(x => x.Id == id);
    }
    public void Save(Student entity)
    {
      _context.ModelExamples.Add(entity);
    }
  }
}