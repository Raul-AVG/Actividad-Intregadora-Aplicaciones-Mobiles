using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Usuarios;
using Usuarios.Models;
using Xamarin.Essentials;

namespace Usuarios.Data
{
    public class SQLiteHelper
    {

        readonly SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleado>().Wait();
            db.CreateTableAsync<Curso>().Wait();
            db.CreateTableAsync<Users>().Wait();
            db.CreateTableAsync<SCurso>().Wait();
        }

        // Registro de Empleados
        public Task<int> SaveEmpleadoAsync(Empleado emp)
        {
            if (emp.IdEmpleado != 0)
            {
                return db.UpdateAsync(emp);
            }
            else
            {
                return db.InsertAsync(emp);
            }
        }

        public Task<List<Empleado>> GetEmpleadosAsync()
        {
            return db.Table<Empleado>().ToListAsync();
        }

        public Task<Empleado> GetEmpleadoById(int idEmpleado)
        {
            return db.Table<Empleado>().Where(a => a.IdEmpleado == idEmpleado).FirstOrDefaultAsync();
        }

        public Task<int> DeleteEmpleadoAsync(Empleado empleado)
        {
            return db.DeleteAsync(empleado);
        }




        // REGRISTRO DE CURSOS
        public Task<int> SaveCursosAsync(Curso curs)
        {
            if (curs.IdCurso != 0)
            {
                return db.UpdateAsync(curs);
            }
            else
            {
                return db.InsertAsync(curs);
            }
        }

        public Task<List<Curso>> GetCursosAsync()
        {
            return db.Table<Curso>().ToListAsync();
        }

        public Task<Curso> GetCursosById(int idCurso)
        {
            return db.Table<Curso>().Where(a => a.IdCurso == idCurso).FirstOrDefaultAsync();
        }

        public Task<int> DeleteCursosAsync(Curso curso)
        {
            return db.DeleteAsync(curso);
        }

        // REGISTRO DE USUARIOS (LOGIN)

        public Task<int> SaveUserAsync(Users usr)
        {
            if (usr.UserId != 0)
            {
                return db.UpdateAsync(usr);
            }
            else
            {
                return db.InsertAsync(usr);
            }
        }

        public Task<List<Users>> GetUsersAsync()
        {

            return db.Table<Users>().ToListAsync();
        }

        public Task<Users> GetUsersById(int userId)
        {
            return db.Table<Users>().Where(a => a.UserId == userId).FirstOrDefaultAsync();
        }

        public Task<int> DeleteUsersAsync(Users users)
        {
            return db.DeleteAsync(users);
        }
        public Task<Users> ObtenerUsuarioPorEmail(string email)
        {
            return db.Table<Users>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        // Registro de Seguimiento de cursos
        public Task<int> SaveSCursoAsync(SCurso scu)
        {
            if (scu.IdEmpleado != 0)
            {
                return db.UpdateAsync(scu);
            }
            else
            {
                return db.InsertAsync(scu);
            }
        }

        public Task<List<SCurso>> GetSCursoAsync()
        {
            return db.Table<SCurso>().ToListAsync();
        }

        public Task<SCurso> GetSCursoById(int idEmpleado)
        {
            return db.Table<SCurso>().Where(a => a.IdEmpleado == idEmpleado).FirstOrDefaultAsync();
        }

        public Task<int> DeleteSCursoAsync(SCurso scurso)
        {
            return db.DeleteAsync(scurso);
        }



    }
}
