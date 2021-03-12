using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica.Library
{
    public class Uploadimagen
    {
        private OpenFileDialog fd = new OpenFileDialog();

        public void CargarImagen(PictureBox pictureBox)
        {
            pictureBox.WaitOnLoad = true;
            fd.Filter = "Images|*.jpg;*.gif;*.png;*.bmp";
            fd.ShowDialog();
            if (fd.FileName != string.Empty)
            {
                pictureBox.ImageLocation = fd.FileName;
            }
        }

        public byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}
