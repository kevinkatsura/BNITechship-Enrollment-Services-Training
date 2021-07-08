using AlphaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Data
{
    public class StudentData : IStudent
    {
        private BNITechshipContext _db;

        public StudentData(BNITechshipContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            try
            {
                var result =  await GetById(id);
                if ( result != null )
                {
                    _db.Enrollments.RemoveRange(_db.Enrollments.Where(e => e.StudentId==result.Id));
                    _db.Students.Remove(result);
                    await _db.SaveChangesAsync();
                } else
                {
                    throw new Exception($"Tidak terdapat Student dengan ID: {id}");
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

        public async Task<IEnumerable<Student>> GetAll()
        {
            var result = await _db.Students.OrderBy(s => s.FirstMidName).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Student> GetById(string id)
        {
            var result = await _db.Students.Where(s => s.Id == Convert.ToInt32(id)).FirstOrDefaultAsync();
            var Enrollment = await _db.Enrollments.Where(e => e.StudentId == result.Id).ToListAsync();
            result.Enrollments = Enrollment;
            return result;
        }

        public async Task Insert(Student obj)
        {
            try
            {
                _db.Students.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Database Error: {e.Message}");
            } catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}" );
            }
        }

        public async Task Update(string id, Student obj)
        {
            try
            {
                var result = await GetById(id);
                if (result != null)
                {
                    result.FirstMidName = obj.FirstMidName;
                    result.LastName = obj.LastName;
                    result.EnrollmentDate = obj.EnrollmentDate;
                    await _db.SaveChangesAsync();
                } else
                {
                    throw new Exception($"Student dengan id: {id} tidak ditemukan.");
                }
            }
            catch (DbUpdateException e)
            {
                throw new Exception($"Database Error: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }

        public async Task<IEnumerable<Student>> SearchStudent(string keyword)
        {
            var students = await _db.Students.ToListAsync();
            List<Student> listStudent = new List<Student>();
            foreach (Student student in students)
            {
                if (await BoyerMoore.BoyerMooreHorsepool(keyword.ToLower(), $"{student.FirstMidName.ToLower()} {student.LastName.ToLower()}") == 0)
                {
                    listStudent.Add(student);
                }
            };
            return listStudent;
        }
    }
}
