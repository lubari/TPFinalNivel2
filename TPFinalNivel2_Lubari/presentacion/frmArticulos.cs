using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using static System.Net.Mime.MediaTypeNames;

namespace presentacion
{
    public partial class frmArticulos : Form
    {   
        private List<Articulo> articulos;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            try
            {
                articulos = negocio.listar();
                dtgvArticulos.DataSource = articulos;
                cargarImagen(articulos[0].ImagenUrl);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string url)
        {
            try
            {
                pbArticulo.Load(url);
            }
            catch (Exception ex)
            {
                pbArticulo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }
    }
}
