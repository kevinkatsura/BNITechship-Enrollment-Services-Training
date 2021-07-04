using AlphaAPI.Data;
using AlphaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollment _enrollment;

        public EnrollmentsController(IEnrollment enrollment)
        {
            _enrollment = enrollment;
        }   

        [HttpGet]
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments() {
            var results = await _enrollment.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<Enrollment> GetById(string id) {
            var result = await _enrollment.GetById(id);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> InsertEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                await _enrollment.Insert(enrollment);
                return Ok("Enrollment berhasil ditambahkan");
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(string id, [FromBody] Enrollment enrollment) {
            try
            {
                await _enrollment.Update(id, enrollment);
                return Ok($"Enrollment dengan ID: {id} berhasil diupdate");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(string id) {
            try
            {
                await _enrollment.Delete(id);
                return Ok($"Enrollment dengan ID: {id} berhasil dihapus");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
