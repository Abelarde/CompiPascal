using CompiPascal.interprete.expresion;
using CompiPascal.interprete.instruccion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CompiPascal.interprete.analizador
{

    //TODO: strings de tipos para no usar strings
    //Token.Text o Token.ValueString
    //arreglar cuando en la gramatica venga ID cambiarlo a expression

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
        Form1 form;

        public void analizar(String cadena, Form1 formp)
        {
           // try
           //{
                form = formp;

                GramaticaInterprete gramatica = new GramaticaInterprete();
                LanguageData lenguaje = new LanguageData(gramatica);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(cadena);
                ParseTreeNode ini = arbol.Root;
                //aqui podria colocar el analizador de errores de la gramatica

                if (ini != null)
                {
                    OutputMessage("Analisis exitosamente");
                    FillErrors(arbol);
                    ParseTreeNode program_structure = ini.ChildNodes.ElementAt(0);
                    LinkedList<Instruccion> listaInstrucciones = instrucciones(program_structure);
                    ejecutar(listaInstrucciones);
                }
                else
                {
                    WarningMessage("La cadena de entrada no es correcta");
                    FillErrors(arbol);
                }

                foreach (string mensaje in ErrorPascal.cola)
                {
                    form.Write(mensaje + Environment.NewLine);
                }

            //}
            //catch (Exception ex)
            //{
            //    form.Write(ex.ToString() + Environment.NewLine);
            //}
        }

        public void ejecutar(LinkedList<Instruccion> instrucciones)
        {
            Entorno global = new Entorno(null);

            foreach (var instruccion in instrucciones)
            {
                if(instruccion is FunctionInstruccion )
                {
                    instruccion.ejecutar(global);
                }
            }

            foreach (var instruccion in instrucciones)
            {
                if (instruccion != null && !(instruccion is FunctionInstruccion))
                {
                    instruccion.ejecutar(global);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program_structure">nodo program_structure</param>
        /// <returns>lista de instrucciones de todo el programa, header_statements y body_statements</returns>
        private LinkedList<Instruccion> instrucciones(ParseTreeNode program_structure)
        {
            LinkedList<Instruccion> listaInstrucciones = new LinkedList<Instruccion>();

            ParseTreeNode program = program_structure.ChildNodes.ElementAt(3);

            header_statements(listaInstrucciones, program.ChildNodes.ElementAt(0));

            body_statements(listaInstrucciones, program.ChildNodes.ElementAt(1));
                  
            return listaInstrucciones;
        }

        private Instruccion header_statements_return(ParseTreeNode opciones_header)
        {
            switch (opciones_header.Term.Name)
            {
                case "constant_variables":
                    return constant_variables(opciones_header);
                case "type_declarations":
                    return type_declarations(opciones_header);
                case "variables_declarations":
                    return variables_declarations(opciones_header);
                case "functions_declarations":
                    return functions_declarations(opciones_header);
                case "procedures_declarations":
                    return procedures_declarations(opciones_header);
                default:
                    return null;
            }
        }

        private void header_statements(LinkedList<Instruccion> listaInstrucciones, ParseTreeNode header_statements)
        {
            ParseTreeNode constant_optional = header_statements.ChildNodes.ElementAt(0);
            if (constant_optional.ChildNodes.Count > 0) //Empty
            {
                ParseTreeNode constant_declarations = constant_optional.ChildNodes.ElementAt(0);
                ParseTreeNode constant_variables = constant_declarations.ChildNodes.ElementAt(1);
                listaInstrucciones.AddLast(header_statements_return(constant_variables)); //constantes
            }

            ParseTreeNode statements = header_statements.ChildNodes.ElementAt(1);
            if (statements.ChildNodes.Count > 0) //statement+  (puede venir o no puede venir) 
            {
                foreach (ParseTreeNode statement in statements.ChildNodes)//statement+
                {
                    listaInstrucciones.AddLast(header_statements_return(statement.ChildNodes.ElementAt(0)));
                }
            }
        }

        private Instruccion main_declarations_return(ParseTreeNode opciones_main)
        {
            switch (opciones_main.Term.Name)
            {
                case "variable_ini":
                    return variable_ini(opciones_main);
                case "array_ini":
                    return array_ini(opciones_main);
                case "if_statements":
                    return if_statements(opciones_main);
                case "case_statements":
                    return case_statements(opciones_main);
                case "while_statements":
                    return while_statements(opciones_main);
                case "for_do_statements":
                    return for_do_statements(opciones_main);
                case "repeat_statements":
                    return repeat_statements(opciones_main);
                case "exit_statements":
                    return new ExitInstruccion(Expresion(opciones_main.ChildNodes.ElementAt(2)));
                //case "return_statements":
                //    return new ReturnInstruccion(opciones_main.ChildNodes.ElementAt(0).Token.Text, Expresion(opciones_main.ChildNodes.ElementAt(4)));
                case "write_statements":
                    return write_statements(opciones_main);
                case "graficar_statements":
                    return null;
                case "BREAK":
                    return new BreakInstruccion(true);
                case "CONTINUE":
                    return new ContinueInstruccion(true);
                //case "callFuncProc":
                //    return callFuncProc(opciones_main);
                case "union_1":
                    {
                        ParseTreeNode union_1_a = opciones_main.ChildNodes.ElementAt(4);
                        if (union_1_a.ChildNodes.ElementAt(0).Term.Name == "SEMI_COLON")
                            return callFuncProc(opciones_main);
                        else
                            return new ReturnInstruccion(opciones_main.ChildNodes.ElementAt(0).Token.Text, Expresion(union_1_a.ChildNodes.ElementAt(1)));
                    }

                default:
                    return null;
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
                        listaInstrucciones.AddLast(main_declarations_return(begin_end_statement.ChildNodes.ElementAt(0)));
                    }
                }
            }
        }

        private void body_statements(LinkedList<Instruccion> listaInstrucciones, ParseTreeNode body_statements)
        {
            main_declarations(listaInstrucciones, body_statements.ChildNodes.ElementAt(0));
        }


        private Instruccion variable_ini(ParseTreeNode variable_ini)
        {
            return new VariableInicializarInstruccion(Expresion(variable_ini.ChildNodes.ElementAt(0)), Expresion(variable_ini.ChildNodes.ElementAt(2)));
        }
        private Instruccion array_ini(ParseTreeNode array_ini)
        {
            LinkedList<Expresion> lista_expresiones = new LinkedList<Expresion>();
            ParseTreeNode expression_list_plus = array_ini.ChildNodes.ElementAt(2);
            foreach (ParseTreeNode expression in expression_list_plus.ChildNodes)
            {
                lista_expresiones.AddLast(Expresion(expression));
            }

            return new ArrayAsignar(Expresion(array_ini.ChildNodes.ElementAt(0)),
                lista_expresiones,
                Expresion(array_ini.ChildNodes.ElementAt(5)));
        }

        private Instruccion callFuncProc(ParseTreeNode callFuncProc)
        {
            LinkedList<Expresion> procedure_lista_expresiones = new LinkedList<Expresion>();
            expression_list(procedure_lista_expresiones, callFuncProc.ChildNodes.ElementAt(2));


            return new CallFuncProc(callFuncProc.ChildNodes.ElementAt(0).Token.Text,
                procedure_lista_expresiones);
        }

        private Instruccion write_statements(ParseTreeNode write_statements)
        {
            LinkedList<Expresion> lista_expresiones = new LinkedList<Expresion>();

            ParseTreeNode expression_list_plus = write_statements.ChildNodes.ElementAt(2);
            if (expression_list_plus.ChildNodes.Count > 0)
            {
                foreach (ParseTreeNode expression in expression_list_plus.ChildNodes)
                {
                    lista_expresiones.AddLast(Expresion(expression));
                }
            }

            bool newLine = false;
            if (write_statements.ChildNodes.ElementAt(0).Term.Name == "WRITELN")
                newLine = true;


            return new WriteInstruccion(lista_expresiones, newLine, form);

        }

        private Instruccion repeat_statements(ParseTreeNode repeat_statements)
        {
            LinkedList<Instruccion> repeat_lista_instrucciones = new LinkedList<Instruccion>();
            main_declarations(repeat_lista_instrucciones, repeat_statements.ChildNodes.ElementAt(1));


            return new RepeatInstruccion(repeat_lista_instrucciones, Expresion(repeat_statements.ChildNodes.ElementAt(3)));
        }
        private Instruccion for_do_statements(ParseTreeNode for_do_statements)
        {
            LinkedList<Instruccion> for_lista_instrucciones = new LinkedList<Instruccion>();
            main_declarations(for_lista_instrucciones, for_do_statements.ChildNodes.ElementAt(7));

            return new ForInstruccion(Expresion(for_do_statements.ChildNodes.ElementAt(1)),
                Expresion(for_do_statements.ChildNodes.ElementAt(3)),
                Expresion(for_do_statements.ChildNodes.ElementAt(5)),
                for_lista_instrucciones);

        }
        private Instruccion while_statements(ParseTreeNode while_statements)
        {
            LinkedList<Instruccion> while_lista_instrucciones = new LinkedList<Instruccion>();
            main_declarations(while_lista_instrucciones, while_statements.ChildNodes.ElementAt(3));

            return new WhileInstruccion(Expresion(while_statements.ChildNodes.ElementAt(1)), while_lista_instrucciones);
        }
        private Instruccion case_statements(ParseTreeNode case_statements)
        {
            ParseTreeNode case_list = case_statements.ChildNodes.ElementAt(3);

            LinkedList<Cases> cases_lista = new LinkedList<Cases>();

            if (case_list.ChildNodes.Count > 0)
            {
                foreach (ParseTreeNode cases in case_list.ChildNodes)
                {
                    LinkedList<Instruccion> cases_Instrucciones = new LinkedList<Instruccion>();

                    main_declarations(cases_Instrucciones, cases.ChildNodes.ElementAt(2));

                    Cases caso = new Cases(Expresion(cases.ChildNodes.ElementAt(0)), cases_Instrucciones);
                    cases_lista.AddLast(caso);
                }
            }


            ParseTreeNode cases_words = case_statements.ChildNodes.ElementAt(4);
            LinkedList<Instruccion> else_or_otherwise_instrucciones = new LinkedList<Instruccion>();

            if (cases_words.ChildNodes.Count > 0)//Empty
            {
                //if por si me interesa saber de donde, sino no seria necesario esta validacion
                if (cases_words.ChildNodes.ElementAt(0).Term.Name == "ELSE")
                    main_declarations(else_or_otherwise_instrucciones, cases_words.ChildNodes.ElementAt(1));
                else//"OTHERWISE"
                    main_declarations(else_or_otherwise_instrucciones, cases_words.ChildNodes.ElementAt(1));
            }

            return new CaseInstruccion(Expresion(case_statements.ChildNodes.ElementAt(1)), cases_lista,
                else_or_otherwise_instrucciones);
        }
        private Instruccion if_statements(ParseTreeNode if_statements)
        {
            LinkedList<Instruccion> if_lista_instrucciones = new LinkedList<Instruccion>();
            main_declarations(if_lista_instrucciones, if_statements.ChildNodes.ElementAt(3));


            if (if_statements.ChildNodes.Count == 4)
            {
                return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones);

            }
            else if (if_statements.ChildNodes.Count == 6)
            {
                LinkedList<Instruccion> else_lista_instrucciones = new LinkedList<Instruccion>();
                main_declarations(else_lista_instrucciones, if_statements.ChildNodes.ElementAt(5));

                return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones,
                    else_lista_instrucciones);

            }
            else if (if_statements.ChildNodes.Count == 7)
            {
                LinkedList<ElseIf> else_if_lista = new LinkedList<ElseIf>();

                ParseTreeNode else_if_list = if_statements.ChildNodes.ElementAt(4);
                if (else_if_list.ChildNodes.Count > 0)
                {
                    LinkedList<Instruccion> else_if_lista_instrucciones;

                    foreach (ParseTreeNode else_if in else_if_list.ChildNodes)
                    {
                        else_if_lista_instrucciones = new LinkedList<Instruccion>();
                        main_declarations(else_if_lista_instrucciones, else_if.ChildNodes.ElementAt(4));

                        ElseIf else_if_object = new ElseIf(Expresion(else_if.ChildNodes.ElementAt(2)), else_if_lista_instrucciones);

                        else_if_lista.AddLast(else_if_object);
                    }
                }


                LinkedList<Instruccion> else_lista_instrucciones = new LinkedList<Instruccion>();
                main_declarations(else_lista_instrucciones, if_statements.ChildNodes.ElementAt(6));

                return new IfInstruccion(Expresion(if_statements.ChildNodes.ElementAt(1)), if_lista_instrucciones,
                    else_if_lista,
                    else_lista_instrucciones);

            }
            else
            {
                return null;
            }
        }
        private Instruccion procedures_declarations(ParseTreeNode procedures_declarations)
        {
            string nombre_procedimiento = procedures_declarations.ChildNodes.ElementAt(1).Token.Text;
            
            LinkedList<ParametroInst> lista_parametros = parameters(procedures_declarations.ChildNodes.ElementAt(3));

            LinkedList<Instruccion> procedure_header_instrucciones = new LinkedList<Instruccion>();
            header_statements(procedure_header_instrucciones, procedures_declarations.ChildNodes.ElementAt(6));

            LinkedList<Instruccion> procedure_body_instrucciones = new LinkedList<Instruccion>();
            body_statements(procedure_body_instrucciones, procedures_declarations.ChildNodes.ElementAt(7));

            return new FunctionInstruccion(nombre_procedimiento, lista_parametros, "",
                procedure_header_instrucciones, procedure_body_instrucciones, false);
        }
        private Instruccion functions_declarations(ParseTreeNode functions_declarations)
        {
            string nombre_funcion = functions_declarations.ChildNodes.ElementAt(1).Token.Text;

            LinkedList<ParametroInst> lista_parametros = parameters(functions_declarations.ChildNodes.ElementAt(3));

            string tipo_funcion = variables_native_id(functions_declarations.ChildNodes.ElementAt(6));

            LinkedList<Instruccion> function_header_instrucciones = new LinkedList<Instruccion>();
            header_statements(function_header_instrucciones, functions_declarations.ChildNodes.ElementAt(8));

            LinkedList<Instruccion> function_body_instrucciones = new LinkedList<Instruccion>();
            body_statements(function_body_instrucciones, functions_declarations.ChildNodes.ElementAt(9));

            return new FunctionInstruccion(nombre_funcion, lista_parametros, tipo_funcion,
                function_header_instrucciones, function_body_instrucciones, true);

        }
        private Instruccion variables_declarations(ParseTreeNode variables_declarations)
        {
            ParseTreeNode variables = variables_declarations.ChildNodes.ElementAt(1);

            if (variables.ChildNodes.Count > 0)
            {
                LinkedList<Var> Var_lista = new LinkedList<Var>();

                foreach (ParseTreeNode variable in variables.ChildNodes)
                {
                    LinkedList<string> lista_ids = list_id(variable.ChildNodes.ElementAt(0));

                    string tipo = variables_native_id(variable.ChildNodes.ElementAt(2));

                    ParseTreeNode variable_initialization = variable.ChildNodes.ElementAt(3);
                    Expresion valor = null;
                    if (variable_initialization.ChildNodes.Count > 0) //Empty
                    {
                        valor = Expresion(variable_initialization.ChildNodes.ElementAt(1));
                    }

                    Var_lista.AddLast(new Var(lista_ids, tipo, valor));
                }

                return new ListaVarInstruccion(Var_lista);

            }
            return null; //TODO: esto?
        }
        private Instruccion type_declarations(ParseTreeNode type_declarations)
        {
            LinkedList<TypeInstruccion> type_lista = new LinkedList<TypeInstruccion>();

            ParseTreeNode type_variables = type_declarations.ChildNodes.ElementAt(1);
            foreach (ParseTreeNode type_variable in type_variables.ChildNodes)
            {
                LinkedList<string> lista_ids = list_id(type_variable.ChildNodes.ElementAt(0));

               
                ParseTreeNode type_type = type_variable.ChildNodes.ElementAt(2);
                if(type_type.ChildNodes.Count == 3)//object
                {
                    LinkedList<Instruccion> lista_lista_var_ejec = new LinkedList<Instruccion>();

                    ParseTreeNode type_declarations_vars = type_type.ChildNodes.ElementAt(1);
                    foreach(ParseTreeNode variables_declarations_nodo in type_declarations_vars.ChildNodes)
                    {
                       lista_lista_var_ejec.AddLast(variables_declarations(variables_declarations_nodo)); //new ListaVarInstruccion
                    }

                    type_lista.AddLast(new TypeInstruccion(lista_ids.First.Value, lista_lista_var_ejec));

                }
                else//primitivo,id[array,object], array
                {
                    ParseTreeNode variables_native_array_id = type_type.ChildNodes.ElementAt(0);

                    string primitivo_id;

                    switch (variables_native_array_id.ChildNodes.ElementAt(0).Term.Name)
                    {
                        case "variables_native"://primitivo
                            ParseTreeNode variables_native = variables_native_array_id.ChildNodes.ElementAt(0);
                            primitivo_id = variables_native.ChildNodes.ElementAt(0).Term.Name;
                            
                            type_lista.AddLast(new TypeInstruccion(lista_ids, primitivo_id));
                            
                            break;
                        case "ID"://id[array,object]
                            primitivo_id = variables_native_array_id.ChildNodes.ElementAt(0).Token.Text;

                            type_lista.AddLast(new TypeInstruccion(lista_ids, primitivo_id));

                            break;
                        case "variables_array"://array
                            ParseTreeNode variables_array = variables_native_array_id.ChildNodes.ElementAt(0);
                            
                            LinkedList<Expresion> lista_expresiones = new LinkedList<Expresion>();
                            ParseTreeNode expression_list_plus = variables_array.ChildNodes.ElementAt(2);
                            if (expression_list_plus.ChildNodes.Count > 0)
                            {
                                foreach (ParseTreeNode expression in expression_list_plus.ChildNodes)
                                {
                                    lista_expresiones.AddLast(Expresion(expression));
                                }
                            }
                            string nativo_id = variables_native_id(variables_array.ChildNodes.ElementAt(5));

                            type_lista.AddLast(new TypeInstruccion(lista_ids, lista_expresiones, nativo_id));
                            
                            break;
                        default:
                            return null;
                    }
                    
                }
            }
            return new ListaTypeInstruccion(type_lista);
        }

        private Instruccion constant_variables(ParseTreeNode constant_variables)
        {
            LinkedList<Const> const_lista = new LinkedList<Const>();

            if (constant_variables.ChildNodes.Count > 0)
            {
                foreach (ParseTreeNode constant_variable in constant_variables.ChildNodes)
                {
                    const_lista.AddLast(new Const(constant_variable.ChildNodes.ElementAt(0).Token.Text, Expresion(constant_variable.ChildNodes.ElementAt(2))));
                }
            }
            return new ListaConstInstruccion(const_lista);
        }

        private Expresion Expresion(ParseTreeNode actual)
        {
            ParseTreeNode expression = actual;

            if (expression.ChildNodes.Count == 3)
            {
                switch (expression.ChildNodes.ElementAt(1).Term.Name)
                {
                    case "PLUS":
                        return new Aritmetica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "+");
                    case "MINUS":
                        return new Aritmetica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "-");
                    case "ASTERISK":
                        return new Aritmetica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "*");
                    case "SLASH":
                        return new Aritmetica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "/");
                    case "MODULUS":
                        return new Aritmetica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "%");


                    case "EQUAL":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "=");
                    case "LESS_THAN_GREATER_THAN":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "<>");
                    case "GREATER_THAN":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), ">");
                    case "LESS_THAN":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "<");
                    case "GREATER_THAN_EQUAL":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), ">=");
                    case "LESS_THAN_EQUAL":
                        return new Relacional(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "<=");


                    case "AND":
                        return new Logica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "AND");
                    case "OR":
                        return new Logica(Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]), "OR");


                    case "PERIOD":
                        return new Literal("PERIOD", Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]));
                    case "PERIOD_PERIOD":
                        return new Literal("PERIOD_PERIOD", Expresion(expression.ChildNodes[0]), Expresion(expression.ChildNodes[2]));
                    case "expression":
                        return Expresion(expression.ChildNodes[1]);
                    default:
                        return null;


                }
            }
            else if (expression.ChildNodes.Count == 2)
            {
                switch (expression.ChildNodes.ElementAt(0).Term.Name)
                {
                    case "MINUS":
                        return new Aritmetica(null, Expresion(expression.ChildNodes[1]), "-");
                    case "PLUS":
                        return new Aritmetica(null, Expresion(expression.ChildNodes[1]), "+");

                    case "NOT":
                        return new Logica(null, Expresion(expression.ChildNodes[1]), "NOT");
                    default:
                        return null;
                }
            }
            else if (expression.ChildNodes.Count == 1)
            {
                switch (expression.ChildNodes.ElementAt(0).Term.Name)
                {
                    case "ID":
                        return new Literal("ID", expression.ChildNodes.ElementAt(0).Token.Text);
                    case "CADENA":
                        return new Literal("CADENA", expression.ChildNodes.ElementAt(0).Token.Text);
                    case "NUMBER":
                        return new Literal("NUMBER", expression.ChildNodes.ElementAt(0).Token.Text);
                    case "TRUE":
                        return new Literal("TRUE", expression.ChildNodes.ElementAt(0).Token.Text);
                    case "FALSE":
                        return new Literal("FALSE", expression.ChildNodes.ElementAt(0).Token.Text);
                    default:
                        return null;
                }
            }
            else if(expression.ChildNodes.Count == 4)
            {
                LinkedList<Expresion> lista_expresiones = new LinkedList<Expresion>();

                ParseTreeNode expression_list_plus = expression.ChildNodes.ElementAt(2);
                foreach (ParseTreeNode Nodoexpression in expression_list_plus.ChildNodes)
                {
                    lista_expresiones.AddLast(Expresion(Nodoexpression));
                }

                if (expression.ChildNodes.ElementAt(1).Term.Name == "LEFT_BRACKET")
                    return new ArrayAcceso(Expresion(expression.ChildNodes[0]), lista_expresiones);
                else if (expression.ChildNodes.ElementAt(1).Term.Name == "LEFT_PARENTHESIS")
                    return new FuncionLlamada(expression.ChildNodes.ElementAt(0).Token.Text, lista_expresiones);
                else
                    return null;
            }
            else
            {
                return null;
            }

        }

        private string variables_native_id(ParseTreeNode variables_native_id) //actual = variables_native_id
        {
            //un mejor control de los tipos, no importa si es nativo o predefinido
            if (variables_native_id.ChildNodes.ElementAt(0).Term.Name != "ID")//tipos nativos
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
                foreach (ParseTreeNode id in list_id.ChildNodes)//cada id
                {
                    lista_ids.AddLast(id.Token.Text);//agrego cada id
                }
            }

            return lista_ids;
        }
        private LinkedList<ParametroInst> parameters(ParseTreeNode parameters)
        {
            LinkedList<ParametroInst> lista_parametros = new LinkedList<ParametroInst>();

            foreach (ParseTreeNode parameter in parameters.ChildNodes)
            {
                LinkedList<string> ids = null;
                string tipo_nativo_id = string.Empty;

                if (parameter.ChildNodes.ElementAt(0).Term.Name == "VAR")//referencia
                {
                    ids = list_id(parameter.ChildNodes.ElementAt(1));
                    tipo_nativo_id = variables_native_id(parameter.ChildNodes.ElementAt(3));
                    lista_parametros.AddLast(new ParametroInst(ids, tipo_nativo_id, true));
                }
                else//valor
                {
                    ids = list_id(parameter.ChildNodes.ElementAt(0));
                    tipo_nativo_id = variables_native_id(parameter.ChildNodes.ElementAt(2));
                    lista_parametros.AddLast(new ParametroInst(ids, tipo_nativo_id, false));
                }
            }

            return lista_parametros;
        }

        private void expression_list(LinkedList<Expresion> lista_expresiones, ParseTreeNode expression_list)
        {
            if (expression_list.ChildNodes.Count > 0)
            {
                //ParseTreeNode expression_starRule = expression_list.ChildNodes.ElementAt(0);
                foreach (ParseTreeNode expression in expression_list.ChildNodes)
                {
                    lista_expresiones.AddLast(Expresion(expression));
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
        private void OutputMessage(string msn) => txtOutput += msn + "\r\n";
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

/* 1) cuantas producciones tiene
 * 2) condiciones para saber que produccion esta reconociendo [puede basarse en la cantidad de hijos de la produccion] 
 * 3) tambien la cantidad de ChildNodes o la posicion del elemento que nos interese puede variar DEPENDIENDO de las
 * preferencias que le colocamos al AST en nuestra gramatica, por ejemplo el metodo MarkPunctuation() nos elimina nodos
 * que no nos interesa y por lo tanto nos modifica la cantidad de los ChildNodes de nuestro arbol.
 */

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

/*
ChildNodes... tipo Array y contiene todas las cualidades de una lista
si esta lista = vacia = nodo es una hoja
 != entonces tiene un subarbol
se le manda el nodo RAIZ del arbol 
recorrido al AST ... se le manda el nodo raiz del arbol
 */