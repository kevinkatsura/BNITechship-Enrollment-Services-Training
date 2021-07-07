using AlphaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaAPI.Data
{
    public interface ICourse : ICrud<Course>
    {
        public Task<IEnumerable<Course>> SearchCourse(string keyword);
    }
}
