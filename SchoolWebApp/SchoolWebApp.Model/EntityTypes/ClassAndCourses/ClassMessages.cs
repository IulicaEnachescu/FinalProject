using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
   public class ClassMessages : EntityBase
    {
        public int ClassId { get; set; }
       
        public System.DateTime Date { get; set; }
        public string MessageBody { get; set; }


      //  public IList<Message> MessagesPerClass { get; set; } = new List<Message>();
    }
}
