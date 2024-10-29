using Academy.Application.Dtos;
using Academy.Domain.Entities;
using Academy.Domain.Enums;
using Academy.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Academy.Application.Services.StudentService;

public class StudentManager : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentManager(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<StudentDto> AddAsync(StudentCreateDto createDto)
    {
        var studentEntity = _mapper.Map<Student>(createDto);
        var createdStudent = await _studentRepository.AddAsync(studentEntity);
        return _mapper.Map<StudentDto>(createdStudent);
    }

    public async Task<StudentDto> DeleteAsync(int id)
    {
        var existStudent = await _studentRepository.GetAsync(id);

        if (existStudent == null) throw new Exception("Not found");

        var deletedStudent = await _studentRepository.DeleteAsync(existStudent);

        return _mapper.Map<StudentDto>(deletedStudent);
    }

    public async Task<StudentDto?> GetAsync(int id)
    {
        var studentEntity = await _studentRepository.GetAsync(id);

        if (studentEntity == null) throw new Exception("Not found");

        return _mapper.Map<StudentDto>(studentEntity);
    }

    public async Task<StudentDto?> GetAsync(Expression<Func<Student, bool>> predicate, Func<IQueryable<Student>, IIncludableQueryable<Student, object>>? include = null)
    {
        var studentEntity = await _studentRepository.GetAsync(predicate, include);

        if (studentEntity == null) throw new Exception("Not found");

        return _mapper.Map<StudentDto>(studentEntity);
    }

    public async Task<StudentListDto> GetListAsync(Expression<Func<Student, bool>>? predicate = null, Func<IQueryable<Student>, IOrderedQueryable<Student>>? orderBy = null, Func<IQueryable<Student>, IIncludableQueryable<Student, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
    {
        var studentListEntity = await _studentRepository.GetListAsync(predicate, orderBy, include, index, size, enableTracking);

        if (studentListEntity == null) throw new Exception("Not found");

        return _mapper.Map<StudentListDto>(studentListEntity);
    }

    public async Task<StudentDto> UpdateAsync(int id, StudentUpdateDto updateDto)
    {
        var existStudent = await _studentRepository.GetAsync(id);

        if (existStudent == null) throw new Exception("Not found");

        existStudent = _mapper.Map(updateDto, existStudent);

        var updatedStudent = await _studentRepository.UpdateAsync(existStudent);

        return _mapper.Map<StudentDto>(updatedStudent);
    }
}
