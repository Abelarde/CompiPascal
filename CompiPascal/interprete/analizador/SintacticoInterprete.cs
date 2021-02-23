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

            ParseTreeNode body_statements = program.ChildNodes.ElementAt(1);
            ParseTreeNode main_declarations = body_statements.ChildNodes.ElementAt(0);
            ParseTreeNode begin_end_statements = main_declarations.ChildNodes.ElementAt(1);
            if(begin_end_statements.ChildNodes.Count > 0)//begin_end_statement+
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
                    return new Const();
                case "TYPE":
                    return new TypeInstruccion();
                case "VAR":
                    return new Var();
                case "FUNCTION":
                    return new FunctionInstruccion();
                case "PROCEDURE":
                    return new ProcedureInstruccion();
                case "IF":
                    return new IfInstruccion();
                case "CASE":
                    return new CaseInstruccion();
                case "WHILE":
                    return new WhileInstruccion();
                case "FOR":
                    return new ForInstruccion();
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
                            //if a validar que no sea null cuando lo recorro
                            return null;
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
