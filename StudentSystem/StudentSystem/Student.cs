using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem
{
    public class Student
    {
        public string cedula;
        public string nombre;
        public int edad;

        public Student(string cedula, string nombre, int edad)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.edad = edad;
        }
    }
}
