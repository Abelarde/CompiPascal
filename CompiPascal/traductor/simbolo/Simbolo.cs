using CompiPascal.interprete.expresion;
using CompiPascal.interprete.instruccion;
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

        public LinkedList<Expresion> array_dimensiones_min_max;

        public Simbolo(Tipo tipo, string id, object valor, Entorno entorno, int otro)
        {
            this.tipo = tipo;
            this.id = id;
            this.valor = castValor(tipo, valor, entorno, id);
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
        public Simbolo(Tipo tipo, object valor, Entorno entorno, Arreglo clase)//Simbolos Indices
        {
            //Este indice va a tener...
            this.tipo = tipo;
            this.valor = castValor(tipo, valor, entorno, "");
            this.isConst = false;
            this.isType = false;
            this.isArray = false;
            this.isObject = false;
        }

        public object castValor(Tipo tipo, object valor, Entorno entorno, string id)
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
                        Simbolo variable_objeto = entorno.getVariable(tipo.tipoAuxiliar);//base
                        if(variable_objeto != null)
                        {
                            Structs copia_interna = variable_objeto.valor as Structs;//base-base
                            if(copia_interna != null)
                            {

                                Dictionary<string, Simbolo> nueva_lista = new Dictionary<string, Simbolo>();

                                foreach (KeyValuePair<string, Simbolo> kvp in  copia_interna.atributos)//kvp.Key, kvp.Value //base-base
                                {
                                    if(valor != null)//parametro
                                    {
                                        Structs val = valor as Structs;
                                        if (val != null)
                                        {
                                            //del val busco el atributo con el id, retorna el simbolo, agarro su valor
                                            //y creo uno nuevo, con ese valor
                                            Simbolo var = new Simbolo(new Tipo(kvp.Value.tipo.tipo, kvp.Value.tipo.tipoAuxiliar), kvp.Value.id, val.getAtributo(kvp.Value.id).valor, entorno, 0);
                                            nueva_lista.Add(var.id, var);
                                        }
                                        else
                                            throw new ErrorPascal("error al asignar un valor al parametro", 0, 0, "semantico");
                                    }
                                    else//asignacion nueva variable [var otra:curso]
                                    {
                                        Simbolo var = new Simbolo(new Tipo(kvp.Value.tipo.tipo, kvp.Value.tipo.tipoAuxiliar), kvp.Value.id, null, entorno, 0);
                                        nueva_lista.Add(var.id, var);
                                    }
                                }

                                Structs nuevo = new Structs(nueva_lista);//limpio, lleno [copia], lleno [referencia igualando al valor]
                                return nuevo;
                            }
                            throw new ErrorPascal("error al asignar un tipo objeto a la variable", 0, 0, "semantico");    

                        }else if(tipo.tipoAuxiliar == id)
                        {
                            return valor;
                        }
                        else
                        {
                            throw new ErrorPascal("ese tipo de dato no esta definido",0,0,"semantico");
                        }

                    case Tipos.ARRAY:
                        Simbolo variable_array = entorno.getVariable(tipo.tipoAuxiliar);
                        if (variable_array != null)
                        {
                            Arreglo copia_interna = variable_array.valor as Arreglo;
                            if (copia_interna != null)
                            {
                                //Simbolo nuevo = ArrayDeclarar.declarada(variable_array.id, variable_array.array_dimensiones_min_max, variable_array.tipo, entorno);
                                //nuevo.array_dimensiones_min_max = variable_array.array_dimensiones_min_max;

                                Arreglo copia = copia_interna.Clone();

                                return copia;
                            }
                            throw new ErrorPascal("error al asignar un tipo arreglo a la variable", 0, 0, "semantico");
                        }
                        else if (tipo.tipoAuxiliar == id)
                        {
                            return valor;
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

/*
 
 Simbolo nuevo = ArrayDeclarar.declarada(variable_array.id, variable_array.array_dimensiones_min_max, variable_array.tipo, entorno);
                            nuevo.array_dimensiones_min_max = variable_array.array_dimensiones_min_max;

                            return nuevo.valor;
 
 
 */