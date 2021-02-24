using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class FunctionInstruccion : Instruccion
    {
        private string id;
        private LinkedList<string> parametros_valor;
        private LinkedList<string> parametros_referencia;
        private string tipo_funcion;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;


        public FunctionInstruccion(string id, LinkedList<string> parametros_referencia, LinkedList<string> parametros_valor,
            string tipo_funcion, LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id = id;
            this.parametros_referencia = parametros_referencia;
            this.parametros_valor = parametros_valor;
            this.tipo_funcion = tipo_funcion;
            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;
        }

        //TODO: validar que las listas tengan el Count > 0... porlomenos uno

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
