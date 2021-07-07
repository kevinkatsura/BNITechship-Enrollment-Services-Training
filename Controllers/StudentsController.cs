using AlphaAPI.Data;
using AlphaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private IStudent _student;

        public StudentsController(IStudent student)
        {
            _student = student;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var result = await _student.GetAll();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(String id)
        {
            var result = await _student.GetById(id);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> InsertStudent([FromBody] Student student)
        {
            try
            {
                await _student.Insert(student);
                return Ok($"Student dengan nama {student.FirstMidName} {student.LastName} berhasil ditambahkan.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] Student student)
        {
            try
            {
                await _student.Update(id, student);
                return Ok($"Student dengan id: {id} berhasil diupdate.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                await _student.Delete(id);
                return Ok($"Student dengan id: {id} berhasil dihapus.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("search/{keyword}")]
        public async Task<IEnumerable<Student>> SearchStudent(string keyword)
        {
            return await _student.SearchStudent(keyword);
        }
    }
}
