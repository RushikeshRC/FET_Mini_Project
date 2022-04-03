using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_MiniProject.Models
{
    interface IUpdateable
    {
        
            DateTime CreatedDate { get; set; }
            DateTime UpdatedDate { get; set; }
            //public string ModifiedBy { get; set; }
    }
}
