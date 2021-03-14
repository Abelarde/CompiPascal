using CompiPascal.traductor.expresion;
using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.traductor.analizador;

namespace CompiPascal.traductor.instruccion
{
    class FunctionInstruccion : Instruccion
    {
        public string id;

        public LinkedList<ParametroInst> lista_parametros;        

        private string tipo_funcion_nativo_id; //== ""

        public LinkedList<Instruccion> header_instrucciones;
        public LinkedList<Instruccion> body_instrucciones;

        public bool isFuncion; //==false

        private string traductor_cadena;
        private string traductor_func_proc;
        private string trad_parametros;
        private string trad_tipo_func;
        private string trad_header;
        private string trad_body;

        public FunctionInstruccion(string id, LinkedList<ParametroInst> lista_parametros, string tipo_funcion_nativo_id, 
            LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones, bool isFuncion)
        {
            this.id = id;

            this.lista_parametros = lista_parametros;

            this.tipo_funcion_nativo_id = tipo_funcion_nativo_id;

            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;

            this.isFuncion = isFuncion;

            traductor_cadena = string.Empty;
            traductor_func_proc = string.Empty;
            trad_parametros = string.Empty;
            trad_tipo_func = string.Empty;
            trad_header = string.Empty;
            trad_body = string.Empty;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {

                Tipo tipo_funcion = null;
                if (tipo_funcion_nativo_id != string.Empty && isFuncion)
                {
                    tipo_funcion = new Tipo(tipo_funcion_nativo_id, entorno);
                    trad_tipo_func = " : " + tipo_funcion_nativo_id;
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
                        if (isFuncion)
                        {
                            this.traductor_func_proc = "FUNCTION";
                            entorno.guardarFuncion(id, new Funcion(id, tipo_funcion, lista_parametros, header_instrucciones, body_instrucciones, isFuncion));
                        }
                        else
                        {
                            this.traductor_func_proc = "PROCEDURE";
                            entorno.guardarFuncion(id, new Funcion(id, tipo_funcion, lista_parametros, header_instrucciones, body_instrucciones, isFuncion));                          
                        }
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

            LinkedList<ParametroInst> nueva_lista = new LinkedList<ParametroInst>();
            nueva_lista = lista_parametros;

            int contadorTotal = 1;
            int contadorIDs = 1;
            string listaid = string.Empty;
            string tra_parametro = string.Empty;
            foreach (ParametroInst parametro in lista_parametros)
            {
                contadorIDs = 1;
                foreach(string ids in parametro.lista_ids)
                {
                    if(contadorIDs == parametro.lista_ids.Count)
                        listaid += ids;
                    else
                        listaid += ids + ",";

                    contadorIDs++;
                }

                if (parametro.isReferencia)
                {
                    if(contadorTotal != lista_parametros.Count)
                        tra_parametro = " var " + listaid + " : " + parametro.tipo_nativo_id + "; ";
                    else
                        tra_parametro = " var " + listaid + " : " + parametro.tipo_nativo_id;
                }
                else
                {
                    if (contadorTotal != lista_parametros.Count)
                        tra_parametro = " " + listaid + " : " + parametro.tipo_nativo_id + "; ";
                    else
                        tra_parametro = " " + listaid + " : " + parametro.tipo_nativo_id;
                }
                contadorTotal++;
                trad_parametros += tra_parametro;
                listaid = string.Empty;
            }


            string hijos = string.Empty;
            foreach(Instruccion header in header_instrucciones)
            {
                if(header is FunctionInstruccion)
                {
                    FunctionInstruccion convertido = header as FunctionInstruccion;
                    if(convertido != null)
                    {
                        foreach(ParametroInst pa in nueva_lista)
                        {
                            convertido.lista_parametros.AddLast(pa);
                        }

                        foreach(Instruccion ins in header_instrucciones)
                        {
                            if(ins is ListaVarInstruccion)
                            {
                                convertido.header_instrucciones.AddLast(ins);
                            }
                        }

                        convertido.id = this.id + "_" + convertido.id;
                        object resultado = header.ejecutar(entorno);
                        if (resultado != null)
                            hijos += Convert.ToString(resultado);
                    }
                }
                else
                {
                    object resultado = header.ejecutar(entorno);
                    if (resultado != null)
                        trad_header += Convert.ToString(resultado);
                }
            }



            foreach(Instruccion body in body_instrucciones)
            {
                if(body is CallFuncProc)
                {
                    CallFuncProc convertido = body as CallFuncProc;
                    if(convertido != null)
                    {
                        //p11() 
                        //p1_p11 
                        convertido.id = entorno.getFuncionIDFaltante(convertido.id);
                        object resultado = body.ejecutar(entorno);
                        if (resultado != null)
                            trad_body += Convert.ToString(resultado);
                    }
                }
                else
                {
                    object resultado = body.ejecutar(entorno);
                    if (resultado != null)
                        trad_body += Convert.ToString(resultado);
                }

            }

            //agregar saltos de lineas y espacios
            return traductor_func_proc + " " + id + "(" + trad_parametros + ")" + trad_tipo_func + ";" + Environment.NewLine
                + trad_header  
                + "begin" + Environment.NewLine 
                + trad_body
                + "end" + ";" + Environment.NewLine
                + hijos;
        }
    }
}
/*
una funcion: abue1_padre1_nieta1 puede ir a usar la funcion: abue2_padre2_nieta2, 
tomando en cuenta que son de diferentes ambitos pero al desanidar ambas nietas 
ya estan en el ambito global?

todas estas validaciones y otras (ver fotos de anotaciones) se hacen pero en el traductor,
aqui en el traductor solo es ejecutar. 


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