﻿using Irony.Parsing;

namespace CompiPascal.interprete.analizador
{
    class GramaticaInterprete : Grammar
    {
        public GramaticaInterprete() : base(caseSensitive: false)
        {

            #region EXPRESIONES_REGULARES Y LITERALES
            /* Expresiones regulares de los tokens que nuestra gramatica reconocera*/
            //RegexBasedTerminal Numero = new RegexBasedTerminal("Numero", "[0-9]+");
            //StringLiteral cadena1 = TerminalFactory.CreateCSharpString("Cadena");
            //StringLiteral char1 = TerminalFactory.CreateCSharpChar("CharLiteral"); 
            //NumberLiteral numero2 = TerminalFactory.CreateCSharpNumber("Number");
            //IdentifierTerminal id2 = TerminalFactory.CreateCSharpIdentifier("Identifier");
            NumberLiteral NUMBER = new NumberLiteral("NUMBER");//entero, decimal, negativo
            IdentifierTerminal ID = new IdentifierTerminal("ID"); //TODO: tomar en cuenta para las variables que no es caseSensitive 
            StringLiteral CADENA = new StringLiteral("CADENA", "\'"); //cadena, char

            //var INTEGER = ToTerm("INTEGER");  //integer value
            //var REAL = ToTerm("REAL");  //real number value
            //var CHAR = ToTerm("CHAR");  //single character
            //var STRING = ToTerm("CHAR");  //array of character

            CommentTerminal LINE_COMMENT = new CommentTerminal("LINE_COMMENT", "//", "\n", "\r\n", "\r", "\u2085", "\u2028", "\u2029");
            CommentTerminal BLOCK_COMMENT_ONE = new CommentTerminal("BLOCK_COMMENT_ONE", "(*", "*)");
            CommentTerminal BLOCK_COMMENT_TWO = new CommentTerminal("BLOCK_COMMENT_TWO", "{", "}");
            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(BLOCK_COMMENT_ONE);
            NonGrammarTerminals.Add(BLOCK_COMMENT_TWO);

            #endregion

            #region Simbolos
            /* Conjunto de terminales que serán utilizados en nuestra gramática, que no fueron aceptados por ninguna de las expresiones regulares definidas anteriormente */

            var PLUS = ToTerm("+", "PLUS");  //Binary arithmetic addition ; unary arithmetic identity ; set union.
            var MINUS = ToTerm("-", "MINUS");  //Binary arithmetic subtraction ; unary arithmetic negation ; set difference.
            var ASTERISK = ToTerm("*", "ASTERISK");  //Arithmetic multiplication ; set intersection.
            var SLASH = ToTerm("/", "SLASH");  //Floating point division.
            var EQUAL = ToTerm("=", "EQUAL");  //Equality test.
            var LESS_THAN = ToTerm("<", "LESS_THAN");  //Less than test.
            var GREATER_THAN = ToTerm(">", "GREATER_THAN");  //Greater than test.
            var LEFT_BRACKET = ToTerm("[", "LEFT_BRACKET");  //Delimits sets and array indices.
            var RIGHT_BRACKET = ToTerm("]", "RIGHT_BRACKET");  //Delimits sets and array indices.
            var PERIOD = ToTerm(".", "PERIOD");  //Used for selecting an individual field of a record variable. Follows the final END of a program.
            var COMMA = ToTerm(",", "COMMA");  //Separates arguments, variable declarations, and indices of multi-dimensional arrays.
            var COLON = ToTerm(":", "COLON");  //Separates a function declaration with the function type. Separates variable declaration with the variable type.
            var SEMI_COLON = ToTerm(";", "SEMI_COLON");  //Separates Pascal statements.
            var POINTER = ToTerm("^", "POINTER");  //Used to declare pointer types and variables ; Used to access the contents of pointer typed variables/file buffer variables.
            var LEFT_PARENTHESIS = ToTerm("(", "LEFT_PARENTHESIS");  //Group mathematical or boolean expression, or function and procedure arguments.
            var RIGHT_PARENTHESIS = ToTerm(")", "RIGHT_PARENTHESIS");  //Group mathematical or boolean expression, or function and procedure arguments.
            var LESS_THAN_GREATER_THAN = ToTerm("<>", "LESS_THAN_GREATER_THAN");  //Non-equality test.
            var LESS_THAN_EQUAL = ToTerm("<=", "LESS_THAN_EQUAL");  //Less than or equal to test ; Subset of test.
            var GREATER_THAN_EQUAL = ToTerm(">=", "GREATER_THAN_EQUAL");  //Greater than or equal to test ; Superset of test.
            var COLON_EQUAL = ToTerm(":=", "COLON_EQUAL");  //Variable assignment.
            var PERIOD_PERIOD = ToTerm("..", "PERIOD_PERIOD");  //Range delimiter.
            var MODULUS = ToTerm("%", "MODULUS");  //modulus operator

            #endregion

            #region PALABRAS RESERVADAS
            var EXIT = ToTerm("EXIT");  //Boolean conjunction operator
            var NEW = ToTerm("NEW");  //Boolean conjunction operator
            var AND = ToTerm("AND");  //Boolean conjunction operator
            var ARRAY = ToTerm("ARRAY");  //Array type
            var BEGIN = ToTerm("BEGIN");  //Starts a compound statement
            var CASE = ToTerm("CASE");  //Starts a CASE statement
            var CONST = ToTerm("CONST");  //Declares a constant
            var DIV = ToTerm("DIV");  //Integer division
            var DO = ToTerm("DO");  //Follows WHILE and FOR clause, preceding action to take
            var DOWNTO = ToTerm("DOWNTO");  //In a FOR loop, indicates that FOR variable is decremented at each pass
            var ELSE = ToTerm("ELSE");  //If the boolean in the IF is false, the action following ELSE is executed
            var END = ToTerm("END");  //Ends a compound statement, a case statement, or a record declaration
            var FILE = ToTerm("FILE");  //Declares a variable as a file
            var FOR = ToTerm("FOR");  //Executes line(s"); of code while FOR loop variable in within range
            var FUNCTION = ToTerm("FUNCTION");  //Declares a Pascal function
            var GOTO = ToTerm("GOTO");  //Branches to a specified label
            var IF = ToTerm("IF");  //Examine a boolean condition and execute code if true
            var IN = ToTerm("IN");  //Boolean evaluated to true if value is in a specified set
            var LABEL = ToTerm("LABEL");  //Indicates code to branch to in a GOTO statement
            var MOD = ToTerm("MOD");  //Modular integer evaluation
            var NIL = ToTerm("NIL");  //Null value for a pointer
            var NOT = ToTerm("NOT");  //Negates the value of a boolean expression
            var OF = ToTerm("OF");  //Used in CASE statement after case variable
            var OR = ToTerm("OR");  //Boolean disjunction operator
            var PACKED = ToTerm("PACKED");  //Used with ARRAY, FILE, RECORD, and SET to pack data storage
            var PROCEDURE = ToTerm("PROCEDURE");  //Declares a Pascal procedure
            var PROGRAM = ToTerm("PROGRAM");  //Designates the program heading
            var RECORD = ToTerm("RECORD");  //Declares a record type
            var REPEAT = ToTerm("REPEAT");  //Starts a REPEAT/UNTIL loop
            var SET = ToTerm("SET");  //Declares a set
            var THEN = ToTerm("THEN");  //Follows the boolean expression after an IF statement
            var TO = ToTerm("TO");  //In a FOR loop, indicates that FOR variable is incremented at each pass
            var TYPE = ToTerm("TYPE");  //Defines a variable type
            var UNTIL = ToTerm("UNTIL");  //Ends a REPEAT/UNTIL loop
            var VAR = ToTerm("VAR");  //Declares a program variable
            var WHILE = ToTerm("WHILE");  //Executes block of code until WHILE condition is false
            var WITH = ToTerm("WITH");  //Specifies record variable to use for a block of code
            var OTHERWISE = ToTerm("OTHERWISE");  //a optional reserved word for case's
            var BREAK = ToTerm("BREAK");  //break statement
            var CONTINUE = ToTerm("CONTINUE");  //continue statement
            var WRITE = ToTerm("WRITE");  //continue statement
            var WRITELN = ToTerm("WRITELN");  //continue statement
            var GRAFICAR_TS = ToTerm("GRAFICAR_TS");  //continue statement


            var TRUE = ToTerm("TRUE");  //Boolean conjunction operator
            var FALSE = ToTerm("FALSE");  //Boolean conjunction operator

            var INTEGER = ToTerm("INTEGER");  //type integer
            var REAL = ToTerm("REAL");  //type real
            var CHAR = ToTerm("CHAR");  //type char
            var STRING = ToTerm("STRING");  //type string
            var BOOLEAN = ToTerm("BOOLEAN");  //type boolean
            var OBJECT = ToTerm("OBJECT");  //type boolean

            //TODO: las funciones nativas
            #endregion

            #region No Terminales  /* Conjunto de no terminales que serán utilizados en nuestra gramática. */

            NonTerminal ini = new NonTerminal("ini");
            NonTerminal program_structure = new NonTerminal("program_structure");
            NonTerminal program = new NonTerminal("program");
            NonTerminal header_statements = new NonTerminal("header_statements");
            NonTerminal body_statements = new NonTerminal("body_statements");
            NonTerminal statements = new NonTerminal("statements");
            NonTerminal statement = new NonTerminal("statement");


            NonTerminal list_id = new NonTerminal("list_id");

            NonTerminal type_declarations = new NonTerminal("type_declarations");
            NonTerminal type_variables = new NonTerminal("type_variables");
            NonTerminal type_variable = new NonTerminal("type_variable");
            NonTerminal type_declarations_vars = new NonTerminal("type_declarations_vars");
            NonTerminal type_type = new NonTerminal("type_type");


            NonTerminal constant_declarations = new NonTerminal("constant_declarations");
            NonTerminal constant_optional = new NonTerminal("constant_optional");
            NonTerminal constant_variables = new NonTerminal("constant_variables");
            NonTerminal constant_variable = new NonTerminal("constant_variable");

            NonTerminal variables_declarations = new NonTerminal("variables_declarations");
            NonTerminal variables = new NonTerminal("variables");
            NonTerminal variable = new NonTerminal("variable");
            NonTerminal variable_initialization = new NonTerminal("variable_initialization");
            NonTerminal variable_ini = new NonTerminal("variable_ini");

            NonTerminal functions_declarations = new NonTerminal("functions_declarations");
            NonTerminal procedures_declarations = new NonTerminal("procedures_declarations");
            NonTerminal callFuncProc = new NonTerminal("callFuncProc");
            NonTerminal parameters = new NonTerminal("parameters");
            NonTerminal parameter = new NonTerminal("parameter");

            NonTerminal main_declarations = new NonTerminal("main_declarations");
            NonTerminal begin_end_statements = new NonTerminal("begin_end_statements");
            NonTerminal begin_end_statement = new NonTerminal("begin_end_statement");


            NonTerminal exit_statements = new NonTerminal("exit_statements");
            NonTerminal return_statements = new NonTerminal("return_statements");
            NonTerminal write_statements = new NonTerminal("write_statements");
            NonTerminal graficar_statements = new NonTerminal("graficar_statements");


            NonTerminal if_statements = new NonTerminal("if_statements");
            NonTerminal else_if_list = new NonTerminal("else_if_list");
            NonTerminal else_if = new NonTerminal("else_if");


            NonTerminal case_statements = new NonTerminal("case_statements");
            NonTerminal case_list = new NonTerminal("case_list");
            NonTerminal cases = new NonTerminal("cases");
            NonTerminal cases_words = new NonTerminal("cases_words");


            NonTerminal while_statements = new NonTerminal("while_statements");
            NonTerminal for_do_statements = new NonTerminal("for_do_statements");
            NonTerminal repeat_statements = new NonTerminal("repeat_statements");


            NonTerminal array_ini = new NonTerminal("array_ini");


            NonTerminal expression_list = new NonTerminal("expression_list");
            NonTerminal expression_list_plus = new NonTerminal("expression_list_plus");

            NonTerminal expression = new NonTerminal("expression");

            NonTerminal values_native = new NonTerminal("values_native");
            NonTerminal values_native_id = new NonTerminal("values_native_id");

            NonTerminal variables_native = new NonTerminal("variables_native");
            NonTerminal variables_array = new NonTerminal("variables_array");
            NonTerminal variables_native_array = new NonTerminal("variables_native_array");
            NonTerminal variables_native_array_id = new NonTerminal("variables_native_array_id");
            NonTerminal variables_native_id = new NonTerminal("variables_native_id");


            NonTerminal union_1 = new NonTerminal("union_1");
            NonTerminal union_1_a = new NonTerminal("union_1_a");


            NonTerminal for_opciones = new NonTerminal("for_opciones");


            #endregion

            #region GRAMATICA /* Región donde se define la gramática. */

            ini.Rule = program_structure;

            program_structure.Rule = PROGRAM + ID + SEMI_COLON + program;
            program_structure.ErrorRule = SyntaxError + SEMI_COLON;

            program.Rule = header_statements + body_statements + PERIOD;
            header_statements.Rule = constant_optional + statements; //se aplican unicamente al cuerpo de su propia funcion
            body_statements.Rule = main_declarations;


            constant_optional.Rule = constant_declarations
                | Empty;
            constant_optional.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;

            constant_declarations.Rule = CONST + constant_variables;
            constant_variables.Rule = MakePlusRule(constant_variables, constant_variable);
            constant_variable.Rule = ID + EQUAL + expression + SEMI_COLON;


            statements.Rule = MakeStarRule(statements, statement);
            statement.Rule = type_declarations
                | variables_declarations  //Un programa puede tener el mismo nombre para variables locales y globales, pero el valor de la variable local dentro de una función tendrá preferencia. //si pues... practicamente se refiere a que si existe una variable local con el mismo nombre que una global se ignora la global y se trabaja siempre sobre la local, sin dar error de que ya existe la variables... simplemente se ignora
                | functions_declarations
                | procedures_declarations;
            statement.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;


            type_declarations.Rule = TYPE + type_variables;
            variables_declarations.Rule = VAR + variables;
            //TODO: return_value, por el momento solo tengo los tipos nativos, verificar si tambien se admiten un id (tipo propio), array? 
            //adicional, verificar que tenga el retorno, puede: venir dentro de otra instruccion (if) o puede venir afuera de todo y ser valido media vez cumpla con que tenga un retorno o retornos necesarios
            // como validar eso? //Debe haber una declaración de asignación del tipo - nombre: = expresión  en el cuerpo de la función que asigna un valor al nombre de la función
            functions_declarations.Rule = FUNCTION + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + COLON + variables_native_id + SEMI_COLON + header_statements + body_statements + SEMI_COLON;
            procedures_declarations.Rule = PROCEDURE + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + SEMI_COLON + header_statements + body_statements + SEMI_COLON;

            list_id.Rule = MakePlusRule(list_id, COMMA, ID);

            type_variables.Rule = MakePlusRule(type_variables, type_variable);
            type_variable.Rule = list_id + EQUAL + type_type + SEMI_COLON;

            type_type.Rule = OBJECT + type_declarations_vars + END
                | variables_native_array_id;

            type_declarations_vars.Rule = MakeStarRule(type_declarations_vars, variables_declarations);

            variables.Rule = MakePlusRule(variables, variable);
            variable.Rule = list_id + COLON + variables_native_id + variable_initialization + SEMI_COLON;

            variable_initialization.Rule = COLON_EQUAL + expression | Empty; //TODO: cuidar si estoy inicializando una variable de tipo de otra variable y quiero asignar un valor que no es, primero se puede inicializar en ese caso? segundo si es posible entonces, tendria que ver primero que tipo es la variable para permitir la inicializacion

            parameters.Rule = MakeStarRule(parameters, SEMI_COLON, parameter); // Estos parámetros pueden tener un tipo de datos estándar, un tipo de datos definido por el usuario o un tipo de datos de subrango.
            parameter.Rule = list_id + COLON + variables_native_id //pueden ser id's de variables simples o matrices o subprogramas
                | VAR + list_id + COLON + variables_native_id;


            main_declarations.Rule = BEGIN + begin_end_statements + END;
            main_declarations.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;

            begin_end_statements.Rule = MakeStarRule(begin_end_statements, begin_end_statement);
            begin_end_statement.Rule = variable_ini
                | array_ini 
                | if_statements
                | case_statements
                | while_statements
                | for_do_statements
                | repeat_statements
                | BREAK + SEMI_COLON //validar que este dentro de un bucle o un case
                | CONTINUE + SEMI_COLON //validar que este dentro de un bucle //Para el ciclo for-do , la instrucción continue hace que se ejecuten las porciones de prueba e incremento condicional del ciclo. Para los bucles while-do y repeat ... until , la instrucción continue hace que el control del programa pase a las pruebas condicionales.          
                | exit_statements
                //| callFuncProc
                //| return_statements 
                | write_statements
                | graficar_statements
                | union_1
                | Empty;
            begin_end_statement.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;

            variable_ini.Rule = expression + COLON_EQUAL + expression + SEMI_COLON; //expression + PERIOD + expression

            array_ini.Rule = expression + LEFT_BRACKET + expression_list_plus + RIGHT_BRACKET + COLON_EQUAL + expression + SEMI_COLON;

            union_1.Rule = ID + LEFT_PARENTHESIS + expression_list + RIGHT_PARENTHESIS + union_1_a;

            union_1_a.Rule = SEMI_COLON //funcion
                | COLON_EQUAL + expression + SEMI_COLON;//return

            //callFuncProc.Rule = ID + LEFT_PARENTHESIS + expression_list + RIGHT_PARENTHESIS + ;
            expression_list.Rule = MakeStarRule(expression_list, COMMA, expression);
                        
            //return_statements.Rule = ID + LEFT_PARENTHESIS + RIGHT_PARENTHESIS + ;

            exit_statements.Rule = EXIT + LEFT_PARENTHESIS + expression + RIGHT_PARENTHESIS + SEMI_COLON;

            #region bien
            if_statements.Rule = IF + expression + THEN + main_declarations
                | IF + expression + THEN + main_declarations + ELSE + main_declarations
                | IF + expression + THEN + main_declarations + else_if_list + ELSE + main_declarations;

            else_if_list.Rule = MakePlusRule(else_if_list, else_if);
            else_if.Rule = ELSE + IF + expression + THEN + main_declarations;

            case_statements.Rule = CASE + expression + OF + case_list + cases_words + END + SEMI_COLON;

            case_list.Rule = MakePlusRule(case_list, cases);
            cases.Rule = expression + COLON + main_declarations + SEMI_COLON;

            cases_words.Rule = ELSE + main_declarations + SEMI_COLON
                | OTHERWISE + main_declarations + SEMI_COLON
                | Empty;

            while_statements.Rule = WHILE + expression + DO + main_declarations + SEMI_COLON;

            for_do_statements.Rule = FOR + expression + COLON_EQUAL + expression + for_opciones + expression + DO + main_declarations + SEMI_COLON;

            for_opciones.Rule = TO
                | DOWNTO;

            repeat_statements.Rule = REPEAT + main_declarations + UNTIL + expression + SEMI_COLON;

            #endregion finBien

            #region 2bien
            write_statements.Rule = WRITE + LEFT_PARENTHESIS + expression_list_plus + RIGHT_PARENTHESIS + SEMI_COLON
                | WRITELN + LEFT_PARENTHESIS + expression_list_plus + RIGHT_PARENTHESIS + SEMI_COLON;

            graficar_statements.Rule = GRAFICAR_TS + LEFT_PARENTHESIS + RIGHT_PARENTHESIS + SEMI_COLON;
            #endregion fin2Bien

            expression.Rule =  expression + PLUS + expression //a
                | expression + MINUS + expression//a
                | expression + ASTERISK + expression//a
                | expression + SLASH + expression//a
                | expression + MODULUS + expression//a

                | expression + EQUAL + expression //r
                | expression + LESS_THAN_GREATER_THAN + expression //r
                | expression + GREATER_THAN + expression //r
                | expression + LESS_THAN + expression //r
                | expression + GREATER_THAN_EQUAL + expression //r
                | expression + LESS_THAN_EQUAL + expression //r

                | LEFT_PARENTHESIS + expression + RIGHT_PARENTHESIS

                | expression + AND + expression //lo
                | expression + OR + expression //lo

                | NOT + expression //lo

                | MINUS + expression //a
                | PLUS + expression //a

                | ID | CADENA | NUMBER | TRUE | FALSE 
                
                | ID + LEFT_PARENTHESIS + expression_list + RIGHT_PARENTHESIS

                | expression + LEFT_BRACKET + expression_list_plus + RIGHT_BRACKET 

                | expression + PERIOD + expression 

                | expression + PERIOD_PERIOD + expression; 

            
            values_native.Rule = CADENA | NUMBER | TRUE | FALSE; 
            values_native_id.Rule = values_native | ID;


            variables_native.Rule = STRING | INTEGER | REAL | BOOLEAN;
            variables_array.Rule = ARRAY + LEFT_BRACKET + expression_list_plus + RIGHT_BRACKET + OF + variables_native_id; 
            expression_list_plus.Rule = MakePlusRule(expression_list_plus, COMMA, expression);

            //object

            variables_native_array.Rule = variables_native | variables_array;
            variables_native_array_id.Rule = variables_native | variables_array | ID;
            variables_native_id.Rule = variables_native | ID; //primitivo, array, object
            //TODO: verificar que en las fun/proc se puede
            //enviar parametros con un tipo predefinido por el usuario
            #endregion

            #region PREFERENCIAS
            this.Root = ini;
            //precedencia y asociatividad
            RegisterOperators(4, NOT); //~
            RegisterOperators(3, ASTERISK, SLASH, DIV, MOD, AND); //&
            RegisterOperators(2, PLUS, MINUS, OR); // |, !
            RegisterOperators(1, EQUAL, LESS_THAN_GREATER_THAN, LESS_THAN, LESS_THAN_EQUAL, GREATER_THAN, GREATER_THAN_EQUAL, IN); //or else, and then
            //MarkPunctuation("(", ")", "[", "]");
            //RegisterOperators(1, Associativity.Left,  MAS, MENOS);
            //MarkTransient(aritmeticas, relacionales, logicas);
            #endregion

        }
    }
}

