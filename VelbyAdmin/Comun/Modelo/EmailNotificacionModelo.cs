using System;

namespace Comun.Modelo
{
    [Serializable]
    public class EmailNotificacionModelo
    {
        public EmailNotificacionModelo()
        {
        }
        public string Asunto { get; set; }
        public string CueporMensaje { get; set; }
        public string CorreoDestino { get; set; }
    }
}
