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
            //retornar algo que me diga si es un Break o Continuo
            //podria saberlo casteando la clase? para saber que tipo
            //es: break, continue, exit
            throw new NotImplementedException();
        }
    }
}
