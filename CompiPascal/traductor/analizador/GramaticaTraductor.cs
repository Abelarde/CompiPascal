using Irony.Parsing;

namespace CompiPascal.traductor.analizador
{
    class GramaticaTraductor : Grammar
    {
        public GramaticaTraductor() : base(caseSensitive: false)
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
            CommentTerminal BLOCK_COMMENT_ONE = new CommentTerminal("BLOCK_COMMENT_ONE", "{*", "*}");
            CommentTerminal BLOCK_COMMENT_TWO = new CommentTerminal("BLOCK_COMMENT_TWO", "{", "}");
            NonGrammarTerminals.Add(LINE_COMMENT);
            NonGrammarTerminals.Add(BLOCK_COMMENT_ONE);
            NonGrammarTerminals.Add(BLOCK_COMMENT_TWO);

            #endregion

            #region Simbolos
            /* Conjunto de terminales que serán utilizados en nuestra gramática, que no fueron aceptados por ninguna de las expresiones regulares definidas anteriormente */

            var PLUS = ToTerm("+");  //Binary arithmetic addition ; unary arithmetic identity ; set union.
            var MINUS = ToTerm("-");  //Binary arithmetic subtraction ; unary arithmetic negation ; set difference.
            var ASTERISK = ToTerm("*");  //Arithmetic multiplication ; set intersection.
            var SLASH = ToTerm("/");  //Floating point division.
            var EQUAL = ToTerm("=");  //Equality test.
            var LESS_THAN = ToTerm("<");  //Less than test.
            var GREATER_THAN = ToTerm(">");  //Greater than test.
            var LEFT_BRACKET = ToTerm("[");  //Delimits sets and array indices.
            var RIGHT_BRACKET = ToTerm("]");  //Delimits sets and array indices.
            var PERIOD = ToTerm(".");  //Used for selecting an individual field of a record variable. Follows the final END of a program.
            var COMMA = ToTerm(",");  //Separates arguments, variable declarations, and indices of multi-dimensional arrays.
            var COLON = ToTerm(":");  //Separates a function declaration with the function type. Separates variable declaration with the variable type.
            var SEMI_COLON = ToTerm(";");  //Separates Pascal statements.
            var POINTER = ToTerm("^");  //Used to declare pointer types and variables ; Used to access the contents of pointer typed variables/file buffer variables.
            var LEFT_PARENTHESIS = ToTerm("(");  //Group mathematical or boolean expression, or function and procedure arguments.
            var RIGHT_PARENTHESIS = ToTerm(")");  //Group mathematical or boolean expression, or function and procedure arguments.
            var LESS_THAN_GREATER_THAN = ToTerm("<>");  //Non-equality test.
            var LESS_THAN_EQUAL = ToTerm("<=");  //Less than or equal to test ; Subset of test.
            var GREATER_THAN_EQUAL = ToTerm(">=");  //Greater than or equal to test ; Superset of test.
            var COLON_EQUAL = ToTerm(":=");  //Variable assignment.
            var PERIOD_PERIOD = ToTerm("..");  //Range delimiter.
            var MODULUS = ToTerm("%");  //modulus operator

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


            var TRUE = ToTerm("TRUE");  //Boolean conjunction operator
            var FALSE = ToTerm("FALSE");  //Boolean conjunction operator

            var INTEGER = ToTerm("INTEGER");  //type integer
            var REAL = ToTerm("REAL");  //type real
            var CHAR = ToTerm("CHAR");  //type char
            var STRING = ToTerm("STRING");  //type string
            var BOOLEAN = ToTerm("BOOLEAN");  //type boolean

            //TODO: las funciones nativas
            #endregion

            #region No Terminales  /* Conjunto de no terminales que serán utilizados en nuestra gramática. */

            NonTerminal ini = new NonTerminal("ini");
            NonTerminal program_structure = new NonTerminal("program_structure");
            NonTerminal program = new NonTerminal("program");
            NonTerminal header_statements = new NonTerminal("header_statements");
            NonTerminal body_statements = new NonTerminal("header_statements");
            NonTerminal statements = new NonTerminal("statements");
            NonTerminal statement = new NonTerminal("statement");


            NonTerminal list_id = new NonTerminal("list_id");

