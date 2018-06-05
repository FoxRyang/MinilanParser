// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  HACKER
// DateTime: 2018-04-30 오후 2:27:55
// UserName: Fox
// Input file <proj.y - 2018-04-30 오후 2:27:35>

// options: lines

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace RealTree
{
internal enum Tokens {error=126,
    EOF=127,_SKIP=128,ABORT=129,GT=130,NE=131,EQ=132,
    LT=133,LTE=134,GTE=135,PLUS=136,MINUS=137,MULTIPLY=138,
    DIVIDE=139,ID=140,NUMBER=141,IF=142,END_IF=143,DO=144,
    END_DO=145,ERROR=146,EOL=147,UMINUS=148};

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal partial class Parser: ShiftReduceParser<RealTree.Node, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[37];
  private static State[] states = new State[68];
  private static string[] nonTerms = new string[] {
      "PROGRAM", "$accept", "SENTENCES", "SENTENCE", "SEQUENCE", "ASSIGNSEQUENCE", 
      "IFSEQUENCE", "DOSEQUENCE", "Term", "EXPR", "IDT", "SLSQs", "SLSQ", "GUARD", 
      };

  static Parser() {
    states[0] = new State(new int[]{128,7,129,8,45,20,140,22,141,23,142,36,144,48,146,67},new int[]{-1,1,-3,3,-4,4,-5,9,-6,10,-9,11,-11,19,-7,35,-8,47});
    states[1] = new State(new int[]{127,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{59,5,127,-5,124,-5,143,-5,145,-5});
    states[5] = new State(new int[]{128,7,129,8,45,20,140,22,141,23,142,36,144,48},new int[]{-3,6,-4,4,-5,9,-6,10,-9,11,-11,19,-7,35,-8,47});
    states[6] = new State(-4);
    states[7] = new State(-6);
    states[8] = new State(-7);
    states[9] = new State(-8);
    states[10] = new State(-9);
    states[11] = new State(new int[]{44,12,58,32});
    states[12] = new State(new int[]{45,20,140,22,141,23},new int[]{-6,13,-9,11,-11,19});
    states[13] = new State(new int[]{44,14});
    states[14] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,15,-9,16,-11,19});
    states[15] = new State(-12);
    states[16] = new State(new int[]{43,17,45,24,42,26,47,28,37,30,59,-23,127,-23,124,-23,143,-23,145,-23,44,-23,62,-23,60,-23,61,-23,33,-23,58,-23});
    states[17] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,18,-9,16,-11,19});
    states[18] = new State(-18);
    states[19] = new State(-14);
    states[20] = new State(new int[]{140,21});
    states[21] = new State(-16);
    states[22] = new State(-17);
    states[23] = new State(-15);
    states[24] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,25,-9,16,-11,19});
    states[25] = new State(-19);
    states[26] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,27,-9,16,-11,19});
    states[27] = new State(-20);
    states[28] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,29,-9,16,-11,19});
    states[29] = new State(-21);
    states[30] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,31,-9,16,-11,19});
    states[31] = new State(-22);
    states[32] = new State(new int[]{61,33});
    states[33] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,34,-9,16,-11,19});
    states[34] = new State(-13);
    states[35] = new State(-10);
    states[36] = new State(new int[]{143,37,45,20,140,22,141,23},new int[]{-12,38,-13,40,-14,43,-10,52,-9,16,-11,19});
    states[37] = new State(-24);
    states[38] = new State(new int[]{143,39});
    states[39] = new State(-25);
    states[40] = new State(new int[]{124,41,143,-28,145,-28});
    states[41] = new State(new int[]{45,20,140,22,141,23},new int[]{-12,42,-13,40,-14,43,-10,52,-9,16,-11,19});
    states[42] = new State(-29);
    states[43] = new State(new int[]{58,44});
    states[44] = new State(new int[]{62,45});
    states[45] = new State(new int[]{128,7,129,8,45,20,140,22,141,23,142,36,144,48},new int[]{-3,46,-4,4,-5,9,-6,10,-9,11,-11,19,-7,35,-8,47});
    states[46] = new State(-30);
    states[47] = new State(-11);
    states[48] = new State(new int[]{145,49,45,20,140,22,141,23},new int[]{-12,50,-13,40,-14,43,-10,52,-9,16,-11,19});
    states[49] = new State(-26);
    states[50] = new State(new int[]{145,51});
    states[51] = new State(-27);
    states[52] = new State(new int[]{62,53,60,57,61,61,33,64});
    states[53] = new State(new int[]{61,55,45,20,140,22,141,23},new int[]{-10,54,-9,16,-11,19});
    states[54] = new State(-31);
    states[55] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,56,-9,16,-11,19});
    states[56] = new State(-32);
    states[57] = new State(new int[]{61,59,45,20,140,22,141,23},new int[]{-10,58,-9,16,-11,19});
    states[58] = new State(-33);
    states[59] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,60,-9,16,-11,19});
    states[60] = new State(-34);
    states[61] = new State(new int[]{61,62});
    states[62] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,63,-9,16,-11,19});
    states[63] = new State(-35);
    states[64] = new State(new int[]{61,65});
    states[65] = new State(new int[]{45,20,140,22,141,23},new int[]{-10,66,-9,16,-11,19});
    states[66] = new State(-36);
    states[67] = new State(-3);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,127});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-1, new int[]{146});
    rules[4] = new Rule(-3, new int[]{-4,59,-3});
    rules[5] = new Rule(-3, new int[]{-4});
    rules[6] = new Rule(-4, new int[]{128});
    rules[7] = new Rule(-4, new int[]{129});
    rules[8] = new Rule(-4, new int[]{-5});
    rules[9] = new Rule(-5, new int[]{-6});
    rules[10] = new Rule(-5, new int[]{-7});
    rules[11] = new Rule(-5, new int[]{-8});
    rules[12] = new Rule(-6, new int[]{-9,44,-6,44,-10});
    rules[13] = new Rule(-6, new int[]{-9,58,61,-10});
    rules[14] = new Rule(-9, new int[]{-11});
    rules[15] = new Rule(-9, new int[]{141});
    rules[16] = new Rule(-11, new int[]{45,140});
    rules[17] = new Rule(-11, new int[]{140});
    rules[18] = new Rule(-10, new int[]{-9,43,-10});
    rules[19] = new Rule(-10, new int[]{-9,45,-10});
    rules[20] = new Rule(-10, new int[]{-9,42,-10});
    rules[21] = new Rule(-10, new int[]{-9,47,-10});
    rules[22] = new Rule(-10, new int[]{-9,37,-10});
    rules[23] = new Rule(-10, new int[]{-9});
    rules[24] = new Rule(-7, new int[]{142,143});
    rules[25] = new Rule(-7, new int[]{142,-12,143});
    rules[26] = new Rule(-8, new int[]{144,145});
    rules[27] = new Rule(-8, new int[]{144,-12,145});
    rules[28] = new Rule(-12, new int[]{-13});
    rules[29] = new Rule(-12, new int[]{-13,124,-12});
    rules[30] = new Rule(-13, new int[]{-14,58,62,-3});
    rules[31] = new Rule(-14, new int[]{-10,62,-10});
    rules[32] = new Rule(-14, new int[]{-10,62,61,-10});
    rules[33] = new Rule(-14, new int[]{-10,60,-10});
    rules[34] = new Rule(-14, new int[]{-10,60,61,-10});
    rules[35] = new Rule(-14, new int[]{-10,61,61,-10});
    rules[36] = new Rule(-14, new int[]{-10,33,61,-10});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // PROGRAM -> SENTENCES
