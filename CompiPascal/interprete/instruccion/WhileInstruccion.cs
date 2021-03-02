using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class WhileInstruccion : Instruccion
    {
        private Expresion valor_condicional;
        private LinkedList<Instruccion> lista_instrucciones;


        public WhileInstruccion(Expresion valor_condicional, LinkedList<Instruccion> lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.lista_instrucciones = lista_instrucciones;

        }
        public override object ejecutar(Entorno entorno)
        {
            //valor
            //tipo de valor
            //ejecuto las instrucciones con el comportamiento  //ver ejemplo de typescript
                //recibo el resultado de = ejecutar las instrucciones
                //verifico es un null o un break o un return, continue, exit (ver cuales de estas)
                    //la accion correspondiente
                //lo mismo... evaluo la misma condicion para ver si sigue siendo valida
                //la evaluo si es boolean
                    //sino error
                //sigue la sig iteracion
            throw new NotImplementedException();
        }
    }
}