            NonTerminal type_declarations = new NonTerminal("type_declarations");
            NonTerminal type_variables = new NonTerminal("type_variables");
            NonTerminal type_variable = new NonTerminal("type_variable");

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
            NonTerminal function_call = new NonTerminal("function_call");
            NonTerminal procedures_declarations = new NonTerminal("procedures_declarations");
            NonTerminal procedure_call = new NonTerminal("procedure_call");
            NonTerminal parameters = new NonTerminal("parameters");
            NonTerminal parameter = new NonTerminal("parameter");

            NonTerminal main_declarations = new NonTerminal("main_declarations");
            NonTerminal begin_end_statements = new NonTerminal("begin_end_statements");
            NonTerminal begin_end_statement = new NonTerminal("begin_end_statement");


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


            NonTerminal array_call = new NonTerminal("array_call");
            NonTerminal array_ini = new NonTerminal("array_ini");

            NonTerminal instructions = new NonTerminal("instructions");
            NonTerminal instruction = new NonTerminal("instruction");

            NonTerminal expression_list = new NonTerminal("expression_list");

            NonTerminal expression = new NonTerminal("expression");
            NonTerminal aritmeticas = new NonTerminal("aritmeticas");
            NonTerminal relacionales = new NonTerminal("relacionales");
            NonTerminal logicas = new NonTerminal("logicas"); 
            NonTerminal expression_terminales = new NonTerminal("expression_terminales");

            NonTerminal values_native = new NonTerminal("values_native");
            NonTerminal values_native_id = new NonTerminal("values_native_id");

            NonTerminal variables_native = new NonTerminal("variables_native");
            NonTerminal variables_array = new NonTerminal("variables_array");
            NonTerminal variables_native_array = new NonTerminal("variables_native_array");
            NonTerminal variables_native_id = new NonTerminal("variables_native_id");



            #endregion

            #region GRAMATICA /* Región donde se define la gramática. */

            ini.Rule = program_structure;

            program_structure.Rule = PROGRAM + ID + SEMI_COLON + program;
            program_structure.ErrorRule = SyntaxError + SEMI_COLON;

            program.Rule = header_statements + body_statements + PERIOD;
            header_statements.Rule = constant_optional + statements; //se aplican unicamente al cuerpo de su propia funcion
            body_statements.Rule = main_declarations;

            constant_optional.Rule = constant_declarations //constantes: deben de declararse antes que todas las variables
                | Empty;
            constant_optional.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;
            constant_declarations.Rule = CONST + constant_variables | Empty; // 


            statements.Rule = MakeStarRule(statements, statement);//TODO: verificar que podre hacer en este lugar y que no
            statement.Rule = type_declarations
                | variables_declarations  //TODO: Un programa puede tener el mismo nombre para variables locales y globales, pero el valor de la variable local dentro de una función tendrá preferencia. //si pues... practicamente se refiere a que si existe una variable local con el mismo nombre que una global se ignora la global y se trabaja siempre sobre la local, sin dar error de que ya existe la variables... simplemente se ignora
                | functions_declarations
                | procedures_declarations;
            statement.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;


            type_declarations.Rule = TYPE + type_variables | Empty; //categoria, o clase de los tipos como integer, real, string
            variables_declarations.Rule = VAR + variables | Empty; //declaracion: solo afuera de BEGIN-END; 
            functions_declarations.Rule = FUNCTION + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + COLON + variables_native_id + SEMI_COLON + header_statements + body_statements + SEMI_COLON; //TODO: return_value, por el momento solo tengo los tipos nativos, verificar si tambien se admiten un id (tipo propio), array? //adicional, verificar que tenga el retorno, puede: venir dentro de otra instruccion (if) o puede venir afuera de todo y ser valido media vez cumpla con que tenga un retorno o retornos necesarios// como validar eso? //Debe haber una declaración de asignación del tipo - nombre: = expresión  en el cuerpo de la función que asigna un valor al nombre de la función
            procedures_declarations.Rule = PROCEDURE + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + SEMI_COLON + header_statements + body_statements + SEMI_COLON;
            main_declarations.Rule = BEGIN + begin_end_statements + END; //aqui inicia la ejecucion del programa
            main_declarations.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;

            list_id.Rule = MakePlusRule(list_id, COMMA, ID);

