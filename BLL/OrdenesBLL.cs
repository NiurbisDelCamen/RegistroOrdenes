using Microsoft.EntityFrameworkCore;
using RegistroOrden.DAL;
using RegistroOrden.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RegistroOrden.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Ordenes.Add(orden) != null)
                    paso = db.SaveChanges() > 0;
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Modificar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalle where OrdenId = {orden.OrdenId}");
                foreach (var item in orden.Detalles)
                {
                    db.Entry(orden).State = EntityState.Added;
                }
                db.Entry(orden).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.Ordenes.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Ordenes orden = new Ordenes();
            Contexto db = new Contexto();

            try
            {
                orden = db.Ordenes.Where(x => x.OrdenId == id).
                     Include(y => y.Detalles).SingleOrDefault();
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return orden;
        }
        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> orden)
        {
            List<Ordenes> lista = new List<Ordenes>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Ordenes.Where(orden).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }
    }
}
