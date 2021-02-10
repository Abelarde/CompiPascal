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
            NonTerminal program_structure_stmt = new NonTerminal("program_structure_stmt");
            NonTerminal statements = new NonTerminal("statements");
            NonTerminal statement = new NonTerminal("statement");

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
            NonTerminal variables_declarations_initialization = new NonTerminal("variables_declarations_initialization");
            NonTerminal variables_declaration_initialization = new NonTerminal("variables_declaration_initialization");
            NonTerminal variable_declaration_initialization = new NonTerminal("variable_declaration_initialization");

            NonTerminal functions_declarations = new NonTerminal("functions_declarations");
            NonTerminal procedures_declarations = new NonTerminal("procedures_declarations");
            NonTerminal parameters = new NonTerminal("parameters");
            NonTerminal parameter = new NonTerminal("parameter");

            NonTerminal main_declarations = new NonTerminal("main_declarations");

            NonTerminal list_id = new NonTerminal("list_id");


            NonTerminal instructions = new NonTerminal("instructions");
            NonTerminal instruction = new NonTerminal("instruction");

            NonTerminal expression = new NonTerminal("expression");
            NonTerminal values_native = new NonTerminal("values_native");
            NonTerminal variables_native = new NonTerminal("variables_native");
            NonTerminal variables_array = new NonTerminal("variables_array");
            NonTerminal variables_native_array = new NonTerminal("variables_native_array");
            NonTerminal variables_native_id = new NonTerminal("variables_native_id");



            #endregion

            #region GRAMATICA /* Región donde se define la gramática. */

            ini.Rule = program_structure;

            program_structure.Rule = PROGRAM + ID + SEMI_COLON + constant_optional + program_structure_stmt;

            constant_optional.Rule = constant_declarations //constantes: deben de declararse antes que todas las variables
                | Empty;

            program_structure_stmt.Rule = statements + main_declarations + PERIOD;

            statements.Rule = MakeStarRule(statements, statement);

            statement.Rule = type_declarations
                | variables_declarations
                //| variables_declarations_initialization
                | functions_declarations + SEMI_COLON
                | procedures_declarations + SEMI_COLON;


            type_declarations.Rule = TYPE + type_variables | Empty; //categoria, o clase de los tipos como integer, real, string
            constant_declarations.Rule = CONST + constant_variables | Empty; // 
            variables_declarations.Rule = VAR + variables | Empty; //declaracion: solo afuera de BEGIN-END; 
            variables_declarations_initialization.Rule = VAR + variables_declaration_initialization | Empty;
            functions_declarations.Rule = FUNCTION + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + BEGIN + END; //TODO: return_value
            procedures_declarations.Rule = PROCEDURE + ID + LEFT_PARENTHESIS + parameters + RIGHT_PARENTHESIS + BEGIN + END;
            main_declarations.Rule = BEGIN + END; //aqui inicia la ejecucion del programa

            variables.Rule = MakePlusRule(variables, variable);
            variable.Rule = list_id + COLON + variables_native_id + SEMI_COLON; //indica el tipo de valores que puede tomar la variable

            list_id.Rule = MakePlusRule(list_id, COMMA ,ID);

            //TODO: aqui tener cuidado si lo hago afuera del cuerpo o si lo hago adentro del cuerpo.
            //con la asignacion porque ID no se permite afuera solo adentro
            variables_declaration_initialization.Rule = MakePlusRule(variables_declaration_initialization, variable_declaration_initialization);
            variable_declaration_initialization.Rule = ID + COLON + variables_native_id + COLON_EQUAL + expression + SEMI_COLON;

            parameters.Rule = MakeStarRule(parameters, SEMI_COLON ,parameter);
            parameter.Rule = list_id + COLON + variables_native
                | VAR + list_id + COLON + variables_native;

            type_variables.Rule = MakePlusRule(type_variables, type_variable);
            type_variable.Rule = list_id + EQUAL + variables_native + SEMI_COLON;

            constant_variables.Rule = MakePlusRule(constant_variables, constant_variable);
            constant_variable.Rule = ID + EQUAL + values_native + SEMI_COLON;


            instructions.Rule = MakePlusRule(instructions, instruction);
            instruction.Rule = Empty;

            expression.Rule = MINUS + expression
                | PLUS + expression
                | expression + PLUS + expression
                | expression + MINUS + expression
                | expression + ASTERISK + expression
                | expression + SLASH + expression
                | ID | CADENA | NUMBER | TRUE | FALSE  //| VOID  //|types-objects
                | ID + LEFT_BRACKET + expression + RIGHT_BRACKET// array_id[]
                | LEFT_PARENTHESIS + expression + RIGHT_PARENTHESIS;

            values_native.Rule = CADENA | NUMBER | TRUE | FALSE;
            variables_native.Rule = INTEGER | REAL | BOOLEAN | CHAR | STRING; //native
            variables_array.Rule = ARRAY + LEFT_BRACKET + RIGHT_BRACKET; //array
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