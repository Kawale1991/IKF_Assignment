using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IKF.BAIKF
{
    public class BOIKF
    {
        public Int32  ID { get; set; }
        
        public String Name { get; set; }

        public DateTime Dob { get; set; }

        public String Skills { get; set; }

        public String Designation { get; set; }

        public String Action { get; set; }

        public Boolean Rcount { get; set; }

        public DataTable rdt { get; set; }
    }
}