            variables.Rule = MakePlusRule(variables, variable);
            variable.Rule = list_id + COLON + variables_native_id + variable_initialization + SEMI_COLON; //indica el tipo de valores que puede tomar la variable //TODO: array tambien?  var n: array[1..10] of integer; (*n is an array of 10 integers *)
            //TODO: verificar que no puedo inicializar una variable afuera del cuerpo igual que adentro 
            //afuera no puedo utilizar ID y adentro si
            variable_initialization.Rule = COLON_EQUAL + expression | Empty; //TODO: cuidar si estoy inicializando una variable de tipo de otra variable y quiero asignar un valor que no es, primero se puede inicializar en ese caso? segundo si es posible entonces, tendria que ver primero que tipo es la variable para permitir la inicializacion
            

            begin_end_statements.Rule = MakeStarRule(begin_end_statements, begin_end_statement);
            begin_end_statement.Rule = variable_ini
                | array_ini
                | if_statements
                | case_statements
                | while_statements
                | for_do_statements
                | repeat_statements
                | BREAK + SEMI_COLON //TODO: validar que este dentro de un bucle o un case
                | CONTINUE + SEMI_COLON //TODO: validar que este dentro de un bucle //Para el ciclo for-do , la instrucción continue hace que se ejecuten las porciones de prueba e incremento condicional del ciclo. Para los bucles while-do y repeat ... until , la instrucción continue hace que el control del programa pase a las pruebas condicionales.
                | function_call + SEMI_COLON
                | procedure_call + SEMI_COLON
                | array_call + SEMI_COLON
                | Empty;
            begin_end_statement.ErrorRule = SyntaxError + SEMI_COLON
                | SyntaxError + END;

            //TODO: verificacion de asignacion: la expresion variables(dentro/fuera) del cuerpo
            variable_ini.Rule = ID + COLON_EQUAL + expression + SEMI_COLON;
            //| ID + LEFT_PARENTHESIS + RIGHT_PARENTHESIS + COLON_EQUAL + expression + SEMI_COLON; //TODO: (funciones) ver que me conviene mas si definirlo como una llamada a funcion o dejarlo como un id, como si fuera una asignacion normal, tomando en cuenta que es el return de una funcion [supuestamente ya definido (trabajado) asi que no tendria que hacer mas porque ya lo tendria que tener]

            array_ini.Rule = ID + LEFT_BRACKET + expression + RIGHT_BRACKET + COLON_EQUAL + expression + SEMI_COLON;


            parameters.Rule = MakeStarRule(parameters, SEMI_COLON ,parameter); // Estos parámetros pueden tener un tipo de datos estándar, un tipo de datos definido por el usuario o un tipo de datos de subrango.
            parameter.Rule = list_id + COLON + variables_native_id //pueden ser id's de variables simples o matrices o subprogramas
                | VAR + list_id + COLON + variables_native_id;

            type_variables.Rule = MakePlusRule(type_variables, type_variable);
            type_variable.Rule = list_id + EQUAL + variables_native_array + SEMI_COLON; //TODO: si viene una lista y es de tipo array se puede inicializar todas o solo inicializo la primera

            //TODO: verificar en el semantico que 'expresion' solo seran valores de tipo base y id de otras constantes... no arrays
            constant_variables.Rule = MakePlusRule(constant_variables, constant_variable);
            constant_variable.Rule = ID + EQUAL + expression + SEMI_COLON;

            if_statements.Rule = IF + expression + THEN + main_declarations
                | IF + expression + THEN + main_declarations + ELSE + main_declarations
                | IF + expression + THEN + main_declarations + else_if_list + ELSE + main_declarations;

            else_if_list.Rule = MakePlusRule(else_if_list, else_if);
            else_if.Rule = ELSE + IF + expression + THEN + main_declarations;

            #region case
            /*
             * Todas las constantes de casos deben tener el mismo tipo.
             * If no else part is present, and no case constant matches the expression value, program flow continues after the final end
             * los case's/else pueden tener el bloque BEGIN - END
             * integers, characters, boolean or enumerated data items
             * The case label for a case must be the same data type as the expression in the case statement, and it must be a constant or a literal.
             * TODO: verificar si los cases pueden tener una variable o solo valores ya conocidos
             */

            case_statements.Rule = CASE + expression + OF + case_list + cases_words + END + SEMI_COLON;

            case_list.Rule = MakePlusRule(case_list, cases);
            cases.Rule = values_native + COLON + main_declarations;//no se permite lista de valores en casos en Compipascal //TODO: no se puede id?

            cases_words.Rule = ELSE + main_declarations 
                | OTHERWISE + main_declarations
                | Empty;


