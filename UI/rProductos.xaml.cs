using RegistroOrden.BLL;
using RegistroOrden.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroOrden.UI
{
    /// <summary>
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        Producto producto = new Producto();
        public rProductos()
        {
            InitializeComponent();
            this.DataContext = producto;
            ProductoIdTextBox.Text = "0";
        }
        private void Limpiar()
        {
            ProductoIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            PrecioTextBox.Text = "0";
            InventarioTextBox.Text = "0";
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool ExisteEnDb()
        {
            Producto producto = ProductoBLL.Buscar(Convert.ToInt32(ProductoIdTextBox.Text));

            return (producto != null);
        }
        
        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                paso = false;
                MessageBox.Show("El campo descripcion no puede estar vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                DescripcionTextBox.Focus();


            }

            if (string.IsNullOrEmpty(PrecioTextBox.Text))
            {
                paso = false;
                MessageBox.Show("El campo precio no puede estar vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                PrecioTextBox.Focus();
            }
            if (string.IsNullOrEmpty(InventarioTextBox.Text))
            {
                paso = false;
                MessageBox.Show("El campo inventario no puede estar vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                InventarioTextBox.Focus();
            }

            return paso;
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (String.IsNullOrEmpty(ProductoIdTextBox.Text) || ProductoIdTextBox.Text == "0")
                paso = ProductoBLL.Guardar(producto);

            else
            {
                if (!ExisteEnDb())
                {
                    MessageBox.Show("No existe el cliente en la base de " +
                        "datos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                paso = ProductoBLL.Modificar(producto);
            }

            if (paso)
            {
                MessageBox.Show("Guardado!!", "EXITO", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show(" No guardado!!", "Informacion", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = producto;
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(ProductoIdTextBox.Text, out id);



            if (ProductoBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado con exito!!!", "ELiminado", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show(" No eliminado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Producto anterior = ProductoBLL.Buscar(int.Parse(ProductoIdTextBox.Text));

            if (producto != null)
            {
                producto = anterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show(" No encontrado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
