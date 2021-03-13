using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.simbolo
{
    class Arreglo
    {
        public Simbolo[] valores;
        public int min;
        public int max;
        public Tipo tipo_del_array;

        //una cosa es la dimension y otra el tamanio max
        public Arreglo(int min, int max)
        {
            this.valores = InitializeArray<Simbolo>(max); 
            this.min = min;
            this.max = max;
            //Tipo tipo_del_array
        }

        public Arreglo()
        {

        }

        //indice-min
        public Simbolo getIndice(int indice)
        {
            indice = indice - (Convert.ToInt32(this.min));
            try
            {   //index-min
                return valores[indice];
            }
            catch (Exception ex)
            {
                throw new ErrorPascal("El indice esta fuera del rango del arreglo o "+ex.ToString(),0,0,"semantico");
            }
        }

        public void setIndice(int indice, Simbolo valor)
        {
            indice = indice - (Convert.ToInt32(this.min));

            try
            {
                this.valores[indice] = valor;
            }
            catch (Exception ex)
            {
                throw new ErrorPascal("El indice esta fuera del rango del arreglo o " + ex.ToString(), 0, 0, "semantico");
            }
        }

        private T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        public void inicializarIndices(Tipo tipoArreglo, Entorno entorno)
        {
            for(int i = 0; i < valores.Length; i++)
            {
                valores[i] = new Simbolo(tipoArreglo, null);
            }
        }

        public Arreglo Clone()
        {
            return (Arreglo)this.MemberwiseClone();
        }
    }
}

//una cosa es == null [por no inicializar el valor]
//otra error por estar fuera del rango