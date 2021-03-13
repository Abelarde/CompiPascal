using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.instruccion;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.simbolo
{
    class Entorno
    {
        /* un solo diccionario y ahi, variables, funciones, structs, etc...
         * un tipo unico Simbolo y que sea variables, funciones, structs, etc...
         * un tipo diferente de Simbolos para variables, funciones, structs, etc...
         * varios diccionarios para cada tipo de Simbolo (si el lenguaje no permite el mismo nombre ir a validar a las dos listas [ejemplo: variable-funcion])
         */

        //se guarda la referencia del simbolo para poder manejarlos por valor o por referencia        
        Dictionary<string, Simbolo> variables;//primitivo, array, object

        Dictionary<string, Funcion> funciones;

        //Dictionary<string, ProcedureInstruccion> metodos;

        Entorno padre;


        public Entorno(Entorno padre)
        {
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            //this.metodos = new Dictionary<string, ProcedureInstruccion>();
        }

        public void guardarVariable(string id, Simbolo variable)
        {
            if (!this.variables.ContainsKey(id))
                this.variables.Add(id, variable);
            else
                throw new ErrorPascal("[Entorno] La variable ya existe en este entorno", 0, 0, "semantico");
        }

        public Simbolo getVariable(string id)
        {
            Entorno actual = this;
            while(actual != null)//sino no lo encuentro aqui esta en el padre //verificar si solo padre o abuelo o verificar el recorrido de este metodo
            {
                if(actual.variables.ContainsKey(id))
                    return actual.variables[id];

                actual = actual.padre;
            }            
            return null;//TODO: verificar si esta bien el null o mejor un error propio
        }

        public void guardarFuncion(string id, Funcion funcion)
        {
            if (!getGlobal().funciones.ContainsKey(id))
                getGlobal().funciones.Add(id, funcion);
            else
                throw new ErrorPascal("[Entorno] La funcion ya existe en este entorno", 0, 0, "semantico");
        }

        public Funcion getFuncion(string id)
        {
            if (getGlobal().funciones.ContainsKey(id))
                return getGlobal().funciones[id];
            else
                return null;              
        }
        /*
        public void guardarProcedure(string id, ProcedureInstruccion procedimiento)
        {
            if (!getGlobal().metodos.ContainsKey(id))
                getGlobal().metodos.Add(id, procedimiento);
            else
                throw new ErrorPascal("[Entorno] El procedimiento ya existe en este entorno", 0, 0, "semantico");
        }

        public ProcedureInstruccion getProcedure(string id)
        {
            if (getGlobal().metodos.ContainsKey(id))
                return getGlobal().metodos[id];
            else
                return null;
        }
        */
        public Entorno getGlobal()
        {
            Entorno actual = this;
            while (actual.padre != null)
            {
                actual = actual.padre;
            }
            return actual;
        }

    }
}
