using AlphaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Data
{
    public interface IStudent : ICrud<Student>
    {
        public Task<IEnumerable<Student>> SearchStudent(string keyword);
    }
}
