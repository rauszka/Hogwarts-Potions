using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Models.Entities
{
    public interface IStudent
    {
        Task<Student> GetStudent(long id);
        Task<List<Student>> GetAllStudents();
    }
}
