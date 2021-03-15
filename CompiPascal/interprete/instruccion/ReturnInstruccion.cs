using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
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
            try
            {
                if (entorno.getFuncion(id) == null)
                    throw new ErrorPascal("el id de la funcion no es correcto", 0, 0, "semantico");

                Simbolo resultado = expreValor.evaluar(entorno);
                this.simbolResultado = resultado;
                return this;
            }
            catch (ErrorPascal ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en el return", 0, 0, "semantico");

            }
        }
    }
}
