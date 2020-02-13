using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Utilitario
{
    public static class UtilitarioFront
    {
        /// <summary>
        /// Convierte 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            if (image != null)
            {
                BinaryReader read = new BinaryReader(image.InputStream);
                imageBytes = read.ReadBytes((int)image.ContentLength);
            }
            return imageBytes;
        }

        public static string RedimencionarImagen(string bytImagenOriginal, int intResolucionImagen)
        {
            byte[] bytImagen;
            int maxWidth = intResolucionImagen;
            int maxHeight = intResolucionImagen;

            if (maxWidth <= 0)
                return bytImagenOriginal;

            using (System.IO.MemoryStream mem = new System.IO.MemoryStream(Convert.FromBase64String(bytImagenOriginal), true))
            {
                using (System.Drawing.Image imagen = System.Drawing.Image.FromStream(mem))
                {
                    double ratioX = (double)maxWidth / imagen.Width;
                    double ratioY = (double)maxHeight / imagen.Height;
                    double ratio = Math.Min(ratioX, ratioY);
                    int newWidth = (int)(imagen.Width * ratio);
                    int newHeight = (int)(imagen.Height * ratio);
                    using (System.Drawing.Bitmap bm = new System.Drawing.Bitmap(newWidth, newHeight))
                    {
                        using (System.Drawing.Graphics grafico = System.Drawing.Graphics.FromImage(bm))
                        {
                            grafico.DrawImage(imagen, 0, 0, newWidth, newHeight);
                            using (System.IO.MemoryStream mem2 = new System.IO.MemoryStream())
                            {
                                bm.Save(mem2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                bytImagen = mem2.ToArray();
                            }
                        }
                    }
                }
            }
            string result = Convert.ToBase64String(bytImagen);
            return result;
        }

    }
}