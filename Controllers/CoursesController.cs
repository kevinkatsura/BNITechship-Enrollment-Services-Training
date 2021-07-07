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
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourse _course;
        public CoursesController(ICourse course)
        {
            _course = course;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            var results = await _course.GetAll();
            return results;
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<Course> Get(string id)
        {
            var result = await _course.GetById(id);
            return result;
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                await _course.Insert(course);
                return Ok($"Data Course {course.Title} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Course course)
        {
            try
            {
                await _course.Update(id, course);
                return Ok($"Data course ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _course.Delete(id);
                return Ok($"Data course {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/{keyword}")]
        public async Task<IEnumerable<Course>> SearchCourse(string keyword)
        {
            var results = await _course.SearchCourse(keyword);
            return results;

        }
    }
}
