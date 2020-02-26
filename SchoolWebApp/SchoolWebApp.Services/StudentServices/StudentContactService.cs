using SchoolDBModel.EntityTypes;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.StudentServices
{
    public class StudentContactService : IStudentContactService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IContactPersonRepository contactRepository;



        public StudentContactService(IStudentRepository studentRepository, IContactPersonRepository contactRepository)
        {
            this.studentRepository = studentRepository;
            this.contactRepository = contactRepository;
        }

        public ContactPerson GetContactPersonByStudentId(int id)
        {
            Student student = this.studentRepository.GetById(id);
            var allContacts = this.contactRepository.GetAll();
            var contact = allContacts.FirstOrDefault(x => x.Id == student.ContactId);
            return contact;
        }

    }        
         
}
