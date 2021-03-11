using CompiPascal.interprete.analizador.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Parametro
    {
        public Simbolo simbolo;//tipo,id,valor[casteado]
        public string tipo;
        public bool isReferencia;

        public Parametro(Simbolo simbolo, bool isReferencia, string tipo)
        {
            this.simbolo = simbolo;
            this.isReferencia = isReferencia;
            this.tipo = tipo;
        }
    }
}
