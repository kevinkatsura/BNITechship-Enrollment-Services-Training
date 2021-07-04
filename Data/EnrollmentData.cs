using AlphaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Data
{
    public class EnrollmentData : IEnrollment
    {
        private BNITechshipContext _db;

        public EnrollmentData(BNITechshipContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            try
            {
                var result = await GetById(id);
                if ( result != null )
                {
                    _db.Enrollments.Remove(result);
                    await _db.SaveChangesAsync();
                } else
                {
                    throw new Exception($"Enrollment dengan ID: {id} tidak ditemukan");
                }
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Database Error: {e.Message}");
            } catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _db.Enrollments.Include(e => e.Course).Include(e => e.Student).OrderBy(e => e.CourseId).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(string id)
        {
            var result = await _db.Enrollments.Where(e => e.EnrollmentId == Convert.ToInt32(id)).FirstOrDefaultAsync();
            var course = await _db.Courses.Where(c => c.CourseId == result.CourseId).FirstOrDefaultAsync();
            var student = await _db.Students.Where(s => s.Id == result.StudentId).FirstOrDefaultAsync();
            result.Course = course;
            result.Student = student;
            return result;
        }

        public async Task Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Database Error: {e.Message}");
            } catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }

        public async Task Update(string id, Enrollment obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.CourseId = obj.CourseId;
                    result.StudentId = obj.StudentId;
                    result.Grade = obj.Grade;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Enrollment dengan ID: {id} tidak ditemukan");
                }
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Database Error: {e.Message}");
            } catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }
    }
}