#line 18 "proj.y"
                    { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 3: // PROGRAM -> ERROR
#line 19 "proj.y"
          { }
#line default
        break;
      case 4: // SENTENCES -> SENTENCE, ';', SENTENCES
#line 22 "proj.y"
                                  { CurrentSemanticValue = MakeRootNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 5: // SENTENCES -> SENTENCE
#line 23 "proj.y"
             { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 6: // SENTENCE -> _SKIP
#line 25 "proj.y"
                { CurrentSemanticValue = MakeSkipNode();}
#line default
        break;
      case 7: // SENTENCE -> ABORT
#line 26 "proj.y"
          { CurrentSemanticValue = MakeAbort(); }
#line default
        break;
      case 8: // SENTENCE -> SEQUENCE
#line 27 "proj.y"
             { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 9: // SEQUENCE -> ASSIGNSEQUENCE
#line 29 "proj.y"
                         { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 10: // SEQUENCE -> IFSEQUENCE
#line 30 "proj.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 11: // SEQUENCE -> DOSEQUENCE
#line 31 "proj.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 12: // ASSIGNSEQUENCE -> Term, ',', ASSIGNSEQUENCE, ',', EXPR
#line 33 "proj.y"
                                                 { CurrentSemanticValue = MakeAssignDeep(ValueStack[ValueStack.Depth-5], ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 13: // ASSIGNSEQUENCE -> Term, ':', '=', EXPR
#line 34 "proj.y"
                      { CurrentSemanticValue = MakeAssign(ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 14: // Term -> IDT
#line 36 "proj.y"
            { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 16: // IDT -> '-', ID
#line 40 "proj.y"
                         { CurrentSemanticValue = MakeUnIdLeaf(ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 18: // EXPR -> Term, '+', EXPR
#line 44 "proj.y"
                     { CurrentSemanticValue = MakeBinary(NodeTag.plus , ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 19: // EXPR -> Term, '-', EXPR
#line 45 "proj.y"
                   { CurrentSemanticValue = MakeBinary(NodeTag.minus , ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 20: // EXPR -> Term, '*', EXPR
#line 46 "proj.y"
                   { CurrentSemanticValue = MakeBinary(NodeTag.mul , ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 21: // EXPR -> Term, '/', EXPR
#line 47 "proj.y"
                   { CurrentSemanticValue = MakeBinary(NodeTag.div , ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 22: // EXPR -> Term, '%', EXPR
#line 48 "proj.y"
                   { CurrentSemanticValue = MakeBinary(NodeTag.rem , ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 23: // EXPR -> Term
#line 49 "proj.y"
          { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 24: // IFSEQUENCE -> IF, END_IF
#line 52 "proj.y"
                      { CurrentSemanticValue = MakeAbort(); }
#line default
        break;
      case 25: // IFSEQUENCE -> IF, SLSQs, END_IF
#line 53 "proj.y"
                     { CurrentSemanticValue = MakeIfSequence(ValueStack[ValueStack.Depth-2]);}
#line default
        break;
      case 26: // DOSEQUENCE -> DO, END_DO
#line 56 "proj.y"
                      { CurrentSemanticValue = MakeAbort(); }
#line default
        break;
      case 27: // DOSEQUENCE -> DO, SLSQs, END_DO
#line 57 "proj.y"
                     { CurrentSemanticValue = MakeDoSequence(ValueStack[ValueStack.Depth-2]);}
#line default
        break;
      case 28: // SLSQs -> SLSQ
#line 59 "proj.y"
              { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 29: // SLSQs -> SLSQ, '|', SLSQs
#line 60 "proj.y"
                   { CurrentSemanticValue = MakeSLNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);}
#line default
        break;
      case 30: // SLSQ -> GUARD, ':', '>', SENTENCES
#line 62 "proj.y"
                               { CurrentSemanticValue = MakeCondition(ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-3]); }
#line default
        break;
      case 31: // GUARD -> EXPR, '>', EXPR
#line 65 "proj.y"
                      { CurrentSemanticValue = MakeGaurd(NodeTag.gt ,ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 32: // GUARD -> EXPR, '>', '=', EXPR
#line 66 "proj.y"
                      { CurrentSemanticValue = MakeGaurd(NodeTag.gte ,ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]); }
#line default
        break;
      case 33: // GUARD -> EXPR, '<', EXPR
#line 67 "proj.y"
                   { CurrentSemanticValue = MakeGaurd(NodeTag.lt ,ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 34: // GUARD -> EXPR, '<', '=', EXPR
#line 68 "proj.y"
                      { CurrentSemanticValue = MakeGaurd(NodeTag.lte ,ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]); }
#line default
        break;
      case 35: // GUARD -> EXPR, '=', '=', EXPR
#line 69 "proj.y"
                      { CurrentSemanticValue = MakeGaurd(NodeTag.eq ,ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]); }
#line default
        break;
      case 36: // GUARD -> EXPR, '!', '=', EXPR
#line 70 "proj.y"
                      { CurrentSemanticValue = MakeGaurd(NodeTag.neq ,ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]); }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 74 "proj.y"
#line default
}
}
