using SchoolDBModel.EntityTypes;
using SchoolDBModel.EntityTypes.ClassAndCourses;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.StudentServices
{
    public class StudentClassesService : IStudentClassesService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;
        private readonly IClassStudentRepository studentClassRepository;


        public StudentClassesService(IStudentRepository studentRepository, IClassRepository classRepository, IClassStudentRepository studentClassRepository)
        {
            this.studentRepository = studentRepository;
            this.classRepository = classRepository;
            this.studentClassRepository = studentClassRepository;
        }




        public IList<Class> GetClassesByStudentId(int id)
        {
            IList<ClassStudent> cl = this.studentClassRepository.GetClassIdByStudentId(id);
            IList<Class> clas = this.classRepository.GetAll();
            IList<Class> classes = clas.Join(cl, c => c.Id, p => p.ClassId, (c, p) => c).ToList();

            return classes;

        }

        public IList<Student> GetStudentsByClassId(int id)
        {
            IList<ClassStudent> cl = this.studentClassRepository.GetStudentsIdByClassId(id);
            IList<Student> stud = this.studentRepository.GetAll();
            IList<Student> students = stud.Join(cl, c => c.Id, p => p.StudentId, (c, p) => c).ToList();
            return students;
        }
    }
}
