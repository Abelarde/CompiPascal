using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
    class Simbolo
    {
        public Tipo tipo; //tipo de dato del simbolo //primitivo,array,object
        public string id; //identificador asociado al simbolo
        public object valor; //guarda el valor del simbolo    //Arreglo() || Structs()
        //linea
        //columna 
        public bool isConst;
        public bool isType;
        public bool isArray;
        public bool isObject;

        public Simbolo(Tipo tipo, string id, object valor)
        {
            this.tipo = tipo;
            this.id = id;
            this.valor = castValor(tipo, valor);
            this.isConst = false;
            this.isType = false;
            this.isArray = false;
            this.isObject = false;
        }

        //TODO: que valores me interesan en la inicializacion para los arreglos
        public Simbolo()
        {

        }

        //arreglos
        public Simbolo(Tipo tipo, int min, int max)//Simbolos variables arrays
        {
            this.tipo = tipo;//Tipos.ARRAY
            this.valor = new Arreglo(min, max); //[0]
            this.isConst = false;
            this.isType = true;
            this.isArray = true;
            this.isObject = false;
        }
        public Simbolo(Tipo tipo, object valor)//Simbolos Indices
        {
            //Este indice va a tener...
            this.tipo = tipo;
            this.valor = castValor(tipo, valor);
            this.isConst = false;
            this.isType = false;
            this.isArray = false;
            this.isObject = false;
        }

        public object castValor(Tipo tipo, object valor)
        {
            if (tipo != null)
            {
                switch (tipo.tipo)
                {
                    case Tipos.STRING:
                        return Convert.ToString(valor);
;                    case Tipos.INTEGER:
                        return Convert.ToInt32(valor);
                    case Tipos.REAL:
                        return Convert.ToDouble(valor);
                    case Tipos.BOOLEAN:
                        return Convert.ToBoolean(valor);
                    case Tipos.OBJECT:
                        Simbolo variable_objeto = null;// entorno.getVariable(tipo.tipoAuxiliar);
                        if(variable_objeto != null)
                        {
                            Structs copia_interna = variable_objeto.valor as Structs;
                            if(copia_interna != null)
                            {
                                Structs copia = copia_interna.Clone();
                                return copia;
                            }
                            throw new ErrorPascal("error al asignar un tipo objeto a la variable", 0, 0, "semantico");    
                        }
                        else
                        {
                            throw new ErrorPascal("ese tipo de dato no esta definido",0,0,"semantico");
                        }

                    case Tipos.ARRAY:
                        Simbolo variable_array = null;// entorno.getVariable(tipo.tipoAuxiliar);
                        if (variable_array != null)
                        {
                            Arreglo copia_interna = variable_array.valor as Arreglo;
                            if (copia_interna != null)
                            {
                                Arreglo copia = copia_interna.Clone();
                                return copia;
                            }
                            throw new ErrorPascal("error al asignar un tipo arreglo a la variable", 0, 0, "semantico");
                        }
                        else
                        {
                            throw new ErrorPascal("ese tipo de dato no esta definido", 0, 0, "semantico");
                        }
                    case Tipos.ERROR:
                        return null;
                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
