using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    internal class Helper
    {
        public static void cargarImagen(string imagen, PictureBox pictureBox)
        {
            try
            {
                pictureBox.Load(imagen);
            }
            catch (Exception ex)
            {
                pictureBox.Load("https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1");
            }
        }

        public static bool soloNumeros(string cadena)
        {   if (cadena.Length == 0) return false;
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)) && !caracter.Equals(",") && !caracter.Equals("."))
                    return false;
            }
            return true;
        }

        public static bool esDecimal(string cadena)
        {
            try
            {
                decimal.Parse(cadena);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
