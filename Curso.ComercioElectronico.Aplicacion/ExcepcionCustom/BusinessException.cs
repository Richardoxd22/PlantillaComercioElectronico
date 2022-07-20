using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ExcepcionCustom
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string FriendlyMessage { get; }
        public BusinessException(string friendlyMessage)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico) : base(mensajeTecnico)
        {
            FriendlyMessage = friendlyMessage;
        }

        public BusinessException(string friendlyMessage, string mensajeTecnico, Exception inner) : base(mensajeTecnico, inner)
        {
            FriendlyMessage = friendlyMessage;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}
