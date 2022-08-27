using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models.Implementations;

public class StudentIMP : IStudent
{
    private HogwartsContext _context;

    public StudentIMP(HogwartsContext context)
    {
        _context = context;
    }
    public async Task<Student> GetStudent(long id)
    {
        return await _context.Students
            .Include(s => s.Room)
            .FirstAsync(student => student.ID == id);
    }

    public async Task<List<Student>> GetAllStudents()
    {
        return await _context.Students
            .Include(s => s.Room)
            .ToListAsync();
    }
}