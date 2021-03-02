using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class Relacional : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;
        public Relacional(Expresion izquierda, Expresion derecha, string tipoOperacion)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            if(this.izquierda == null || this.derecha == null)
            {
                throw new ErrorPascal("Error al obtener los valores de las expresiones", 0, 0, "Semantico");
            }

            Simbolo izquierda = this.izquierda.evaluar(entorno);
            Simbolo derecha = this.derecha.evaluar(entorno);
            
            Tipos tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            if (tipoResultante == Tipos.ERROR)
            {
                throw new ErrorPascal("Tipos de datos incorrectos [relacional: "+izquierda.tipo.tipo+", "+derecha.tipo.tipo +"]", 0, 0, "Semantico");
            }

            Simbolo resultado;
            switch (tipoOperacion)
            {
                case "=":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) == Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                case "<>":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) != Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                case ">":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) > Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                case "<":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) < Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                case ">=":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) >=  Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                case "<=":
                    resultado = new Simbolo(Convert.ToDouble(izquierda.valor) <= Convert.ToDouble(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;
                default:
                    throw new ErrorPascal("Operacion relacional desconocida", 0, 0, "Semantico");
            }


        }
    }
}
