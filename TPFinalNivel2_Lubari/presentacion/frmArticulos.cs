using dominio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using negocio;

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
            cboxCampo.Items.Add("Precio");
            cboxCampo.Items.Add("Nombre");
            cboxCampo.Items.Add("Categoría");
            cboxCampo.Items.Add("Marca");
        }

        private void cargar()
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            try
            {
                articulos = negocio.listar();
                dtgvArticulos.DataSource = articulos;
                ocultarColumnas();
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
                pbArticulo.Load("https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1");
            }
        }

        private void dtgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dtgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dtgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void ocultarColumnas()
        {
            dtgvArticulos.Columns["ImagenUrl"].Visible = false;
            dtgvArticulos.Columns["Id"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtBuscar.Text;

            if(filtro.Length >= 2)
            {
                listaFiltrada = articulos.FindAll(a => a.Nombre.ToLower().Contains(filtro.ToLower())
                                                    || a.Categoria.Descripcion.ToLower().Contains(filtro.ToLower())
                                                    || a.Marca.Descripcion.ToLower().Contains(filtro.ToLower()));
            }
            else
            {
                listaFiltrada = articulos;
            }

            dtgvArticulos.DataSource = null;
            dtgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if (!validarLista())
            {
                return;
            }
            Articulo seleccionado;
            seleccionado = (Articulo)dtgvArticulos.CurrentRow.DataBoundItem;
            frmArticuloDetalles detalles = new frmArticuloDetalles(seleccionado);
            detalles.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!validarLista())
            {
                return;
            }
            eliminar();
        }

        private void eliminar()
        {   
            ArticulosNegocio negocio = new ArticulosNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dtgvArticulos.CurrentRow.DataBoundItem;

                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void cboxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboxCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cboxCriterio.Items.Clear();
                cboxCriterio.Items.Add("Mayor a");
                cboxCriterio.Items.Add("Menor a");
                cboxCriterio.Items.Add("Igual a");
            }
            else
            {
                cboxCriterio.Items.Clear();
                cboxCriterio.Items.Add("Comienza con");
                cboxCriterio.Items.Add("Termina con");
                cboxCriterio.Items.Add("Contiene");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();

            try
            {
                if (validarFiltro())
                    return;

                string campo = cboxCampo.SelectedItem.ToString();
                string criterio = cboxCriterio.SelectedItem.ToString();
                string filtro = txtboxFiltro.Text;
                dtgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool validarFiltro()
        {
            if (cboxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return true;
            }
            if (cboxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return true;
            }
            if (cboxCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtboxFiltro.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para numéricos...");
                    return true;
                }
                if (!(soloNumeros(txtboxFiltro.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por un campo numérico...");
                    return true;
                }
            }
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!validarLista())
            {
                return;
            }
            Articulo seleccionado;
            seleccionado = (Articulo)dtgvArticulos.CurrentRow.DataBoundItem;
            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private bool validarLista()
        {
            if (dtgvArticulos.RowCount < 1)
            {
                MessageBox.Show("Seleccione un artículo.");
                return false;
            }
            return true;
        }
    }
}
