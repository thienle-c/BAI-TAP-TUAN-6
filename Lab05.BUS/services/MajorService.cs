using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05.BUS.services
{


    public class MajorService
    {
        public List<Major> GetAllByFaculty(int facultyID)
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Majors.Where(p => p.FacultyID == facultyID).ToList();
            }
        }
    }
    }
