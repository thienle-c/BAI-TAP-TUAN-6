using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS.services
{

    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Faculties.ToList();
            }
        }
    }
}
