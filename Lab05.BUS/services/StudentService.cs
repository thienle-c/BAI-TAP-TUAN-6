using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab05.BUS.services
{
    public class StudentService
    {
        public List<Student> GetAll()
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Students
                          .Include("Faculty")
                          .Include("Major")
                          .ToList();
            }
        }

        public List<Student> GetAllHasNoMajor()
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Students
                          .Where(p => p.MajorID == null)
                          .Include("Faculty")
                          .ToList();
            }
        }

        public List<Student> GetAllHasNoMajor(int facultyID)
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Students
                          .Where(p => p.MajorID == null && p.FacultyID == facultyID)
                          .Include("Faculty")
                          .ToList();
            }
        }

        public Student FindById(string studentID)
        {
            using (var ctx = new StudentModel())
            {
                return ctx.Students
                          .Include("Faculty")
                          .Include("Major")
                          .FirstOrDefault(p => p.StudentID == studentID);
            }
        }

        public Student FindStudentById(string studentID)
        {
            return FindById(studentID);
        }

        public void InsertOrUpdateStudent(Student student)
        {
            using (var ctx = new StudentModel())
            {
                var existingStudent = ctx.Students
                                         .FirstOrDefault(s => s.StudentID == student.StudentID);

                if (existingStudent != null)
                {
                    ctx.Entry(existingStudent).CurrentValues.SetValues(student);
                }
                else
                {
                    ctx.Students.Add(student);
                }

                ctx.SaveChanges();
            }
        }

        public void DeleteStudent(string studentID)
        {
            try
            {
                using (var ctx = new StudentModel())
                {
                    var student = ctx.Students.FirstOrDefault(s => s.StudentID == studentID);

                    if (student != null)
                    {
                        ctx.Students.Remove(student);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Student not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting student: {ex.Message}");
            }
        }

    }
}
