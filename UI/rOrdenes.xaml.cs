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
    /// Interaction logic for rOrdenes.xaml
    /// </summary>
    public partial class rOrdenes : Window
    {
        public List<OrdenesDetalle> Detalles { get; set; }
        public rOrdenes()
        {
            InitializeComponent();
            this.Detalles = new List<OrdenesDetalle>();
            CargarGrid();


        }
        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            MontoTextBox.Text = "0";
            FechaDatePickerTextBox.Text = "Fecha de orden";

            this.Detalles = new List<OrdenesDetalle>();
            CargarGrid();
        }

        private void CargarGrid() //Metodo para cargar el Grid
        {
            OrdenesDataGrid.ItemsSource = null;
            OrdenesDataGrid.ItemsSource = this.Detalles;
        }

     


            private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            Ordenes orden;

            if (!Validar())
                return;

            orden = LlenaClase();

            if (string.IsNullOrEmpty(OrdenIdTextBox.Text) || OrdenIdTextBox.Text == "0")
                paso = OrdenesBLL.Guardar(orden);
            else
            {
                if (!ExisteEnDb())
                {
                    MessageBox.Show("No existe el cliente en la base de " +
                        "datos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                paso = OrdenesBLL.Modificar(orden);
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

        private bool Validar()
        {
            bool paso = true;
            if(string.IsNullOrEmpty(FechaDatePickerTextBox.Text))
            {
                MessageBox.Show("El Campo Fecha no puede estar vacio", "Aviso",MessageBoxButton.OKCancel,MessageBoxImage.Information);
                FechaDatePickerTextBox.Focus();
                paso = false;
            }
            if(string.IsNullOrEmpty(MontoTextBox.Text))
            {
                MessageBox.Show("El Campo Monto no puede estar vacio","Aviso",MessageBoxButton.OKCancel,MessageBoxImage.Information);
                MontoTextBox.Focus();
                paso = false;
            }
            return paso;
        }
        private Ordenes LlenaClase()
        {
            Ordenes orden = new Ordenes();
            orden.OrdenId = int.Parse(OrdenIdTextBox.Text);
            orden.ClienteId = int.Parse(ClienteIdTextBox.Text);
            orden.Fecha = (DateTime)FechaDatePickerTextBox.SelectedDate;
            orden.Monto = decimal.Parse(MontoTextBox.Text);
           
            orden.Detalles = this.Detalles;

            return orden;
        }
        private void LlenaCampo(Ordenes orden)
        {
           OrdenIdTextBox.Text = Convert.ToString(orden.OrdenId);
            FechaDatePickerTextBox.SelectedDate = orden.Fecha;
            MontoTextBox.Text = Convert.ToString(orden.Monto);
            ClienteIdTextBox.Text = Convert.ToString(orden.ClienteId);

            this.Detalles = orden.Detalles;
            CargarGrid();
        }
        private bool ExisteEnDb()
        {
            Ordenes orden = OrdenesBLL.Buscar(Convert.ToInt32(OrdenIdTextBox.Text));

            return (orden != null);
        }
    
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Ordenes orden = new Ordenes();
            int.TryParse(OrdenIdTextBox.Text, out id);

            orden = OrdenesBLL.Buscar(id);

            if (orden != null)
            {
                LlenaCampo(orden);
            }
            else
            {
                MessageBox.Show(" No encontrado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
       
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(OrdenIdTextBox.Text, out id);



            if (OrdenesBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado con exito!!!", "ELiminado", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show(" No eliminado !!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesDataGrid.Items.Count > 0 && OrdenesDataGrid.SelectedItem != null)
            {
                Detalles.RemoveAt(OrdenesDataGrid.SelectedIndex);
                CargarGrid();
            }
        }



        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesDataGrid.ItemsSource != null)
            {
                this.Detalles = (List<OrdenesDetalle>)OrdenesDataGrid.ItemsSource;
            }
            this.Detalles.Add(new OrdenesDetalle
            {
                OrdenId = Convert.ToInt32(OrdenIdTextBox.Text),
                ProductoId = Convert.ToInt32(ProductoTextBox.Text),
                Cantidad = Convert.ToInt32(CantidadTextBox.Text)

            });
            CargarGrid();
            int valor = Convert.ToInt32(CantidadTextBox.Text);
            int id = Convert.ToInt32(ProductoTextBox.Text);
            ProductoBLL.DisminuirInventario(id, valor);
            ProductoTextBox.Text = string.Empty;
            CantidadTextBox.Text = string.Empty;
        }
    }
    
}
