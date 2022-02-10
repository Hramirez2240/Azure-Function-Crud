using AzureFunctionCrud.Models;
using AzureFunctionCrud.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionCrud.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> Get()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            var entity = await _context.Students.FindAsync(id);

            if (entity is null) { return null; }

            return entity;
        }

        public async Task<Student> Create(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student> Edit(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Students.FindAsync(id);

            if(entity is null) { return false; }

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
