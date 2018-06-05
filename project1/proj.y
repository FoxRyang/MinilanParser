%namespace RealTree
%output=RealTreeParser.cs
%partial
%sharetokens
%visibility internal

%YYSTYPE RealTree.Node

%start PROGRAM

%token _SKIP ABORT GT NE EQ LT LTE GTE PLUS MINUS MULTIPLY DIVIDE ID NUMBER IF END_IF DO END_DO ERROR EOL

%left '+' '-'
%left '*' '/' '%'
%left UMINUS

%%			/*	rules	*/
PROGRAM : SENTENCES	{ $$ = $1; }
		| ERROR	{ }
;

SENTENCES:	SENTENCE ';' SENTENCES { $$ = MakeRootNode($1, $3);}
			|SENTENCE { $$ = $1; }
;
SENTENCE:	_SKIP { $$ = MakeSkipNode();}
			|ABORT { $$ = MakeAbort(); }
			|SEQUENCE { $$ = $1; }
;
SEQUENCE:	ASSIGNSEQUENCE { $$ = $1; }
			|IFSEQUENCE { $$ = $1; }
			|DOSEQUENCE { $$ = $1; }
;
ASSIGNSEQUENCE: Term ',' ASSIGNSEQUENCE ',' EXPR { $$ = MakeAssignDeep($1, $3, $5); }
				|Term ':''=' EXPR { $$ = MakeAssign($1, $4); }
;
Term: 		IDT { $$ = $1; }
			| NUMBER
;

IDT:		'-' ID %prec UMINUS{ $$ = MakeUnIdLeaf($2);} // this maybe auto
			|ID
			;

EXPR:		Term '+' EXPR { $$ = MakeBinary(NodeTag.plus , $1, $3);}
			| Term '-' EXPR { $$ = MakeBinary(NodeTag.minus , $1, $3);}
			| Term '*' EXPR { $$ = MakeBinary(NodeTag.mul , $1, $3);}
			| Term '/' EXPR { $$ = MakeBinary(NodeTag.div , $1, $3);}
			| Term '%' EXPR { $$ = MakeBinary(NodeTag.rem , $1, $3);}
			| Term { $$ = $1; }
;

IFSEQUENCE: IF END_IF	{ $$ = MakeAbort(); }
			| IF SLSQs END_IF { $$ = MakeIfSequence($2);}
;

DOSEQUENCE: DO END_DO	{ $$ = MakeAbort(); }
			| DO SLSQs END_DO	{ $$ = MakeDoSequence($2);}
;
SLSQs: 		SLSQ { $$ = $1; }
			|SLSQ '|' SLSQs { $$ = MakeSLNode($1, $3);}
;
SLSQ: 		GUARD ':''>' SENTENCES { $$ = MakeCondition($1, $4); }
;

GUARD:		EXPR '>' EXPR { $$ = MakeGaurd(NodeTag.gt ,$1, $3); }
			| EXPR '>''=' EXPR { $$ = MakeGaurd(NodeTag.gte ,$1, $4); }
			| EXPR '<' EXPR { $$ = MakeGaurd(NodeTag.lt ,$1, $3); }
			| EXPR '<''=' EXPR { $$ = MakeGaurd(NodeTag.lte ,$1, $4); }
			| EXPR '=''=' EXPR { $$ = MakeGaurd(NodeTag.eq ,$1, $4); }
			| EXPR '!''=' EXPR { $$ = MakeGaurd(NodeTag.neq ,$1, $4); }
;

%%
