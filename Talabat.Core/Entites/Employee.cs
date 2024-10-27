using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class Employee :BaseEntity
    {
        public string Name { get; set; }

        public decimal Salary { get; set; }
        public int? Age { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; } //Navigationa property
    }
}
