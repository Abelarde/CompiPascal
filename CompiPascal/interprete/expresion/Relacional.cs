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
        private Expresion expIzq;
        private Expresion expDer;
        private string tipoOperacion;
        public Relacional(Expresion expIzq, Expresion expDer, string tipoOperacion)
        {
            this.expIzq = expIzq;
            this.expDer = expDer;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo izquierda = validaciones(expIzq, entorno); 
            Simbolo derecha = validaciones(expDer, entorno);          

            Tipos tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            if (tipoResultante == Tipos.ERROR)
                throw new ErrorPascal("Tipos de datos incorrectos [relacional: " + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");

            switch (tipoOperacion)
            {
                case "=":
                    if (tipoResultante == Tipos.REAL || tipoResultante == Tipos.INTEGER)
                        return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) == Convert.ToDouble(derecha.valor));
                    else if (tipoResultante == Tipos.STRING)
                        return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToString(izquierda.valor) == Convert.ToString(derecha.valor));
                    //else if //array
                    //else if //object
                    else
                        throw new ErrorPascal("Operacion relacional no valida", 0, 0, "Semantico");

                case "<>":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) != Convert.ToDouble(derecha.valor));

                case ">":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) > Convert.ToDouble(derecha.valor));
           
                case "<":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) < Convert.ToDouble(derecha.valor));
                   
                case ">=":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) >=  Convert.ToDouble(derecha.valor));
                    
                case "<=":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToDouble(izquierda.valor) <= Convert.ToDouble(derecha.valor));
                  
                default:
                    throw new ErrorPascal("Operacion relacional desconocida", 0, 0, "Semantico");
            }


        }
        private Simbolo validaciones(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[relacional] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo simbolo = exp.evaluar(entorno);

            if (simbolo == null)
                throw new ErrorPascal("[relacional] Error al obtener el simbolo", 0, 0, "d");
            //if (simbolo.valor == null)
            //    throw new ErrorPascal("[aritmetica] El valor del simbolo no tiene un valor definido", 0, 0, "d");
            if (simbolo.tipo == null)
                throw new ErrorPascal("[relacional] El tipo del simbolo no esta definido", 0, 0, "d");

            return simbolo;
        }

    }
}
