using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ReturnInstruccion : Instruccion
    {
        string id;
        private Expresion expreValor;
        public Simbolo simbolResultado;


        public ReturnInstruccion(string id, Expresion expreValor)
        {
            this.id = id;
            this.expreValor = expreValor;
            simbolResultado = null;
        }
        
        public override object ejecutar(Entorno entorno)
        {
            if (entorno.getFuncion(id) == null)
                throw new ErrorPascal("el id de la funcion no es correcto",0,0,"semantico");

            Simbolo resultado = expreValor.evaluar(entorno);
            this.simbolResultado = resultado;
            return this;
        }
    }
}
