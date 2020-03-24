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
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        Clientes cliente = new Clientes();
        public rClientes()
        {
            InitializeComponent();
            this.DataContext = cliente;
            ClienteIdTextBox.Text = "0";
        }
        private void Limpiar()
        {
            ClienteIdTextBox.Text = "0";
            NombresTextBox.Text = string.Empty;
        }
        private bool ExisteEnDb()
        {
            Clientes clientes = ClientesBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            return (clientes != null);
        }
        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = cliente;
        }


        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Clientes anterior = ClientesBLL.Buscar(int.Parse(ClienteIdTextBox.Text));

            if (cliente != null)
            {
                cliente = anterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show(" No encontrado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(ClienteIdTextBox.Text, out id);



            if (ClientesBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado con exito!!!", "ELiminado", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show(" No eliminado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool Validar()
        {
            bool paso = true;
            if(string.IsNullOrWhiteSpace(NombresTextBox.Text))
            {
                paso = false;
                MessageBox.Show("El Campo Nombre no puede estar vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return paso;
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (String.IsNullOrEmpty(ClienteIdTextBox.Text) || ClienteIdTextBox.Text == "0")
                paso = ClientesBLL.Guardar(cliente);
            else
            {
                if (!ExisteEnDb())
                {
                    MessageBox.Show("No existe el cliente en la base de " +
                        "datos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                paso = ClientesBLL.Modificar(cliente);
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

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
