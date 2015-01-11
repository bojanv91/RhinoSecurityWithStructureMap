using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Security.Interfaces;

namespace RhinoSecurityWithStructureMap
{
    public class DefaultEntityInformationExtractor<T> : IEntityInformationExtractor<T>
    {
        public Guid GetSecurityKeyFor(T entity)
        {
            return new Guid();
        }

        public string GetDescription(Guid securityKey)
        {
            return null;
        }

        public string SecurityKeyPropertyName
        {
            get { return null; }
        }
    }
}
