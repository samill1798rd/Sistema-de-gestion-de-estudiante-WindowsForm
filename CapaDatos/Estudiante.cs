using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Estudiante
    {
        [PrimaryKey, Identity]
        public int id { get; set; }

        public string nid { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public byte[] imagen { get; set; }

    }
}
