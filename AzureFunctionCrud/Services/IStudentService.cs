using AzureFunctionCrud.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionCrud.Services
{
    public interface IStudentService
    {
        Task<List<Student>> Get();
        Task<Student> GetById(int id);
        Task<Student> Create(Student student);
        Task<Student> Edit(Student student);
        Task<bool> Delete(int id);
    }
}
