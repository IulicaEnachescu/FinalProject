using SchoolDBModel.EntityTypes;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Services
{

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        


        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            this._studentRepository = studentRepository;
            this._userRepository = userRepository;
      
        }

        public IList<Student> GetAllStudentsWithUser()
        {
            var _student = this._studentRepository.GetAll();
            IList <User> _studentsWithUser = this._userRepository.GetAll();
            IList<Student> _newList = new List<Student>();
            foreach (var item in _student)
            {
                User newS = _studentsWithUser.FirstOrDefault(x => x.Id == item.UserId);
                Student newStudent = StudentFromUser(newS);
                newStudent.Id = item.Id;
                newStudent.StatusActive = item.StatusActive;
                newStudent.ContactId = item.ContactId;
                _newList.Add(newStudent);
            }
            return _newList;
        }

        private static Student StudentFromUser(User newS)
        {
            return new Student
            {
                UserName = newS.UserName,
                FirstName = newS.FirstName,
                LastName = newS.LastName,
                Adress = newS.Adress,
                Category = newS.Category,
                Phone = newS.Password,
                Email = newS.Email,
                Password = newS.Password,
                City = newS.City,
                CreateDate = newS.CreateDate,
                DateBirth = newS.DateBirth,
            };
        }

        public Student GetStudentWithUser(int id)
        {
            var _student = this._studentRepository.GetById(id);
            User _user = this._userRepository.GetById(_student.UserId);
            var _studentWithUser = StudentFromUser(_user);
            _studentWithUser.Id = _student.Id;
            _studentWithUser.StatusActive = _student.StatusActive;
            _studentWithUser.ContactId = _student.ContactId;
            return _studentWithUser;
        }
      
    }
}
