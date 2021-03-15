using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class FunctionInstruccion : Instruccion
    {
        private string id;

        private LinkedList<ParametroInst> lista_parametros;        

        private string tipo_funcion_nativo_id; //== ""

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;

        public bool isFuncion; //==false

        public FunctionInstruccion(string id, LinkedList<ParametroInst> lista_parametros, string tipo_funcion_nativo_id, 
            LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones, bool isFuncion)
        {
            this.id = id;

            this.lista_parametros = lista_parametros;

            this.tipo_funcion_nativo_id = tipo_funcion_nativo_id;

            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;

            this.isFuncion = isFuncion;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Tipo tipo_funcion = null;
                if (tipo_funcion_nativo_id != string.Empty && isFuncion)
                {
                    tipo_funcion = new Tipo(tipo_funcion_nativo_id, entorno);

                    //if (tipo_funcion.tipo == Tipos.OBJECT)//object//array//nativo(otra vez)->id //no existe,error
                    //{
                        //if (entorno.getVariable(tipo_funcion.tipoAuxiliar) == null)
                        //    throw new ErrorPascal("El tipo de dato no existe",0,0,"semantico");
                    //}
                    //nativo..sigue normal...
                }
                else if (isFuncion)//false==sigue normal
                {
                    throw new ErrorPascal("[Funcion] El tipo de dato para la funcion no viene especificada", 0, 0, "semantico");
                }



                if (id != string.Empty)
                {
                    //no exista, puede ser directo porque no hay sobrecarga, porque lo que el id, sera el id y no sera compuesto por sus parametros
                    if (entorno.getFuncion(id) == null)
                    {
                        if(isFuncion)
                            entorno.guardarFuncion(id, new Funcion(id, tipo_funcion, lista_parametros, header_instrucciones, body_instrucciones, isFuncion));
                        else
                            entorno.guardarFuncion(id, new Funcion(id, tipo_funcion, lista_parametros, header_instrucciones, body_instrucciones, isFuncion));
                        //se guardo la funcion correctamente
                    }
                    else
                        throw new ErrorPascal("[Funcion] La funcion con el id ya existe. " + id, 0, 0, "semantico");
                }
                else
                    throw new ErrorPascal("[Funcion] El id para la funcion no viene especificada", 0, 0, "semantico");
            }
            catch (Exception ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en la declaracion de funcion", 0, 0, "semantico");
            }
            return null;
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