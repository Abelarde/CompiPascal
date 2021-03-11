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

        private ParametroInst[] parametros;        

        private string tipo_funcion_nativo_id;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;


        public FunctionInstruccion(string id, ParametroInst[] parametros, string tipo_funcion_nativo_id, 
            LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id = id;

            this.parametros = parametros;

            this.tipo_funcion_nativo_id = tipo_funcion_nativo_id;

            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Tipo tipo;
                //validar Tipo
                if (tipo_funcion_nativo_id != string.Empty)
                {
                    tipo = new Tipo(tipo_funcion_nativo_id, entorno);

                    if (tipo.tipo == Tipos.OBJECT)//object//array//nativo(otra vez)->id //no existe,error
                    {
                        if (entorno.getVariable(tipo.tipoAuxiliar) == null)
                            throw new ErrorPascal("El tipo no de dato no existe",0,0,"semantico");
                    }
                    //nativo..sigue normal...
                }
                else
                    throw new ErrorPascal("[Funcion] El tipo de dato para la funcion no viene especificada", 0, 0, "semantico");

                Parametro[] param = null;

                if (parametros != null)
                {
                    param = new Parametro[ParametroInst.totalVariables];

                    for(int i = 0; i < parametros.Length; i++)
                    {
                        foreach(string id in parametros[i].lista_ids)
                        {                            
                            param[i] = new Parametro(new Simbolo(new Tipo(parametros[i].tipo_nativo_id, entorno), id, null), parametros[i].isReferencia, parametros[i].tipo_nativo_id);
                        }                        
                    }
                }


                if (id != string.Empty)//no vacio
                {
                    //no exista, puede ser directo porque no hay sobrecarga, porque lo que el id, sera el id y no sera compuesto por sus parametros
                    if (entorno.getFuncion(id) == null)
                    {
                        entorno.guardarFuncion(id, new Funcion(id, tipo, param, header_instrucciones, body_instrucciones));//se guarda
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
                ex.ToString();
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