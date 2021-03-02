using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.instruccion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.simbolo
{
    /// <summary>
    /// Para definir un entorno y que tenga relacion con su Entorno padre
    /// </summary>
    class Entorno
    {
        /*
         * un solo diccionario y ahi, variables, funciones, structs, etc...
         * un tipo unico Simbolo y que sea variables, funciones, structs, etc...
         * un tipo diferente de Simbolos para variables, funciones, structs, etc...
         * varios diccionarios para cada tipo de Simbolo (si el lenguaje no permite el mismo nombre ir a validar a las dos listas [ejemplo: variable-funcion])
         */

        Dictionary<string, Simbolo> variables;
        Dictionary<string, FunctionInstruccion> funciones;
        Dictionary<string, ProcedureInstruccion> metodos;
        Entorno padre;

        //TODO: faltarian los demas simbolos del ambito
        public Entorno(Entorno padre)
        {
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
        }

        //TODO: corregir aqui que se permite tener variables locales nombradas iguales que una global
        public void DeclararVariable(string id, Simbolo variable)
        {
            if(this.variables[id] != null)
            {
                this.variables.Add(id, variable);
            }
            else
            {
                throw new Exception("La variable " + id + " ya existe en este ambito");
            }
        }

        public Simbolo ObtenerVariable(string id)
        {
            Entorno actual = this;
            while(actual != null)//sino no lo encuentro aqui esta en el padre //verificar si solo padre o abuelo o verificar el recorrido de este metodo
            {
                if(actual.variables[id] != null)
                {
                    return actual.variables[id];
                }
                actual = actual.padre;
            }
            //TODO: verificar si esta bien el null o mejor un error propio
            return null;
        }

        //TODO: existe donde? en el ambito local y/o global tambien
        public bool ExisteVariable(string id)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.variables[id] != null)
                {
                    return true;
                }
                actual = actual.padre;
            }
            //TODO: verificar si esta bien el null o mejor un error propio
            return false;
        }

        //lenguaje normal:
        //cada vez que ejecutemos una instruccion se crea un nuevo entorno
        //compipascal:
        //verificar cada vez de que es cuando creamos un nuevo entorno

        //guardarVar  //(Simbolo)
        //guardarFunc  //(Funcion)
        //getVar  //Simbolo
        //getFunc //Funcion
        //getGlobal //Entorno


        //se guarda la referencia del simbolo para poder manejarlos por valor o por referencia
    }
}
