using System;
using System.ComponentModel.DataAnnotations;

namespace ChannelApp.DTOs
{
    public record CreateStudentRequest
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string FullName { get; init; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; init; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Department { get; init; } = string.Empty;
    }

    public record UpdateStudentRequest
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string FullName { get; init; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; init; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Department { get; init; } = string.Empty;
    }

    public record StudentResponse
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public int Age { get; init; }
        public string Department { get; init; } = string.Empty;
        public DateTime EnrollmentDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; } = string.Empty;
        public DateTime? UpdatedAt { get; init; }
        public string? UpdatedBy { get; init; }
    }
}