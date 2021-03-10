using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class TypeInstruccion : Instruccion
    {
        private LinkedList<string> lista_ids;//todos

        private string primitivo_id;//primitivo, id

        private LinkedList<Expresion> array_dimensiones_min_max;//array
        private string array_primitivo_id;

        private LinkedList<Instruccion> object_lista_listaVars;//object

        //0 = primitivo, id
        //1 = array
        //2 = object
        private int tipo;

        //primitivo, id
        public TypeInstruccion(LinkedList<string> lista_ids, string primitivo_id)
        {
            this.lista_ids = lista_ids;
            this.primitivo_id = primitivo_id;
            this.tipo = 0;
        }

        //array
        public TypeInstruccion(LinkedList<string> lista_ids, LinkedList<Expresion> array_dimensiones_min_max, string array_primitivo_id)
        {
            this.lista_ids = lista_ids;
            this.array_dimensiones_min_max = array_dimensiones_min_max;
            this.array_primitivo_id = array_primitivo_id;
            this.tipo = 1;
        }

        //object
        public TypeInstruccion(LinkedList<string> lista_ids, LinkedList<Instruccion> object_lista_listaVars)
        {
            this.lista_ids = lista_ids;
            this.object_lista_listaVars = object_lista_listaVars;
            this.tipo = 2;
        }

        public override object ejecutar(Entorno entorno)
        {
            if (lista_ids.Count <= 0)
                throw new ErrorPascal("[type] La lista de id's en la declaracion esta vacia.", 0, 0, "semantico");

            try
            {
                if (tipo == 0)
                    typePrimitivoId(primitivo_id, entorno, lista_ids);
                else if (tipo == 1)
                    typeArray(lista_ids, array_dimensiones_min_max, array_primitivo_id, entorno);
                else if (tipo == 2)
                    typeObject();
                else
                    return null;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }

        private void typePrimitivoId (string tipo, Entorno entorno, LinkedList<string> lista_ids)
        {
            Tipo tipoFinal;
            if (tipo != string.Empty)
                tipoFinal = new Tipo(tipo, entorno);
            else
                throw new ErrorPascal("[type] El tipo de dato para la variable no viene especificada", 0, 0, "semantico");
            
            string msnError = string.Empty;
            bool bandera = false;

            foreach (string id in lista_ids)
            {
                if (entorno.getVariable(id) == null)
                {
                    Simbolo variable = new Simbolo(tipoFinal, id, null);//ir a validar a var//si es object ver su auxiliar
                    variable.isType = true;
                    entorno.guardarVariable(id, variable);
                    bandera = true;
                }
                else
                    msnError += "[type] ya existe la variable " + id + Environment.NewLine;
            }

            if (msnError != "" && bandera)
                throw new ErrorPascal("[type] Se declararon algunas variables sin embargo " + Environment.NewLine + msnError, 0, 0, "semantico");
            else if (msnError != "" && !bandera)
                throw new ErrorPascal(msnError + Environment.NewLine + msnError, 0, 0, "semantico");


        }

        private void typeArray(LinkedList<string> lista_ids, //for
            LinkedList<Expresion> array_dimensiones_min_max,//1 array
            string array_primitivo_id,//1 array
            Entorno entorno)//1 array
        {
            Tipo tipoArreglo;
            if (array_primitivo_id != string.Empty)
                tipoArreglo = new Tipo(array_primitivo_id, entorno);
            else
                throw new ErrorPascal("[type] El tipo de dato para la variable no viene especificada", 0, 0, "semantico");

            string msnError = string.Empty;
            bool bandera = false;

            foreach (string id in lista_ids)
            {
                if (entorno.getVariable(id) == null)
                {
                    Simbolo variable = ArrayDeclarar.declarada(id, array_dimensiones_min_max, tipoArreglo, entorno);

                    if (variable == null)
                        throw new ErrorPascal("error al crear el arreglo", 0, 0, "semantico");

                    entorno.guardarVariable(id, variable);
                    bandera = true;
                }
                else
                    msnError += "[type] ya existe la variable " + id + Environment.NewLine;
            }

            if (msnError != "" && bandera)
                throw new ErrorPascal("[type] Se declararon algunas variables sin embargo " + Environment.NewLine + msnError, 0, 0, "semantico");
            else if (msnError != "" && !bandera)
                throw new ErrorPascal(msnError + Environment.NewLine + msnError, 0, 0, "semantico");

        }

        private void typeObject()
        {

        }




    }
}



//primitivo, array y object
//las matrices se inicializan mediante asignación
//no se pueden leer y/o escribir sobre estos tipos [primitivo, array y object] ... solo las variables de estos tipos [primitivo y array][object no]
//