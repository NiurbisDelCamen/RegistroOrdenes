using RegistroOrden.UI; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroOrden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void ClientesButton_Click(object sender, RoutedEventArgs e)
        {
            rClientes cClientes = new rClientes();
            cClientes.Show();
        }

        private void OrdenButton_Click(object sender, RoutedEventArgs e)
        {
            rOrdenes Rordenes = new rOrdenes();
            Rordenes.Show();
        }

        private void ProductoButton_Click(object sender, RoutedEventArgs e)
        {
            rProductos RProductos = new rProductos();
            RProductos.Show();
        }
    }
}
