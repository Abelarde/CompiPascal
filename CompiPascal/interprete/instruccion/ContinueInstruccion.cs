using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ContinueInstruccion : Instruccion
    {
        bool isContinue;
        public ContinueInstruccion(bool isContinue)
        {
            this.isContinue = isContinue;
        }
        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
