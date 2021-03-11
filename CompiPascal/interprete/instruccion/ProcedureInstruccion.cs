using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ProcedureInstruccion : Instruccion
    {
        private string id;

        private ParametroInst[] parametros;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;


        public ProcedureInstruccion(string id, ParametroInst[] parametros,
            LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id = id;
            this.parametros = parametros; 
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
