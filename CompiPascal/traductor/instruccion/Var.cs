﻿using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class Var : Instruccion
    {
        private LinkedList<string> lista_ids;
        private string tipo;//primitivo, array, object [id]
        private Expresion expresion;

        public Var(LinkedList<string> lista_ids, string tipo, Expresion expresion)
        {
            this.lista_ids = lista_ids;
            this.tipo = tipo;
            this.expresion = expresion;
        }



        //TODO: variables del mismo ambito no mismo id. [si: locales-globales] [no: locales-locales]
        public override object ejecutar(Entorno entorno)
        {
            Tipo tipoFinal;

            if (tipo != string.Empty)
                tipoFinal = new Tipo(this.tipo, entorno);
            else
                throw new ErrorPascal("[Declaracion] El tipo de dato para la variable no viene especificada", 0, 0, "semantico");

            if (lista_ids.Count <= 0)
                throw new ErrorPascal("[Declaracion] La lista de id's en la declaracion esta vacia.", 0, 0, "semantico");

            try
            {
                if (expresion != null)
                    declarar_inicializar(tipoFinal, entorno, lista_ids);
                else
                    declarar(tipoFinal, entorno, lista_ids);
            }
            catch (ErrorPascal e)
            {
                e.ToString();
            }

            return null;
        }

        private void declarar(Tipo tipoFinal, Entorno entorno, LinkedList<string> p_lista_ids)
        {
            string msnError = string.Empty;
            bool bandera = false;

            foreach (string id in p_lista_ids)
            {
                if (entorno.getVariable(id) == null)
                {
                    Simbolo variable = new Simbolo(tipoFinal, id, null);
                    entorno.guardarVariable(id, variable);
                    bandera = true;
                }
                else
                    msnError += "[Declaracion] ya existe la variable " + id + Environment.NewLine;
            }

            if (msnError != "" && bandera)
                throw new ErrorPascal("[Declaracion] Se declararon algunas variables sin embargo " + Environment.NewLine + msnError, 0, 0, "semantico");
            else if (msnError != "" && !bandera)
                throw new ErrorPascal(msnError + Environment.NewLine + msnError, 0, 0, "semantico");
        }

        private void declarar_inicializar(Tipo tipoFinal, Entorno entorno, LinkedList<string> p_lista_ids)
        {
            Simbolo valor = this.expresion.evaluar(entorno);
            if (valor == null)
                throw new ErrorPascal("[Declaracion-Asignacion] Variable no ha sido declarada, Error al calcular u obtener el valor para la variable a declarar y asignar.", 0, 0, "semantico");

            if (valor.valor == null)
                throw new ErrorPascal("[Declaracion-Asignacion] La variable aun no tiene un valor asignado.", 0, 0, "semantico");

            if (valor.tipo.tipo != tipoFinal.tipo)
                throw new ErrorPascal("[Declaracion-Asignacion] El valor para la variable " + lista_ids.First.Value + " no coincide con el tipo declarado.", 0, 0, "semantico");


            if (entorno.getVariable(lista_ids.First.Value) == null)
            {
                valor.id = lista_ids.First.Value;
                valor.tipo = tipoFinal; 
                entorno.guardarVariable(valor.id, valor);
            }
            else
                throw new ErrorPascal("[Declaracion-Asignacion] Ya existe la variable " + lista_ids.First.Value, 0, 0, "semantico");


            if (lista_ids.Count > 1)
                throw new ErrorPascal("[Declaracion-Asignacion] Tratas de declarar y asignar una lista de variables y no es permitido, sin embargo se logro hacer la operacion solo para el primer id", 0, 0, "semantico");


        }

    }
}