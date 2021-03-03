using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    /// <summary>
    /// Analiza, guarda el simbolo que representa una funcion "FunctionInstruccion".
    /// </summary>
    class FunctionInstruccion : Instruccion
    {
        private string id;
        private LinkedList<string> parametros_valor;
        private LinkedList<string> parametros_referencia;
        private string tipo_funcion;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;

        private Tipo tipo_Final;
       

        public FunctionInstruccion(string id, LinkedList<string> parametros_referencia, LinkedList<string> parametros_valor,
            string tipo_funcion, LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id = id;
            this.parametros_referencia = parametros_referencia;
            this.parametros_valor = parametros_valor;
            this.tipo_funcion = tipo_funcion;
            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;

            this.tipo_Final = null;
        }

        public override object ejecutar(Entorno entorno)
        {
            return null;
        }

        private bool validaciones(string p_id, string p_tipo, Entorno entorno, Tipo p_tipoFinal)
        {            

            if (p_tipo != string.Empty)
            {
                p_tipoFinal = new Tipo(p_tipo);

                if(p_tipoFinal.tipo == Tipos.OBJECT)//object//array//nativo(otra vez)->id //no existe,error
                {
                    throw new ErrorPascal("por implementar",0,0,"semantico");
                }
                //nativo,array..sigue normal...
            }
            else
                throw new ErrorPascal("[Funcion] El tipo de dato para la funcion no viene especificada", 0, 0, "semantico");




            if (p_id != string.Empty)//no vacio
            {
                if(entorno.getFuncion(p_id) == null)//no exista, puede ser directo porque no hay sobrecarga, porque lo que el id, sera el id y no sera compuesto por sus parametros
                {
                    entorno.guardarFuncion(p_id, this);//se guarda
                }
                else
                {
                    throw new ErrorPascal("[Funcion] La funcion con el id ya existe. "+p_id, 0, 0, "semantico");
                }
            }
            else
                throw new ErrorPascal("[Funcion] El id para la funcion no viene especificada", 0, 0, "semantico");
            return false;
        }
    }
}
/*
una funcion: abue1_padre1_nieta1 puede ir a usar la funcion: abue2_padre2_nieta2, 
tomando en cuenta que son de diferentes ambitos pero al desanidar ambas nietas 
ya estan en el ambito global?

todas estas validaciones y otras (ver fotos de anotaciones) se hacen pero en el traductor,
aqui en el interprete solo es ejecutar. 


*/

/*
if (entorno.getVariable(p_tipoFinal.tipoAuxiliar) != null)//getArray
{
    //object //variable
    //array //variable
    //nativo
}
else
{
    //no existe esa variable [ese tipo]
}
*/