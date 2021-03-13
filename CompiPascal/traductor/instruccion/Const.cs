using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Const : Instruccion
    {
        private string id;
        private Expresion valor;

        public Const(string id, Expresion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        //TODO: variables del mismo ambito no mismo id. [si: locales-globales] [no: locales-locales]
        public override object ejecutar(Entorno entorno)
        {
            try
            {
                if (id == "")
                    throw new ErrorPascal("[const] El id para la constante no viene especificada", 0, 0, "semantico");

                Simbolo constante = validaciones(valor, entorno);//valor,tipo

                if (entorno.getVariable(id) == null)//aqui sino se permite dos variables con el mismo nombre
                {
                    constante.id = id;
                    constante.isConst = true;
                    entorno.guardarVariable(constante.id, constante);
                }
                else
                    throw new ErrorPascal("[const] Ya existe la constante " + id, 0, 0, "semantico");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }

        //integer, real, logical, string
        private Simbolo validaciones(Expresion expValor, Entorno entorno)
        {
            if (expValor == null)
                throw new ErrorPascal("[const] Error al calcular la expresion del valor para la constante", 0, 0, "semantico");

            Simbolo constante = expValor.evaluar(entorno);//valor, tipo

            if (constante == null)
                throw new ErrorPascal("[const] Error al obtener el simbolo de la constante", 0, 0, "semantico");
            if (constante.valor == null)
                throw new ErrorPascal("[const] El valor de la constante no tiene un valor definido", 0, 0, "semantico");
            if (constante.tipo.tipo != Tipos.INTEGER &&
                constante.tipo.tipo != Tipos.REAL &&
                constante.tipo.tipo != Tipos.STRING &&
                constante.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[const] El tipo de la constante no es ni entero, ni real, ni string, ni boolean. Por lo tanto no es valida", 0, 0, "semantico");

            return constante;
        }
    }
}
/*
 * Tipos primitivos especificos
 * Operaciones con otras constantes nada mas
 * No arrays
 * 
 */