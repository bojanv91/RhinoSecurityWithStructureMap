using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoSecurityWithStructureMap
{
    public class User : Rhino.Security.IUser
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }

        public virtual Rhino.Security.SecurityInfo SecurityInfo
        {
            get { return new Rhino.Security.SecurityInfo(Username, Id); }
        }
    }
}
