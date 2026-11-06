using ChannelApp.Data;
using ChannelApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelApp.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Student>> GetAllAsync() =>
            await _context.Students.AsNoTracking().ToListAsync();

        public async Task<Student?> GetByIdAsync(Guid id) =>
            await _context.Students.FindAsync(id);

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        {
            var query = _context.Students.AsNoTracking()
                .Where(s => s.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
                query = query.Where(s => s.Id != excludeId.Value);
            return await query.AnyAsync();
        }
    }
}