using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctionCrud.Services;
using AzureFunctionCrud.Models;

namespace AzureFunctionCrud
{
    public class StudentFunction : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentFunction(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [FunctionName("GetStudents")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "student")] HttpRequest req,
            ILogger log)
        {
            var entities =  await _studentService.Get();
            return Ok(entities);
        }

        [FunctionName("GetStudent")]
        public async Task<IActionResult> GetById(int id, 
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "student/{id}"
            )] HttpRequest req, ILogger log)
        {
            var entity = await _studentService.GetById(id);

            if (entity is null) return NotFound("Not Found");

            return Ok(entity);
        }

        [FunctionName("CreateStudent")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "student")]
            HttpRequest req, ILogger log)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            Student student = JsonConvert.DeserializeObject<Student>(content);

            var entityAdded = await _studentService.Create(student);

            if (entityAdded is null) return NotFound("NotFound");

            return Ok(entityAdded);
        }

        [FunctionName("UpdateStudent")]
        public async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "student/{id}"
            )]HttpRequest req, ILogger log)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            Student student = JsonConvert.DeserializeObject<Student>(content);

            student = await _studentService.Edit(student);

            if (student is null) return NotFound("NotFound");

            return Ok(student);
        }

        [FunctionName("DeleteStudent")]
        public async Task<IActionResult> Delete(int id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "student/{id}"
            )]HttpRequest req, ILogger log)
        {
            await _studentService.Delete(id);

            return Ok();
        }
    }
}