/*
 * VARIABLE, ARRAY, OBJECTO: [SIMBOLO]
 * Declarar
 * Inicializar
 * Declarar-Inicializar
 * Asignar
 * Usar
 * 
 * 
 * 
 *          ini.Rule = instrucciones;
            instrucciones.Rule = MakePlusRule(instrucciones, expresion); //instrucciones.Rule = MakePlusRule(instruccion, ToTerm(","), instrucciones);
            instruccion.Rule = array_stmt;
            array_stmt.Rule = 
 * 
 * 
 * 
 * 
 *  Automatically add NewLine before EOF so that our BNF rules work correctly when there's no final line break in source
 *  this.LanguageFlags = LanguageFlags.NewLineBeforeEOF | LanguageFlags.CreateAst | LanguageFlags.SupportsBigInt;
 * 
 * BASIC CONCEPTS (LISTAS)
 * TYPES
 * EXPRESSION
 * STATEMENTS
 * CLASES
 * STRUCT
 * ARRAYS
 * INTERFACES
 * ENUMS
 * ATRIBUTES
 * 
 *       this.AddTermsReportGroup("assignment", "=", "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "<<=", ">>=");
      this.AddTermsReportGroup("typename", "bool", "decimal", "float", "double", "string", "object", 
        "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong", "char");
      this.AddTermsReportGroup("statement", "if", "switch", "do", "while", "for", "foreach", "continue", "goto", "return", "try", "yield", 
                                            "break", "throw", "unchecked", "using");
      this.AddTermsReportGroup("type declaration", "public", "private", "protected", "static", "internal", "sealed", "abstract", "partial", 
                                                   "class", "struct", "delegate", "interface", "enum");
      this.AddTermsReportGroup("member declaration", "virtual", "override", "readonly", "volatile", "extern");
      this.AddTermsReportGroup("constant", Number, StringLiteral, CharLiteral);
      this.AddTermsReportGroup("constant", "true", "false", "null");

      this.AddTermsReportGroup("unary operator", "+", "-", "!", "~");
      
      this.AddToNoReportGroup(comma, semi);
      this.AddToNoReportGroup("var", "const", "new", "++", "--", "this", "base", "checked", "lock", "typeof", "default",
                               "{", "}", "[");
 * 
 * 
 * node.Term.Name
 * node.ChildNodes[1].FindTokenAndGetText().ToLower()
 * node.Token.ValueString
 * ----
 * 
 *          string operador = actual.Term.Name;//program_structured
            string operador2 = actual.Token.ValueString;//null
            string operador1 = actual.ChildNodes.ElementAt(1).FindTokenAndGetText().ToLower();//evaluar
            string operador3 = actual.ChildNodes.ElementAt(1).Token.Text;//null
            string operador4 = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
 * ----
 * 
 *       // 7. Error recovery rule
      ExtStmt.ErrorRule = SyntaxError + Eos;
      FunctionDef.ErrorRule = SyntaxError + Dedent; 
 * 
 * 
 * 
      //Scheme is tail-recursive language
      base.LanguageFlags |= LanguageFlags.TailRecursive;// | LanguageFlags.CreateAst; 
 * 
 * table.Rule = LeftSquareBrace + Name + RightSquareBrace;
      table.ErrorRule = SyntaxError + ";";
      column.Rule = LeftSquareBrace + Name + RightSquareBrace;
      column.ErrorRule = SyntaxError + ";";
      query.Rule = MakePlusRule(query, logicOp, logicExpression);
 * 
 * //Reserved words and special words
      var resWordList = "ACCEPT AND BEGIN BREAK BY CASE CHOOSE COMPILE CYCLE DO ELSE ELSIF END EXECUTE EXIT FUNCTION GOTO IF INCLUDE LOOP" +
                        " MEMBER NEW NOT NULL OF OMIT OR OROF PARENT PROCEDURE PROGRAM RETURN ROUTINE SECTION SELF THEN TIMES TO UNTIL WHILE XOR";
      this.MarkReservedWords(resWordList.Split(' '));
      var specialWordsList = "APPLICATION CLASS CODE DATA DETAIL FILE FOOTER FORM GROUP HEADER ITEM ITEMIZE JOIN MAP MENU MENUBAR" + 
                         " MODULE OLECONTROL OPTION QUEUE RECORD REPORT ROW SHEET TAB TABLE TOOLBAR VIEW WINDOW";
      //Initialize special words list (words that cannot be used as proc names); we'll later use them for verifying proc names
      SpecialWords = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
      SpecialWords.UnionWith(specialWordsList.Split(' ')); 
 * 
 * 
 * 
 */