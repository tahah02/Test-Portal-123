using ChannelApp.DTOs;
using ChannelApp.Models;
using ChannelApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task<IEnumerable<StudentResponse>> GetAllAsync()
        {
            var students = await _repo.GetAllAsync();
            return students.Select(ToResponse);
        }

        public async Task<StudentResponse?> GetByIdAsync(Guid id)
        {
            var student = await _repo.GetByIdAsync(id);
            return student is null ? null : ToResponse(student);
        }

        public async Task<StudentResponse> CreateAsync(CreateStudentRequest request, string createdBy)
        {
            if (await _repo.EmailExistsAsync(request.Email))
                throw new InvalidOperationException("Email already exists.");

            var student = new Student
            {
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim().ToLowerInvariant(),
                Age = request.Age,
                Department = request.Department.Trim(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                UpdatedAt = null,
                UpdatedBy = null
            };

            await _repo.AddAsync(student);
            return ToResponse(student);
        }

        public async Task<StudentResponse?> UpdateAsync(Guid id, UpdateStudentRequest request, string updatedBy)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student is null) return null;

            if (await _repo.EmailExistsAsync(request.Email, id))
                throw new InvalidOperationException("Email already exists.");

            student.FullName = request.FullName.Trim();
            student.Email = request.Email.Trim().ToLowerInvariant();
            student.Age = request.Age;
            student.Department = request.Department.Trim();
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = updatedBy;

            await _repo.UpdateAsync(student);
            return ToResponse(student);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student is null) return false;
            await _repo.DeleteAsync(student);
            return true;
        }

        private static StudentResponse ToResponse(Student s) => new StudentResponse
        {
            Id = s.Id,
            FullName = s.FullName,
            Email = s.Email,
            Age = s.Age,
            Department = s.Department,
            EnrollmentDate = s.EnrollmentDate,
            CreatedAt = s.CreatedAt,
            CreatedBy = s.CreatedBy,
            UpdatedAt = s.UpdatedAt,
            UpdatedBy = s.UpdatedBy
        };
    }
}