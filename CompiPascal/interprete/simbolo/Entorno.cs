using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.instruccion;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.simbolo
{
    /// <summary>
    /// Para definir un entorno y que tenga relacion con su Entorno padre
    /// lenguaje normal:
    /// cada vez que ejecutemos una instruccion se crea un nuevo entorno
    /// compipascal:
    /// verificar cada vez de que es cuando creamos un nuevo entorno
    /// </summary>
    class Entorno
    {
        /* un solo diccionario y ahi, variables, funciones, structs, etc...
         * un tipo unico Simbolo y que sea variables, funciones, structs, etc...
         * un tipo diferente de Simbolos para variables, funciones, structs, etc...
         * varios diccionarios para cada tipo de Simbolo (si el lenguaje no permite el mismo nombre ir a validar a las dos listas [ejemplo: variable-funcion])
         */

        //se guarda la referencia del simbolo para poder manejarlos por valor o por referencia
        //primitivo, array, object
        Dictionary<string, Simbolo> variables;


        Dictionary<string, Funcion> funciones;

        Dictionary<string, ProcedureInstruccion> metodos;
        Entorno padre;


        public Entorno(Entorno padre)
        {
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.metodos = new Dictionary<string, ProcedureInstruccion>();
        }

        /// <summary>
        /// Permite guardar una nueva variable en el entorno. Variables en el mismo
        /// entorno con el mismo nombre no se permite pero si puede nombrarse igual a 
        /// una global.
        /// </summary>
        /// <param name="id">id de la variable</param>
        /// <param name="variable">referencia del simbolo que representa a la variable</param>
        public void guardarVariable(string id, Simbolo variable)
        {
            if (!this.variables.ContainsKey(id))
                this.variables.Add(id, variable);
            else
                throw new ErrorPascal("[Entorno] La variable ya existe en este entorno", 0, 0, "semantico");
        }

        /// <summary>
        /// Obtengo el simbolo que representa a la variable, puede estar en el
        /// entorno presente o en el padre.
        /// </summary>
        /// <param name="id">id de la variable</param>
        /// <returns>referencia del simbolo que representa a la variable o null</returns>
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

        //TODO: funciones, anidadas pueden llamar a otras globales y que esten en su ambito
        /// <summary>
        /// Guarda la funcion en el entorno global del programa.
        /// </summary>
        /// <param name="id">id de la funcion</param>
        /// <param name="funcion">simbolo que representa a la funcion</param>
        public void guardarFuncion(string id, Funcion funcion)
        {
            if (!getGlobal().funciones.ContainsKey(id))
                getGlobal().funciones.Add(id, funcion);
            else
                throw new ErrorPascal("[Entorno] La funcion ya existe en este entorno", 0, 0, "semantico");
        }

        /// <summary>
        /// Retorna la referencia a un objeto que representa a una funcion.
        /// Bucando en el entorno global unicamente, ya que seria el unico 
        /// lugar donde deberian de estar guardadas las funciones.
        /// </summary>
        /// <param name="id">id de la funcion a buscar</param>
        /// <returns>la FunctionInstruccion o null</returns>
        public Funcion getFuncion(string id)
        {
            if (getGlobal().funciones.ContainsKey(id))
                return getGlobal().funciones[id];
            else
                return null;              
        }

        /// <summary>
        /// Guarda el procedimiento en el entorno global del programa.
        /// </summary>
        /// <param name="id">id del procedimiento</param>
        /// <param name="procedimiento">simbolo que representa al procedimiento</param>
        public void guardarProcedure(string id, ProcedureInstruccion procedimiento)
        {
            if (!getGlobal().metodos.ContainsKey(id))
                getGlobal().metodos.Add(id, procedimiento);
            else
                throw new ErrorPascal("[Entorno] El procedimiento ya existe en este entorno", 0, 0, "semantico");
        }

        /// <summary>
        /// Retorna la referencia a un objeto que representa al procedimiento.
        /// Bucando en el entorno global unicamente, ya que seria el unico 
        /// lugar donde deberian de estar guardados los procedimientos.
        /// </summary>
        /// <param name="id">id del procedimiento a buscar</param>
        /// <returns>El ProcedureInstruccion o null</returns>
        public ProcedureInstruccion getProcedure(string id)
        {
            if (getGlobal().metodos.ContainsKey(id))
                return getGlobal().metodos[id];
            else
                return null;
        }


        /// <summary>
        /// Obtiene el entorno global, es decir el primero que se creo 
        /// al ejecutar el programa. 
        /// </summary>
        /// <returns>El entorno global</returns>
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
