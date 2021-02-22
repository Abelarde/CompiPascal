using CompiPascal.interprete.analizador.simbolo;
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
        Dictionary<string, Simbolo> variables;
        //TODO: preguntar: porque funcion y metodo no son de tipo Simbolo
        Dictionary<string, Simbolo> funciones;
        Dictionary<string, Simbolo> metodos;
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
            while(actual != null)
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



    }
}