            #endregion

            while_statements.Rule = WHILE + expression + DO + main_declarations + SEMI_COLON;
            for_do_statements.Rule = FOR + ID + COLON_EQUAL + expression + TO + expression + DO + main_declarations + SEMI_COLON;
            //TODO: aqui validar que en lugar de expresion, solo se acepte condicionales
            repeat_statements.Rule = REPEAT + main_declarations + UNTIL + expression + SEMI_COLON;

            function_call.Rule = ID + LEFT_PARENTHESIS + expression_list + RIGHT_PARENTHESIS;
            expression_list.Rule = MakeStarRule(expression_list, COMMA ,expression);

            procedure_call.Rule = ID + LEFT_PARENTHESIS + expression_list + RIGHT_PARENTHESIS;

            array_call.Rule = ID + LEFT_BRACKET + expression + RIGHT_BRACKET;


            //TODO: verificar que podre hacer en este lugar y que no
            instructions.Rule = MakePlusRule(instructions, instruction);
            instruction.Rule = Empty;

            expression.Rule = aritmeticas | relacionales | logicas;

            aritmeticas.Rule = MINUS + aritmeticas
                | PLUS + aritmeticas
                | aritmeticas + PLUS + aritmeticas //cadena
                | aritmeticas + MINUS + aritmeticas
                | aritmeticas + ASTERISK + aritmeticas
                | aritmeticas + SLASH + aritmeticas
                | aritmeticas + MODULUS + aritmeticas
                | LEFT_PARENTHESIS + aritmeticas + RIGHT_PARENTHESIS
                | expression_terminales;

            relacionales.Rule = relacionales + EQUAL + relacionales
                | relacionales + LESS_THAN_GREATER_THAN + relacionales
                | relacionales + GREATER_THAN + relacionales
                | relacionales + LESS_THAN + relacionales
                | relacionales + GREATER_THAN_EQUAL + relacionales
                | relacionales + LESS_THAN_EQUAL + relacionales
                | LEFT_PARENTHESIS + relacionales + RIGHT_PARENTHESIS
                | expression_terminales;

            logicas.Rule = logicas + AND + logicas //bool
                | logicas + OR + logicas //bool
                | logicas + NOT + logicas //bool
                | LEFT_PARENTHESIS + logicas + RIGHT_PARENTHESIS
                | expression_terminales;//bool

            expression_terminales.Rule = ID | CADENA | NUMBER 
                | TRUE | FALSE | function_call | array_call //| VOID  //|types-objects 
                | NUMBER + PERIOD_PERIOD + NUMBER; // char (palabra reservada) //TODO: array_call  tamanio fijo, variables mismo tipo, declarar, inicializar, acceder, indice para los arrays?


            values_native.Rule = CADENA | NUMBER | TRUE | FALSE; //valores
            values_native_id.Rule = values_native | ID;
            variables_native.Rule = INTEGER | REAL | BOOLEAN | CHAR | STRING; //native reserved words
            variables_array.Rule = ARRAY + LEFT_BRACKET + expression + RIGHT_BRACKET + OF + variables_native; //array //tipo string[], int[], id[] //TODO: tipos de elementos para las matrices solo nativos o tambien otros? por el momento solo lo tengo nativos //despues del OF tambien acepta esto 1 .. 26
            variables_native_array.Rule = variables_native | variables_array; //native, array
            variables_native_id.Rule = variables_native | ID; //native, id
            //TODO: verificar que en las fun/proc se puede
            //enviar parametros con un tipo predefinido por el usuario
            #endregion

            #region PREFERENCIAS
            /* Configuraciones especiales necesarias para el uso de Irony. */
            this.Root = ini;
            //MarkTransient(instruccion);

            //precedencia y asociatividad
            //TRUE, FALSE?
            RegisterOperators(4, NOT); //~
            RegisterOperators(3, ASTERISK, SLASH, DIV, MOD, AND); //&
            RegisterOperators(2, PLUS, MINUS, OR); // |, !
            RegisterOperators(1, EQUAL, LESS_THAN_GREATER_THAN, LESS_THAN, LESS_THAN_EQUAL, GREATER_THAN, GREATER_THAN_EQUAL, IN); //or else, and then

            //MarkPunctuation("(", ")", "[", "]");
            //RegisterOperators(1, Associativity.Left,  MAS, MENOS);
            #endregion

        }
    }
}

/*
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
 * 
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