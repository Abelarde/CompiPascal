using CompiPascal.interprete.expresion;
using CompiPascal.interprete.instruccion;
using CompiPascal.interprete.simbolo;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CompiPascal.interprete.analizador
{

    //TODO: strings de tipos para no usar strings
    //Token.Text o Token.ValueString

    /// <summary>
    /// Como ya hemos mencionado, Irony no acepta acciones
    /// entre sus producciones, se limita a devolver el
    /// AST(Abstract Syntax Tree) que arma luego de ser
    /// aceptada la cadena de entrada.
    /// </summary>
    /// 
    class SintacticoInterprete
    {
        private string txtOutput = string.Empty;
        private string[,] errors;

        public void analizar(String cadena)
        {
            GramaticaInterprete gramatica = new GramaticaInterprete();//cargar el arbol 
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);

            //AST devuelto por Irony que sera posteriormente recorrido y analizado
            ParseTree arbol = parser.Parse(cadena);
            //cada uno de los nodos del ParseTree... nos interesara el atributo .ChildNodes
            ParseTreeNode raiz = arbol.Root;


            /*foreach (var item in lenguaje.Errors) //lenguaje.Errors.Count
            {
                //OutputMessage("gramatica: " + item);
                errors[fila, 0] = "gramatica";
                errors[fila, 1] = item.Message;
                errors[fila, 2] = "-";
                errors[fila, 3] = "-";
                errors[fila, 4] = "-";
                fila++;
            }
            */

            if (raiz != null)
            {
                //ChildNodes... tipo Array y contiene todas las cualidades de una lista
                //si esta lista = vacia = nodo es una hoja
                // != entonces tiene un subarbol
                //se le manda el nodo RAIZ del arbol 
                //recorrido al AST ... se le manda el nodo raiz del arbol

                //instrucciones(raiz.ChildNodes.ElementAt(0));
                OutputMessage("Analisis exitosamente");
                FillErrors(arbol);
                LinkedList<Instruccion> listaInstrucciones = instrucciones(raiz.ChildNodes.ElementAt(0));
                //ejecutar(listaInstrucciones);
            }
            else
            {
                WarningMessage("La cadena de entrada no es correcta");
                FillErrors(arbol);
            }

        }

        public void ejecutar(LinkedList<Instruccion> instrucciones)
        {
            Entorno global = new Entorno(null);
            foreach (var instruccion in instrucciones)
            {
                if(instruccion != null)
                {
                    instruccion.ejecutar(global);
                }
            }
        }


        /*
         * 1) cuantas producciones tiene
         * 2) condiciones para saber que produccion esta reconociendo [puede basarse en la cantidad de hijos de la produccion] 
         * 3) tambien la cantidad de ChildNodes o la posicion del elemento que nos interese puede variar DEPENDIENDO de las
         * preferencias que le colocamos al AST en nuestra gramatica, por ejemplo el metodo MarkPunctuation() nos elimina nodos
         * que no nos interesa y por lo tanto nos modifica la cantidad de los ChildNodes de nuestro arbol.
         */

        /* cuantas producciones tiene este NO TERMINAL */
        private LinkedList<Instruccion> instrucciones(ParseTreeNode actual)
        {
            //lista de instrucciones general
            LinkedList<Instruccion> listaInstrucciones = new LinkedList<Instruccion>();

            //cuerpo del programa exceptuando el id de la clase
            ParseTreeNode program = actual.ChildNodes.ElementAt(3);
            ParseTreeNode header_statements = program.ChildNodes.ElementAt(0);
            ParseTreeNode constant_optional = header_statements.ChildNodes.ElementAt(0);
            if(constant_optional.ChildNodes.Count > 0) //Empty
            {
                ParseTreeNode constant_declarations = constant_optional.ChildNodes.ElementAt(0);
                listaInstrucciones.AddLast(instruccion(constant_declarations)); //constantes
            }

            ParseTreeNode statements = header_statements.ChildNodes.ElementAt(1);       
            if(statements.ChildNodes.Count > 0) //statement+  (puede venir o no puede venir) 
            {
                ParseTreeNode statement_starRule = statements.ChildNodes.ElementAt(0); 
                foreach (ParseTreeNode statement in statement_starRule.ChildNodes)//statement+
                {
                    listaInstrucciones.AddLast(instruccion(statement.ChildNodes.ElementAt(0)));
                }

            }

            //TODO: hacer un metodo para el header y el body statement para que me llenen la lista de instrucciones

            ParseTreeNode body_statements = program.ChildNodes.ElementAt(1);

            main_declarations(listaInstrucciones, body_statements.ChildNodes.ElementAt(0));


            //TODO: hacer una pasada para recoger las funciones o procedimientos o las pasadas que me interesen para lo que me interese

            return listaInstrucciones;


            /*
            if (actual.ChildNodes.Count == 2)
            {
                instruccion(actual.ChildNodes.ElementAt(0));
                instrucciones(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                instruccion(actual.ChildNodes.ElementAt(0));
            }
            */
        }

        /* cuantas producciones tiene este NO TERMINAL */
        private Instruccion instruccion(ParseTreeNode actual)
        {
            string sentencia = actual.ChildNodes.ElementAt(0).Term.Name;
            switch (sentencia)
            {
                case "CONST":
                    ParseTreeNode constant_variables = actual.ChildNodes.ElementAt(1);
                    if(constant_variables.ChildNodes.Count > 0)
                    {
                        ParseTreeNode constant_variable_plusRule = constant_variables.ChildNodes.ElementAt(0);
                        foreach(ParseTreeNode constant_variable in constant_variable_plusRule.ChildNodes)
                        {
                            return new Const(constant_variable.ChildNodes.ElementAt(0).Token.Text, Expresion(constant_variable.ChildNodes.ElementAt(2)));
                        }
                    }
                    return null; //TODO: esto?
                case "TYPE":
                    ParseTreeNode type_variables = actual.ChildNodes.ElementAt(1);
                    if (type_variables.ChildNodes.Count > 0)
                    {
                        ParseTreeNode type_variable_plusRule = type_variables.ChildNodes.ElementAt(0);
                        foreach (ParseTreeNode type_variable in type_variable_plusRule.ChildNodes)//cada linea del type
                        {
                            LinkedList<string> lista_ids = list_id(type_variable.ChildNodes.ElementAt(0)); //futura lista de id's

                            //TODO: verificar si esta bien que le mande la lista de ids o mejor creo un nuevo objeto por cada uno.
                            //o talvez aqui si pero en el metod ejecutar ya creo un simbolo individual por cada uno de la lista y
                            //al mandar por referencia seria la direccion del simbolo y no de esta clase que hereda de Instruccion.

                            ParseTreeNode variables_native_array = type_variable.ChildNodes.ElementAt(2);//tipo de los id's

                            if(variables_native_array.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name != "ARRAY")//tipos nativos
                            {
                                return new TypeInstruccion(lista_ids, variables_native_array.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name);
                            }
                            else //tipos array
                            {
                                ParseTreeNode variables_array = variables_native_array.ChildNodes.ElementAt(0);
                                return new TypeInstruccion(lista_ids, variables_native_array.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name, Expresion(variables_array.ChildNodes.ElementAt(2)), variables_array.ChildNodes.ElementAt(5).Token.Text);
                            }
                        }
                    }
                    return null; //TODO: esto?
                case "VAR":
                    ParseTreeNode variables = actual.ChildNodes.ElementAt(1);
                    if(variables.ChildNodes.Count > 0)
                    {

                        ParseTreeNode variable_plusRule = variables.ChildNodes.ElementAt(0);
                        foreach (ParseTreeNode variable in variable_plusRule.ChildNodes)
                        {
                            LinkedList<string> lista_ids = list_id(variable.ChildNodes.ElementAt(0));


                            string tipo = variables_native_id(variable.ChildNodes.ElementAt(2));


                            ParseTreeNode variable_initialization = variable.ChildNodes.ElementAt(3);
                            Expresion valor = null;
                            if(variable_initialization.ChildNodes.Count > 0) //Empty
                            {
                                valor = Expresion(variable_initialization.ChildNodes.ElementAt(1));
                            }


                            return new Var(lista_ids, tipo, valor);

                        }

                    }
                    return null; //TODO: esto?
                case "FUNCTION":
                    ParseTreeNode functions_declarations = actual;

                    string nombre_funcion = functions_declarations.ChildNodes.ElementAt(1).Token.Text;


                    LinkedList<string> parametros_referencia_funcion = new LinkedList<string>();
                    LinkedList<string> parametros_valor_funcion = new LinkedList<string>();
                    parameters(parametros_referencia_funcion, parametros_valor_funcion, functions_declarations.ChildNodes.ElementAt(3));
                    


                    string tipo_funcion = variables_native_id(functions_declarations.ChildNodes.ElementAt(6));


                    return new FunctionInstruccion(nombre_funcion, parametros_referencia_funcion, parametros_valor_funcion, tipo_funcion, 
                        instrucciones(functions_declarations.ChildNodes.ElementAt(8)), instrucciones(functions_declarations.ChildNodes.ElementAt(9)));
                case "PROCEDURE":

                    ParseTreeNode procedures_declarations = actual;

                    string nombre_procedimiento = procedures_declarations.ChildNodes.ElementAt(1).Token.Text;


                    LinkedList<string> parametros_referencia_procedimiento = new LinkedList<string>();
                    LinkedList<string> parametros_valor_procedimiento = new LinkedList<string>();
                    parameters(parametros_referencia_procedimiento, parametros_valor_procedimiento, procedures_declarations.ChildNodes.ElementAt(3));



                    return new ProcedureInstruccion(nombre_procedimiento, parametros_referencia_procedimiento, parametros_valor_procedimiento,
                        instrucciones(procedures_declarations.ChildNodes.ElementAt(6)), instrucciones(procedures_declarations.ChildNodes.ElementAt(7)));
                case "IF":
                    //TODO: quizas en el constructor seria bueno enviar el tipo de if con un char para
                    //facilidad de validar que tipo de if es en el metodo ejecutar
                    ParseTreeNode if_statements = actual;

                    LinkedList<Instruccion> if_lista_instrucciones = new LinkedList<Instruccion>();
                    main_declarations(if_lista_instrucciones, if_statements.ChildNodes.ElementAt(3));



                    if (if_statements.ChildNodes.Count == 4)
                    {
                        return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones);

                    }else if (if_statements.ChildNodes.Count == 6)
                    {
                        LinkedList<Instruccion> else_lista_instrucciones = new LinkedList<Instruccion>();
                        main_declarations(else_lista_instrucciones, if_statements.ChildNodes.ElementAt(5));

                        return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones,
                            else_lista_instrucciones);

                    }
                    else if(if_statements.ChildNodes.Count == 7)
                    {

                        LinkedList<Expresion> elseIf_lista_valor_condicional = new LinkedList<Expresion>();
                        LinkedList<Instruccion> elseIf_lista_instrucciones = new LinkedList<Instruccion>();

                        ParseTreeNode else_if_list = if_statements.ChildNodes.ElementAt(4);
                        if (else_if_list.ChildNodes.Count > 0)
                        {
                            ParseTreeNode else_if_plusRule = else_if_list.ChildNodes.ElementAt(0);
                            foreach (ParseTreeNode else_if in else_if_plusRule.ChildNodes)
                            {
                                elseIf_lista_valor_condicional.AddLast(Expresion(else_if.ChildNodes.ElementAt(2)));
                                main_declarations(elseIf_lista_instrucciones, else_if.ChildNodes.ElementAt(4));
                            }
                        }


                        LinkedList<Instruccion> else_lista_instrucciones = new LinkedList<Instruccion>();
                        main_declarations(else_lista_instrucciones, if_statements.ChildNodes.ElementAt(6));

                        return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones,
                            elseIf_lista_valor_condicional,
                            elseIf_lista_instrucciones,
                            else_lista_instrucciones);

                    }
                    else
                    {
                        return null;
                    }

                case "CASE":
                    ParseTreeNode case_statements = actual;
                    ParseTreeNode case_list = case_statements.ChildNodes.ElementAt(3);

                    LinkedList<Expresion> cases_valores = new LinkedList<Expresion>();
                    LinkedList<Instruccion> cases_Instrucciones = new LinkedList<Instruccion>();

                    if (case_list.ChildNodes.Count > 0)
                    {
                        ParseTreeNode cases_plusRule = case_list.ChildNodes.ElementAt(0);
                        foreach(ParseTreeNode cases in cases_plusRule.ChildNodes)
                        {
                            cases_valores.AddLast(Expresion(cases.ChildNodes.ElementAt(0)));
                            main_declarations(cases_Instrucciones, cases.ChildNodes.ElementAt(2));
                        }
                    }


                    ParseTreeNode cases_words = case_statements.ChildNodes.ElementAt(4);
                    LinkedList<Instruccion> else_or_otherwise_instrucciones = new LinkedList<Instruccion>();

                    if (cases_words.ChildNodes.Count > 0)//Empty
                    { 
                        //if por si me interesa saber de donde, sino no seria necesario esta validacion
                        if(cases_words.ChildNodes.ElementAt(0).Term.Name == "ELSE")
                        {
                            main_declarations(else_or_otherwise_instrucciones, cases_words.ChildNodes.ElementAt(1));
                        }
                        else//"OTHERWISE"
                        {
                            main_declarations(else_or_otherwise_instrucciones, cases_words.ChildNodes.ElementAt(1));
                        }
                    }

                    return new CaseInstruccion(Expresion(case_statements.ChildNodes.ElementAt(1)), cases_valores,
                        cases_Instrucciones, else_or_otherwise_instrucciones);

                case "WHILE":
                    ParseTreeNode while_statements = actual;

                    LinkedList<Instruccion> while_lista_instrucciones = new LinkedList<Instruccion>();
                    main_declarations(while_lista_instrucciones, while_statements.ChildNodes.ElementAt(3));

                    return new WhileInstruccion(Expresion(while_statements.ChildNodes.ElementAt(1)), while_lista_instrucciones);
                case "FOR":
                    ParseTreeNode for_do_statements = actual;

                    LinkedList<Instruccion> for_lista_instrucciones = new LinkedList<Instruccion>();
                    main_declarations(for_lista_instrucciones, for_do_statements.ChildNodes.ElementAt(7));

                    return new ForInstruccion(Expresion(for_do_statements.ChildNodes.ElementAt(1)),
                        Expresion(for_do_statements.ChildNodes.ElementAt(3)),
                        Expresion(for_do_statements.ChildNodes.ElementAt(5)),
                        for_lista_instrucciones);
                
                case "REPEAT":
                    return new RepeatInstruccion();
                case "BREAK":
                    return new BreakInstruccion();
                case "CONTINUE":
                    return new ContinueInstruccion();
                default:
                    switch (actual.Term.Name)
                    {
                        case "variable_ini":
                            return new VariableInicializarInstruccion();
                        case "array_ini":
                            return new ArrayInicializarInstruccion();
                        case "function_call":
                            return new FunctionCallInstruccion();
                        case "procedure_call":
                            return new ProcedureCallinstruccion();
                        case "array_call":
                            return new ArrayCallInstruccion();
                        default:                            
                            return null;//if a validar que no sea null cuando lo recorro
                    }
            }
        }

        private Expresion Expresion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)//expression:=
            {
                //Token == terminal [lexema]
                //Term == no terminal [nombre del no terminal o terminal]
                string tokenOperador = actual.ChildNodes.ElementAt(1).Token.Text.ToUpper();
                switch (tokenOperador)
                {
                    case "+":
                        return new Aritmetica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "+");
                    case "-":
                        return new Aritmetica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "-");
                    case "*":
                        return new Aritmetica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "*");
                    case "/":
                        return new Aritmetica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "/");
                    case "%":
                        return new Aritmetica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "%");

                    case "=":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "=");
                    case "<>":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "<>");
                    case ">":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), ">");
                    case "<":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "<");
                    case ">=":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), ">=");
                    case "<=":
                        return new Relacional(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "<=");

                    case "AND":
                        return new Logica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "AND");
                    case "OR":
                        return new Logica(Expresion(actual.ChildNodes[0]), Expresion(actual.ChildNodes[2]), "OR");
                    default: //" ( expresion ) "
                        return Expresion(actual.ChildNodes.ElementAt(1));
                }

            }
            else if (actual.ChildNodes.Count == 2)
            {
                string operador = actual.ChildNodes.ElementAt(0).Token.Text.ToUpper();
                switch (operador)
                {
                    case "-":
                        return new Aritmetica(null, Expresion(actual.ChildNodes[1]), "-");
                    case "+":
                        return new Aritmetica(null, Expresion(actual.ChildNodes[1]), "+");
                    default: //"NOT"
                        return new Logica(null, Expresion(actual.ChildNodes[1]), "NOT");
                }
            }
            else
            {
                //Literal == lo mas pequeno de una expresion, es decir que tienen valor real de compilacion

                if(actual.ChildNodes.ElementAt(0).ChildNodes.Count == 1) //expression_terminales:=
                {
                    string tipoDeDato = actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name;
                    switch (tipoDeDato)
                    {
                        case "ID":
                            return new Literal("ID", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        case "CADENA":
                            return new Literal("CADENA", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        case "NUMBER":
                            return new Literal("NUMBER", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        case "TRUE":
                            return new Literal("TRUE", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        case "FALSE":
                            return new Literal("FALSE", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        case "function_call":
                            return new Literal("function_call", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                        default: //"array_call"
                            return new Literal("array_call", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                    }
                }
                else
                {
                    //".."
                    return new Literal("..", actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text, actual.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2).Token.Text);
                }
            }
            
        }


        private string variables_native_id(ParseTreeNode variables_native_id) //actual = variables_native_id
        {
            //un mejor control de los tipos, no importa si es nativo o predefinido
            if (variables_native_id.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name != "ID")//tipos nativos
            {
                return variables_native_id.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name; //en mayuscula todos
            }
            else //tipos id (es decir de otro tipo predefinido)
            {
                return variables_native_id.ChildNodes.ElementAt(0).Token.Text;//como sea
            }

        }
        private LinkedList<string> list_id(ParseTreeNode list_id)
        {
            LinkedList<string> lista_ids = new LinkedList<string>(); //futura lista de id's

            if (list_id.ChildNodes.Count > 0) //id+
            {
                ParseTreeNode id_plusRule = list_id.ChildNodes.ElementAt(0);
                foreach (ParseTreeNode id in id_plusRule.ChildNodes)//cada id
                {
                    lista_ids.AddLast(id.Token.Text);//agrego cada id
                }
            }

            return lista_ids;
        }
        private void parameters(LinkedList<string> parametros_referencia, LinkedList<string> parametros_valor, ParseTreeNode parameters)
        {
            if (parameters.ChildNodes.Count > 0) //parameter+
            {
                ParseTreeNode parameter_starRule = parameters.ChildNodes.ElementAt(0);
                foreach (ParseTreeNode parameter in parameter_starRule.ChildNodes)
                {
                    if (parameter.ChildNodes.ElementAt(0).Term.Name == "VAR")//referencia
                    {

                        parametros_referencia = list_id(parameter.ChildNodes.ElementAt(1));

                    }
                    else//valor
                    {
                        parametros_valor = list_id(parameter.ChildNodes.ElementAt(0));

                    }
                }
            }

        }
        private void main_declarations(LinkedList<Instruccion> listaInstrucciones, ParseTreeNode main_declarations)
        {
            ParseTreeNode begin_end_statements = main_declarations.ChildNodes.ElementAt(1);
            if (begin_end_statements.ChildNodes.Count > 0)//begin_end_statement+
            {
                ParseTreeNode begin_end_statement_starRule = begin_end_statements.ChildNodes.ElementAt(0);
                foreach (ParseTreeNode begin_end_statement in begin_end_statement_starRule.ChildNodes) //begin_end_statement+
                {
                    if (begin_end_statement.ChildNodes.Count > 0) //Empty
                    {
                        listaInstrucciones.AddLast(instruccion(begin_end_statement.ChildNodes.ElementAt(0)));
                    }
                }
            }
        }




        public void graficar(String cadena)
        {
            //txtOutput = string.Empty;

            GramaticaInterprete gramatica = new GramaticaInterprete();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);

            //AST devuelto por Irony que sera posteriormente recorrido y analizado
            ParseTree arbol = parser.Parse(cadena);
            //cada uno de los nodos del ParseTree... nos interesara el atributo .ChildNodes
            ParseTreeNode raiz = arbol.Root;

            //ChildNodes... tipo Array y contiene todas las cualidades de una lista
            //si esta lista = vacia = nodo es una hoja
            // != entonces tiene un subarbol
            //se le manda el nodo RAIZ del arbol 
            //recorrido al AST ... se le manda el nodo raiz del arbol
            if (raiz != null)
            {
                graficarAstIrony(raiz, "ast");
                FillErrors(arbol);
            }
            else
            {
                //TODO: necesitaria saber la linea, columan, token, estructura?, informacion del error en general.
                WarningMessage("La cadena de entrada no es correcta");
                FillErrors(arbol);
            }

        }

        private void graficarAstIrony(ParseTreeNode actual, string nombreGrafica)
        {
            string grafoDOT = AstIronyDOT.getDot(actual);
            Bitmap bm = FileDotEngine.Run(grafoDOT, nombreGrafica);

            if (bm != null)
            {
                OutputMessage("Imagen generada exitosamente: " + "ast");
            }
            else
            {
                WarningMessage("Error al generar la grafica para: " + "ast");
            }

            /*
             * NOTE: esta libreria saca algunas excepciones respecto a la memoria, por lo tanto no se utiliza porque
             * ya no hay versiones mas actulizadas, en lugar de ello se utiliza la clase FileDotEngine.
             * Se deja comentado a fin de conocer que existe este otro metodo muy conocido pero fallido.
             * 
             * WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
             * WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDOT);
             * img.Save("C:\\Users\\eduab\\Desktop\\Compiladores 2\\Proyecto1\\Proyecto1\\output");
             * 
             */


        }

        private void OutputMessage(string msn) => txtOutput += "[OUTPUT]  " + msn + "\r\n";

        private void WarningMessage(string msn) => txtOutput += "[WARNING]  " + msn + "\r\n";

        private void FillErrors(ParseTree arbol)
        {
            errors = new string[arbol.ParserMessages.Count, 5];
            int fila = 0;

            if (arbol.ParserMessages.Count > 0)
            {
                //string errors = string.Empty; //tipo, descripcion, linea, columna, extra
                foreach (var item in arbol.ParserMessages)
                {
                    //error lexico
                    if (item.Message.Contains("Invalid character"))
                    {
                        errors[fila, 0] = "lexico";
                        errors[fila, 1] = item.Message;
                        errors[fila, 2] = item.Location.Line.ToString();
                        errors[fila, 3] = item.Location.Column.ToString();
                        errors[fila, 4] = item.Location.Position.ToString();
                    }
                    //error sintactico
                    else
                    {
                        errors[fila, 0] = "sintactico";
                        errors[fila, 1] = item.Message;
                        errors[fila, 2] = item.Location.Line.ToString();
                        errors[fila, 3] = item.Location.Column.ToString();
                        errors[fila, 4] = item.Location.Position.ToString();
                    }
                    fila++;
                }

            }
        }

        public string Message() => txtOutput;

        public string[,] Errores() => errors;

        public string ClearMessage() => txtOutput = string.Empty;

        public void ClearErrores() => errors = null;


    }
}
