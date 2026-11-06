using ChannelApp.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelApp.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponse>> GetAllAsync();
        Task<StudentResponse?> GetByIdAsync(Guid id);
        Task<StudentResponse> CreateAsync(CreateStudentRequest request, string createdBy);
        Task<StudentResponse?> UpdateAsync(Guid id, UpdateStudentRequest request, string updatedBy);
        Task<bool> DeleteAsync(Guid id);
    }
}