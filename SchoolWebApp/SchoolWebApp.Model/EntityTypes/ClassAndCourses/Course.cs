using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class Course : EntityBase
    {
        //public IList<Class> classes { get; set; } = new List<Class>();

        public int NumberOfLessons { get; set; }
        public string Description { get; set; }
        public CategoryTypes Category { get; set; }
        public LanguageTypes Language { get; set; }
        public LevelTypes Level { get; set; }

        public bool StatusActive { get; set; }

        //public List<Course> AllCourses { get; set; } = new List<Course>();

    }
    

    public enum CategoryTypes
    {
        Children, Adults, Teens
    }
    public enum LanguageTypes
    {
        English, French, German
    }
    public enum LevelTypes
        {
            A1Beginer,
            A2Elementary,
            B1Intermediate,
            B2UpperIntermediate,
            B2PreAdvance,
            C1Advance,
            C2Profiency
        }

    
}
