using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace CapaLogica.Library
{
    public class TextBoxEvent
    {
        public void textKeyPress(KeyPressEventArgs e)
        
        {
            //Condicion que solo nos permite ingresar datos de tipo texto.
            if (char.IsLetter(e.KeyChar)) { e.Handled = false; }
            //Condicion que solo no dar salto de liena cuando se oprime enter.
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //Condicion que nos permite utilizar latecla backspace. 
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //Condicion que nos permite utilizar la tecla de espacio.
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }
        public void numberKeyPress(KeyPressEventArgs e)
        {
            //Condicion que solo nos permite ingresar datos de tipo numero.
            if (char.IsDigit(e.KeyChar)) { e.Handled = false; }
            //Condicion que solo no dar salto de liena cuando se oprime enter.
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //Condicion que nos permite utilizar latecla backspace. 
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //Condicion que nos permite utilizar la tecla de espacio.
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }

        public bool comprobarFormatoEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
    
        }
    }
}
