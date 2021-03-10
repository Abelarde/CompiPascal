using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
    /// <summary>
    /// Simbolo es un objeto que se guardara en la tabla de simbolos
    /// Representa: una variable, una constante, una funcion, un metodo
    /// Puede tener: atributos que ayuden al interprete
    /// </summary>
    class Simbolo
    {
        public Tipo tipo; //tipo de dato del simbolo //primitivo,array,object
        public string id; //identificador asociado al simbolo
        public object valor; //guarda el valor del simbolo    
        //linea
        //columna 
        public bool isConst;
        public bool isType;
        public bool isArray;

        public Simbolo(Tipo tipo, string id, object valor)
        {
            this.tipo = tipo;
            this.id = id;
            //this.valor = valor;
            this.valor = castValor(tipo, valor);
            this.isConst = false;
            this.isType = false;
            this.isArray = false;
        }

        //TODO: que valores me interesan en la inicializacion para los arreglos
        public Simbolo()
        {

        }

        //arreglos
        public Simbolo(Tipo tipo, int min, int max)//Simbolos variables arrays
        {
            this.valor = new Arreglo(min, max); //[0]
            this.tipo = tipo;//Tipos.ARRAY
            this.isConst = false;
            this.isType = true;
            this.isArray = true;
        }
        public Simbolo(Tipo tipo, object valor)//Simbolos Indices
        {
            this.tipo = tipo;
            //this.valor = valor;
            this.valor = castValor(tipo, valor);
            this.isConst = false;
            this.isType = false;
            this.isArray = false;
        }

        //para inicializar con un valor las variables que se declaran
        //en las operaciones no tengo problemas porque ya iria con un valor convertido aqui
        //o en ese clase lo convierte ahi, si fuera null.
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
                        return null;//TODO: quiza un objeto de la clase?
                    case Tipos.ARRAY:
                        return null;//TODO: lo mismo de object
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
