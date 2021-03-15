using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ElseIf : Instruccion
    {
        private Expresion expCondicion;
        private LinkedList<Instruccion> if_lista_instrucciones;

        public bool bandera;

        public ElseIf(Expresion expCondicion, LinkedList<Instruccion> if_lista_instrucciones)
        {
            this.expCondicion = expCondicion;
            this.if_lista_instrucciones = if_lista_instrucciones;

            bandera = false;
        }


        public override object ejecutar(Entorno entorno)
        {
            this.bandera = false;
            try
            {
                Simbolo condicion = validaciones(expCondicion, entorno);

                if (Convert.ToBoolean(condicion.valor))
                {                    
                    foreach (var instruccion in if_lista_instrucciones)
                    {
                        if (instruccion != null)
                        {
                            object resultado = instruccion.ejecutar(entorno);
                            if (resultado != null)
                            {
                                if (resultado is ExitInstruccion || resultado is ReturnInstruccion)
                                {
                                    this.bandera = true;
                                    return resultado;
                                }
                            }
                        }
                    }
                    this.bandera = true;
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en la sentencia else if",0,0,"semantico");
            }                        
        }

        private Simbolo validaciones(Expresion expCondicion, Entorno entorno)
        {
            if (expCondicion == null)
                throw new ErrorPascal("[IF] Error al calcular la expresion de la condicion", 0, 0, "semantico");

            Simbolo condicion = expCondicion.evaluar(entorno);

            if (condicion == null)
                throw new ErrorPascal("[if] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.valor == null)
                throw new ErrorPascal("[if] El valor de la condicion no tiene un valor definido", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[if] El tipo de la condicion no es boolean", 0, 0, "d");

            return condicion;
        }

    }
}
