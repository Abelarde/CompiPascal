using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
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
            //calculo
            Simbolo condicion = this.valor_condicional.evaluar(entorno);

            //verificar errores
            if (condicion == null || condicion.valor == null)
                throw new ErrorPascal("[if] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[if] El tipo de la condicion no es boolean", 0, 0, "d");


            //comportamiento
            while (Convert.ToBoolean(condicion.valor))
            {
                //resultado de cada uno de sus instrucciones
                foreach(Instruccion instruccion in lista_instrucciones)
                {
                    object respuesta = instruccion.ejecutar(entorno);
                    //verifico es un null o un break o un return, continue, exit (ver cuales de estas)
                    if (respuesta != null)
                    {
                        //la accion correspondiente
                        if (respuesta is BreakInstruccion)
                            break;
                        else if (respuesta is ContinueInstruccion)
                        {
                            //lo mismo... evaluo la misma condicion para ver si sigue siendo valida
                            condicion = this.valor_condicional.evaluar(entorno);
                            continue;
                        }
                        else
                            print += respuesta;
                    }
                }


                //lo mismo... evaluo la misma condicion para ver si sigue siendo valida
                condicion = this.valor_condicional.evaluar(entorno);
                //verificar errores
                if (condicion == null || condicion.valor == null)
                    throw new ErrorPascal("[if] Error al obtener el valor de la condicion", 0, 0, "d");
                if (condicion.tipo.tipo != Tipos.BOOLEAN)
                    throw new ErrorPascal("[if] El tipo de la condicion no es boolean", 0, 0, "d");
            }
            
            return print;
        }
    }
}
