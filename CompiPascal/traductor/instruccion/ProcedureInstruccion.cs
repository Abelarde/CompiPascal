using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ProcedureInstruccion : Instruccion
    {
        private string id;

        private LinkedList<ParametroInst> lista_parametros;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;


        public ProcedureInstruccion(string id, LinkedList<ParametroInst> lista_parametros,
            LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id = id;
            this.lista_parametros = lista_parametros; 
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
