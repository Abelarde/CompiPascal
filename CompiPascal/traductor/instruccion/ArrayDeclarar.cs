using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ArrayDeclarar
    {
        private string id;
        LinkedList<Expresion> array_dimensiones_min_max;
        Tipo tipoArreglo;

        static Expresion[] lista_comodin;

        //ya lista solo para guardar
        public static Simbolo declarada(string id, LinkedList<Expresion> array_dimensiones_min_max, Tipo tipoArreglo, Entorno entorno)
        {
            try
            {                
                validacionesID(id);
                validacionesTipo(tipoArreglo);
                int dimensiones = array_dimensiones_min_max.Count;


                if(dimensiones > 0)
                {
                    Expresion era_dimension = array_dimensiones_min_max.First.Value;

                    //.tipo, .valor(object)[Arreglo]//.isConst//.isType//.isArray
                    Simbolo variable_array = era_dimension.evaluar(entorno); 
                    //id //.valor(object)[Arreglo] -> tipoDatos
                    variable_array.id = id;

                    if (variable_array.valor == null)
                        throw new ErrorPascal("error al calcular la dimension del arreglo", 0, 0, "semantico");
                    Arreglo arr = variable_array.valor as Arreglo;
                    if (arr == null)
                        throw new ErrorPascal("error al calcular la dimension del arreglo", 0, 0, "semantico");

                    arr.tipo_del_array = tipoArreglo;
                    arr.inicializarIndices(tipoArreglo);

                    lista_comodin = new Expresion[array_dimensiones_min_max.Count];
                    int indi = 0;
                    foreach (Expresion nodo in array_dimensiones_min_max)
                    {
                        lista_comodin[indi] = nodo;
                        indi++;
                    }

                    Simbolo anterior = null;
                    Simbolo nuevo = null;
                    Simbolo courier = null;

                    for (int i = lista_comodin.Length-1; i >= 1 ; i--)//ultima dimension
                    {
                        courier = Dimension(lista_comodin[i], entorno, tipoArreglo); //.tipo, .valor(object)[Arreglo]//.isConst//.isType//.isArray
                                               
                        if (i == lista_comodin.Length - 1)
                        {
                            anterior = courier;
                            continue;
                        }

                        Rellenar(courier, anterior);
                        nuevo = courier;
                        anterior = nuevo;
                    }

                    Rellenar(variable_array, anterior);

                    return variable_array;

                }
                else
                {
                    throw new ErrorPascal("error las dimensiones para el arreglo son 0 o menos, para ser arreglo/variable debe ser almenos 1", 0, 0, "semantico");
                }



            }catch(Exception ex)
            {
                ex.ToString();
            }

            return null;
        }

        private static void validacionesID(string id)
        {
            if (id == "")
                throw new ErrorPascal("el id no esta especificado para declarar el arreglo",0,0,"semantico");
        }        
        private static void validacionesTipo(Tipo tipoArreglo)
        {
            if (tipoArreglo == null)
                throw new ErrorPascal("el para el arreglo no se pudo obtener", 0, 0, "semantico");
        }

        private static void Rellenar(Simbolo courier, Simbolo anterior) //dimensiones
        {
            Arreglo arr = courier.valor as Arreglo;
            if (arr == null)
                throw new ErrorPascal("error en la dimension",0,0,"semantico");
                       

            for (int i = 0; i < arr.valores.Length; i++) //Simbolo[] valores
            {
                arr.valores[i] = anterior;
            }
        }

        private static Simbolo Dimension(Expresion colonColon, Entorno entorno, Tipo tipoArreglo)
        {
            Simbolo dimension = colonColon.evaluar(entorno);
            if (dimension.valor == null)
                throw new ErrorPascal("error al calcular alguna dimension del arreglo", 0, 0, "semantico");
            Arreglo arrDim = dimension.valor as Arreglo;
            if (arrDim == null)
                throw new ErrorPascal("error al calcular la dimension del arreglo", 0, 0, "semantico");
            arrDim.tipo_del_array = tipoArreglo;
            arrDim.inicializarIndices(tipoArreglo);

            return dimension;
        }

    }
}
