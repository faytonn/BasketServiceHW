using Academy.Application.Dtos;
using Academy.Domain.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Academy.Application.Services.StudentService;

public interface IStudentService
{
    Task<StudentDto?> GetAsync(int id);

    Task<StudentDto?> GetAsync(Expression<Func<Student, bool>> predicate, Func<IQueryable<Student>, IIncludableQueryable<Student, object>>? include = null);

    Task<StudentListDto> GetListAsync(Expression<Func<Student, bool>>? predicate = null,
                                    Func<IQueryable<Student>, IOrderedQueryable<Student>>? orderBy = null,
                                    Func<IQueryable<Student>, IIncludableQueryable<Student, object>>? include = null,
                                    int index = 0, int size = 10, bool enableTracking = true);

    Task<StudentDto> AddAsync(StudentCreateDto createDto);
    Task<StudentDto> UpdateAsync(int id, StudentUpdateDto updateDto);
    Task<StudentDto> DeleteAsync(int id);
}
