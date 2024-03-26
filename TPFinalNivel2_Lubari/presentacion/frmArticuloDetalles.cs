using dominio;
using System;
using System.Windows.Forms;

namespace presentacion
{
    public partial class frmArticuloDetalles : Form
    {
        private Articulo articulo = null;
        public frmArticuloDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void fmrArticuloDetalles_Load(object sender, EventArgs e)
        {
            lblCategoriaArticulo.Text = articulo.Categoria.Descripcion;
            lblCodigoArticulo.Text = articulo.Codigo;
            lblDescripcionArticulo.Text = articulo.Descripcion;
            lblMarcaArticulo.Text = articulo.Marca.Descripcion;
            lblNombreArticulo.Text=articulo.Nombre;
            lblPrecioArticulo.Text = articulo.Precio.ToString();
            Helper.cargarImagen(articulo.ImagenUrl, pboxArticulo);
        }
    }
}
