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
    public class ProductoBLL
    {
        public static bool Guardar(Producto productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Productos.Add(productos) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Producto productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(productos).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
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
                var eliminar = db.Productos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Producto Buscar(int id)
        {
            Producto producto = new Producto();
            Contexto db = new Contexto();

            try
            {
                producto = db.Productos.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return producto;
        }

        public static List<Producto> GetList(Expression<Func<Producto, bool>> producto)
        {
            List<Producto> lista = new List<Producto>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Productos.Where(producto).ToList();
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
        public static bool DisminuirInventario(int id, int Cantidad)
        {
            bool paso = false;
            Contexto db = new Contexto();
            Producto producto = new Producto();
            producto = db.Productos.Find(id);
            
            if(producto !=null)

            try
            {
                    if (producto.Inventario >= 0)
                        producto.Inventario = (producto.Inventario - Cantidad);


                    db.Entry(producto).State = EntityState.Modified;
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
    }
}
