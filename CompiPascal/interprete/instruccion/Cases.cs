using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Cases : Instruccion
    {
        private Expresion case_condicion;
        private LinkedList<Instruccion> cases_Instrucciones;

        public bool bandera;

        public Simbolo condicion;

        public Cases(Expresion case_condicion, LinkedList<Instruccion> cases_Instrucciones)
        {
            this.case_condicion = case_condicion;
            this.cases_Instrucciones = cases_Instrucciones;

            bandera = false;

            condicion = null;
        }


        public override object ejecutar(Entorno entorno)
        {
            object guardado = null;

            try
            {
                this.bandera = false;

                if (condicion == null)
                    throw new ErrorPascal("", 0, 0, "semantico");


                Simbolo caso_valor = validaciones(case_condicion, entorno);

                if (condicion.valor == null && caso_valor.valor == null)
                    throw new ErrorPascal("[case] error al obtener los valores de las expresiones para la condicion y/o el valor del caso",0,0,"semantico");
                if(condicion.tipo.tipo != caso_valor.tipo.tipo)
                    throw new ErrorPascal("[case] error los tipos de datos no coindicen entre el condicional y el del caso", 0, 0, "semantico");

                bool resultado = false;

                if (condicion.tipo.tipo == Tipos.INTEGER)
                    resultado = Convert.ToInt32(condicion.valor) == Convert.ToInt32(caso_valor.valor);
                else if(condicion.tipo.tipo == Tipos.STRING)
                    resultado = Convert.ToString(condicion.valor) == Convert.ToString(caso_valor.valor);
                else if (condicion.tipo.tipo == Tipos.BOOLEAN)
                    resultado = Convert.ToBoolean(condicion.valor) == Convert.ToBoolean(caso_valor.valor);



                if (resultado)
                {
                    foreach (var instruccion in cases_Instrucciones)
                    {
                        if (instruccion != null)
                        {
                            object res = instruccion.ejecutar(entorno);
                            if (res != null)
                            {
                                if (res is ExitInstruccion)
                                    return res;//ya no sigue ejecutando las intrucciones
                                else if (res is ReturnInstruccion)
                                    guardado = res;//sigue ejecutando, pero guarde el valor del return
                            }
                        }
                    }
                    this.bandera = true;
                }

            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return guardado;
        }


        private Simbolo validaciones(Expresion expCondicion, Entorno entorno)
        {
            if (expCondicion == null)
                throw new ErrorPascal("[case] Error al calcular la expresion de la condicion", 0, 0, "semantico");

            Simbolo condicion = expCondicion.evaluar(entorno);

            if (condicion == null)
                throw new ErrorPascal("[case] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.valor == null)
                throw new ErrorPascal("[case] El valor de la condicion no tiene un valor definido", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.INTEGER &&
                condicion.tipo.tipo != Tipos.STRING &&
                condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[case] El tipo de la condicion no es ni entero, ni string, ni boolean. Por lo tanto no es valida", 0, 0, "d");

            return condicion;
        }



    }
}
