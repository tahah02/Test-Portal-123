using ChannelApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelApp.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
    }
}