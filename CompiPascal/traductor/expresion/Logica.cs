using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.expresion
{
    class Logica : Expresion
    {
        private Expresion expIzq;
        private Expresion expDer;
        private string tipoOperacion;

        public Logica(Expresion expIzq, Expresion expDer, string tipoOperacion)
        {
            this.expIzq = expIzq;
            this.expDer = expDer;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo derecha = validaciones(expDer, entorno);

            Tipos tipoResultante = derecha.tipo.tipo;

            Simbolo izquierda = null;

            if (expIzq != null)
            {
                izquierda = validaciones(expIzq, entorno);              

                tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            }

            if (tipoResultante == Tipos.ERROR)
            {
                if (izquierda != null)
                    throw new ErrorPascal("Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                else
                    throw new ErrorPascal("Tipo de dato incorrecto [" + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
            }


            switch (tipoOperacion)
            {
                case "AND":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToBoolean(izquierda.valor) && Convert.ToBoolean(derecha.valor), entorno, 0);        

                case "OR":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToBoolean(izquierda.valor) || Convert.ToBoolean(derecha.valor), entorno, 0);            

                case "NOT":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, !Convert.ToBoolean(derecha.valor), entorno, 0);

                default:
                    throw new ErrorPascal("Operacion logica desconocida", 0, 0, "Semantico");
            }
        }

        private Simbolo validaciones(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[logica] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo simbolo = exp.evaluar(entorno);

            if (simbolo == null)
                throw new ErrorPascal("[logica] Error al obtener el simbolo", 0, 0, "d");
            //if (simbolo.valor == null)
            //    throw new ErrorPascal("[aritmetica] El valor del simbolo no tiene un valor definido", 0, 0, "d");
            if (simbolo.tipo == null)
                throw new ErrorPascal("[logica] El tipo del simbolo no esta definido", 0, 0, "d");

            return simbolo;
        }


    }
}
