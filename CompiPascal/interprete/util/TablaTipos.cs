using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.interprete.analizador.simbolo;

namespace CompiPascal.interprete.util
{
    class TablaTipos
    {

        public static Tipos[,] tipos = new Tipos[2, 2]
        {
            {Tipos.INT, Tipos.ERROR },
            {Tipos.ERROR, Tipos.BOOLEAN }
        };

        public static Tipos GetTipo(Tipo izquierda, Tipo derecha)
        {
            return tipos[(int)izquierda.tipo, (int)derecha.tipo];
        }
       
    }
}
