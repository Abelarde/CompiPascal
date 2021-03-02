using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class Logica : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;

        public Logica(Expresion izquierda, Expresion derecha, string tipoOperacion)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo derecha = this.derecha.evaluar(entorno);
            Simbolo izquierda = null;

            Tipos tipoResultante = derecha.tipo.tipo;

            if (this.izquierda != null)
            {
                izquierda = this.izquierda.evaluar(entorno);
                tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            }


            if (tipoResultante == Tipos.ERROR)
            {
                if (izquierda != null)
                {
                    throw new ErrorPascal("Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                }
                else
                {
                    throw new ErrorPascal("Tipo de dato incorrecto [" + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                }
            }

            Simbolo resultado;

            switch (tipoOperacion)
            {
                case "AND":
                    //TODO: tomar en cuenta si no tengo que validar que venga un null ya sea en la izq o derecha "Simbolo"
                    //y cuidado con el valor que pueda tener tambien porque puede ser null
                    resultado = new Simbolo(Convert.ToBoolean(izquierda.valor) && Convert.ToBoolean(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;

                case "OR":
                    resultado = new Simbolo(Convert.ToBoolean(izquierda.valor) || Convert.ToBoolean(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;

                case "NOT":
                    resultado = new Simbolo(!Convert.ToBoolean(derecha.valor), new Tipo(Tipos.BOOLEAN, null), null);
                    return resultado;

                default:
                    throw new ErrorPascal("Operacion logica desconocida", 0, 0, "Semantico");
            }

        }
    }
}
