using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Funcion : Instruccion
    {
        private string id_funcion;
        private Tipo tipo_funcion;

        private Parametro[] parametros;//valor = new Simbolo; referencia = entorno.simbolo

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;
        
        public Funcion(string id_funcion, Tipo tipo_funcion, Parametro[] parametros, LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id_funcion = id_funcion;
            this.tipo_funcion = tipo_funcion;
            this.parametros = parametros;
            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;
        }

        //entorno [nuevo con global]
        public override object ejecutar(Entorno entorno)
        {          
            if(parametros != null)
            {
                foreach (Parametro parametro in parametros)
                {
                    if (entorno.getVariable(parametro.simbolo.id) == null)//repetidos?
                        entorno.guardarVariable(parametro.simbolo.id, parametro.simbolo);
                    else
                        throw new ErrorPascal("Ya existe un parametro con ese nombre",0,0,"semantico");
                }
            }


            foreach (Instruccion header in header_instrucciones)
            {
                if (header != null)
                    header.ejecutar(entorno);
            }

            //object guardado = null;
            foreach (Instruccion body in body_instrucciones)
            {
                if (body != null)
                {
                    object resultado = body.ejecutar(entorno); 
                    //if (resultado != null)
                    //{
                    //    if (resultado is ExitInstruccion)
                    //    {
                    //        return resultado;
                    //    }
                    //    else if (resultado is ReturnInstruccion)
                    //    {
                    //        guardado = resultado;
                    //    }
                    //}
                } 
            }
            //return guardado;
            return null;
        }

        //posicion puede >= 0
        public void setParametros(int posicion, Simbolo valor)
        {
            if(posicion <= parametros.Length)
                parametros[posicion].simbolo.valor = valor.valor;
            else
                throw new ErrorPascal("no coindice el envio del parametro con los parametros de la funcion", 0, 0, "semantico");
        }
    }
}
