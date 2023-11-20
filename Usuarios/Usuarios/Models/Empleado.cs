using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Usuarios.Models
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [MaxLength(100)]
        public int Telefono { get; set; }

        [MaxLength(100)]
        public int Edad { get; set; }

        [MaxLength(100)]
        public string CURP { get; set; }

        public string TipoEmpleado { get; set; }

    }

    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int IdCurso { get; set; }
        [MaxLength(100)]
        public string NombreDelCurso { get; set; }
        [MaxLength(100)]
        public string TipoDeCurso { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        [MaxLength(100)]
        public int Horas { get; set; }

    }

    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Emailpassword { get; set; }
        [MaxLength(100)]
        public string NombreCompleto { get; set; }
        [MaxLength(100)]
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class SCurso
    {
        [PrimaryKey, AutoIncrement]
        public int IdEmpleado { get; set; }
        [Indexed]
        [MaxLength(100)]
        public string NombreEmp {  get; set; }
        [MaxLength(100)]
        public string NombreCurso { get; set; }
        [MaxLength(100)]
        public string Lugar { get; set; }
        [MaxLength(100)]
        public string Fecha { get; set; }
        [MaxLength(100)]
        public string Hora { get; set; }
        [MaxLength(100)]
        public string Estatus {  get; set; }
        [MaxLength(100)]
        public int Calificacion { get; set; }

    }


}
