using System;
using System.Collections.Generic;
using System.Text;
using QUT.Gppg;
using System.IO;

namespace RealTree
{
    internal partial class Parser
    {


        Parser(Lexer s) : base(s) { }

        internal Node[] regs = new Node[26];

        internal List<string> printArgs = new List<string>();

        internal Dictionary<string, double> idDic = new Dictionary<string, double>();
        internal Dictionary<string, bool> idInitDic = new Dictionary<string, bool>();

        // ==================================================================================

        static void Main(string[] args)
        {
            System.IO.TextReader reader;
            if (args.Length > 0)
                reader = new System.IO.StreamReader(args[0]);
            else
                reader = System.Console.In;

            FileStream fsa = new FileStream(Directory.GetCurrentDirectory() + "/test.txt", FileMode.Open);
            reader = new StreamReader(fsa);

            StreamWriter output = new StreamWriter(Directory.GetCurrentDirectory() + "/output.txt");



            Parser parser = new Parser(new Lexer(reader));
            Console.WriteLine("RealCalc expression evaluator, type ^C to exit, help for help");
            parser.Parse();

            System.Console.Write(parser.ValueStack[1].Unparse());
            String str = parser.ValueStack[1].Unparse();
            //output.WriteLine(str);

            parser.ValueStack[1].Eval(parser);

            for(int i = 0; i < parser.printArgs.Count; i++)
            {
                output.WriteLine(parser.printArgs[i]);
            }

            output.Close();
        }

        // ==================================================================================
        //  Version for real arithmetic.  YYSTYPE is RealTree.Node
        //  This version uses an embedded scanner rather than a gplex-generated one.
        //
        class Lexer : QUT.Gppg.AbstractScanner<RealTree.Node, LexLocation>
        {
            private System.IO.TextReader reader;
            private StringBuilder text = new StringBuilder();

            public Lexer(System.IO.TextReader reader)
            {
                this.reader = reader;
            }

            private LexLocation singleton = new LexLocation();
            public override LexLocation yylloc
            {
                get { return singleton; }
            }

            int linenum = 0;

            public override int yylex()
            {
                char ch;
                char peek;
                int ord = reader.Read();
                //this.text.Clear();
                this.text.Length = 0;
                //
                // Must check for EOF
                //
                if (ord == -1)
                    return (int)Tokens.EOF;
                else
                    ch = (char)ord;

                if (ch == '\n')
                {
                    linenum++;
                    return yylex();
                }
                else if (char.IsWhiteSpace(ch))
                { // Skip white space
                    while (char.IsWhiteSpace(peek = (char)reader.Peek()))
                    {
                        ord = reader.Read();
                        if (ord == (int)'\n')
                        {
                            linenum++;
                            continue;
                        }
                    }
                    return yylex();
                }
                else if (char.IsDigit(ch))
                {
                    text.Append(ch);
                    while (char.IsDigit(peek = (char)reader.Peek()))
                        text.Append((char)reader.Read());
                    if ((peek = (char)reader.Peek()) == '.')
                        text.Append((char)reader.Read());
                    while (char.IsDigit(peek = (char)reader.Peek()))
                        text.Append((char)reader.Read());
                    try
                    {
                        yylval = Parser.MakeNumLeaf(double.Parse(text.ToString()));
                        return (int)Tokens.NUMBER;
                    }
                    catch (FormatException)
                    {
                        this.yyerror("Illegal number \"{0}\"", text);
                        return (int)Tokens.error;
                    }
                }
                else if (char.IsLetter(ch))
                {
                    text.Append(char.ToLower(ch));
                    while (char.IsLetter(peek = (char)reader.Peek()))
                        text.Append(char.ToLower((char)reader.Read()));
                    switch (text.ToString())
                    {
                        case "if":
                            return (int)Tokens.IF;
                        case "fi":
                            return (int)Tokens.END_IF;
                        case "do":
                            return (int)Tokens.DO;
                        case "od":
                            return (int)Tokens.END_DO;
                        case "print":
                            return (int)Tokens.PRINT;
                        default:
                            if (text.Length >= 1)
                            {
                                yylval = Parser.MakeIdLeaf(text.ToString());
                                return (int)Tokens.ID;
                            }
                            else
                            {
                                this.yyerror("Illegal name \"{0}\"", text);
                                return (int)Tokens.error;
                            }
                    }
                }
                else
                    switch (ch)
                    {
                        case '.':
                        case '+':
                        case '-':
                        case '*':
                        case '/':
                        case '(':
                        case ')':
                        case '%':
                        case '=':
                        case ':':
                        case '>':
                        case '<':
                        case '!':
                        case ',':
                        case ';':
                        case '|':
                            return ch;
                        default:
                            yyerror("Illegal character '{0}' on line '{1}'", ch, linenum);
                            return yylex();
                    }
            }

            public override void yyerror(string format, params object[] args)
            {
                Console.Error.WriteLine(format, args);
            }
        }
        // ==================================================================================
        //  End of Lexer class definition.
        // ==================================================================================

        //
        // Now the node factory methods
        //d
        public static Node MakeBinary(NodeTag tag, Node lhs, Node rhs)
        {
            return new Binary(tag, lhs, rhs);
        }

        //d
        public static Node MakeIdLeaf(string n)
        {
            return new Leaf(n);
        }
        //d

        public static Node MakeUnIdLeaf(Node node)
        {
            return new Unary(NodeTag.negate, node);
        }

        //d
        public static Node MakeNumLeaf(double b)
        {
            return new Leaf(b);
        }

        //d
        public static Node MakeRootNode(Node lhs, Node rhs)
        {
            return new Root(NodeTag.root, lhs, rhs);
        }

        //d
        public static Node MakeSLNode(Node lhs, Node rhs)
        {
            return new SLNode(lhs, rhs);
        }

        //d
        public static Node MakeSkipNode()
        {
            return new Leaf(NodeTag.skip);
        }

        //d
        public static Node MakeAbort()
        {
            return new Leaf(NodeTag.abort);
        }

        //d
        public static Node MakeAssignDeep(Node lhs, Node mid, Node rhs)
        {
            return new AssignNode(lhs, mid, rhs);
        }

        public static Node MakePrintNode(Node term)
        {
            return new PrintNode(term);
        }

        //d
        public static Node MakeAssign(Node lhs, Node rhs)
        {
            return new AssignNode(lhs, null, rhs);
        }
        //d
        public static Node MakeIfSequence(Node node)
        {
            return new IfStatus(node);
        }
        //d
        public static Node MakeDoSequence(Node node)
        {
            return new DoStatus(node);
        }

        //d
        public static Node MakeCondition(Node lhs, Node rhs)
        {
            return new Cond(lhs, rhs);
        }

        //d
        public static Node MakeGaurd(NodeTag tag, Node lhs, Node rhs)
        {
            return new Guard(tag, lhs, rhs);
        }

        // ==================================================================================
        // And the semantic helpers
        // ==================================================================================

        private void ClearRegisters()
        {
            for (int i = 0; i < regs.Length; i++)
                regs[i] = null;
        }

        private void PrintRegisters()
        {
            for (int i = 0; i < regs.Length; i++)
                if (regs[i] != null)
                    Console.WriteLine("regs[{0}] = '{1}' = {2}", i, (char)(i + (int)'a'), regs[i].Unparse());
        }

        private void AssignExpression(Node dst, Node expr)
        {
            Leaf destination = dst as Leaf;
            //regs[destination.Index] = expr;
        }

        private void CallExit()
        {
            Console.Error.WriteLine("RealTree will exit");
            System.Environment.Exit(1);
        }

        private double Eval(Node node)
        {
            try
            {
                //return node.Eval(this);
            }
            catch (CircularEvalException)
            {
                Scanner.yyerror("Eval has circular dependencies");
            }
            catch
            {
                Scanner.yyerror("Invalid expression evaluation");
            }
            return 0.0;
        }

        private void Display(Node node)
        {
            try
            {
                //double result = node.Eval(this);
                //Console.WriteLine("result: " + result.ToString());
            }
            catch (CircularEvalException)
            {
                Scanner.yyerror("Eval has circular dependencies");
            }
            catch
            {
                Scanner.yyerror("Invalid expression evaluation");
            }
        }

        public class CircularEvalException : Exception
        {
            internal CircularEvalException() { }
        }
    }

    // ==================================================================================
    //  Start of Node Definitions
    // ==================================================================================

    internal enum NodeTag
    {
        error, name, number, plus, minus, mul, div,
        rem, negate, gt, gte, lt, lte, eq, neq, assign, IF, DO, abort, skip, SL, root, cond, print
    }

    internal abstract class Node
    {
        readonly NodeTag tag;
        protected bool active = false;
        public NodeTag Tag { get { return this.tag; } }

        protected Node(NodeTag tag)
        {
            this.tag = tag;
        }
        public abstract Node Eval(Parser p);
        public abstract string Unparse();
        public virtual double GetValue(Parser p)
        {
            return 0;
        }

        public Node parent = null;

        public void Epilog() { this.active = false; }
    }

    internal class Root : Node
    {
        public Node lhs = null;
        public Node rhs = null;

        internal Root(NodeTag t, Node lhs, Node rhs) : base(t)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public override Node Eval(Parser p)
        {
            try
            {
                lhs.Eval(p);
                rhs.Eval(p);
            }
            finally
            {
                this.Epilog();
            }

            return this;
        }

        public override string Unparse()
        {
            return String.Format("[ Root {0}, {1} ]", lhs.Unparse(), rhs.Unparse());
        }
    }

    internal class IfStatus : Node
    {
        Node remain;

        internal IfStatus(Node node) : base(NodeTag.IF)
        {
            remain = node;
        }

        public override Node Eval(Parser p)
        {
            remain.Eval(p);

            var sls = remain as SLNode;

            bool flag = false;

            if (sls != null)
            {
                flag = sls.DoSL(p);
            }
            else
            {
                flag = (remain as Cond).DoSL(p);
            }

            if (!flag)
            {
                Console.WriteLine("ERRRRRRORRRR IFFFF ALLLLL FAAAAAAAAALSSSSEEEEE");
            }

            return this;
        }

        public override string Unparse()
        {
            return String.Format("[ If {0} ]", remain.Unparse());
        }
    }

    internal class DoStatus : Node
    {
        Node remain;

        internal DoStatus(Node node) : base(NodeTag.DO)
        {
            remain = node;
        }

        public override Node Eval(Parser p)
        {
            remain.Eval(p);

            var sls = remain as SLNode;

            if (sls != null)
            {
                bool flag = true;
                while (flag)
                {
                    flag = sls.DoSL(p);
                }
            }
            else
            {
                bool flag = true;
                while (flag)
                {
                    flag = (remain as Cond).DoSL(p);
                }
            }

            return this;
        }

        public override string Unparse()
        {
            return String.Format("[ Do {0} ]", remain.Unparse());
        }
    }

    internal class SLNode : Node
    {
        Node lhs;
        Node remain;

        public List<Node> slList = new List<Node>();
        public int count = 0;
        public bool isChild = false;

        internal SLNode(Node lhs, Node remain) : base(NodeTag.SL)
        {
            this.lhs = lhs;
            this.remain = remain;
        }

        public override Node Eval(Parser p)
        {
            var SL = remain as SLNode;
            if(SL == null)
            {
                slList.Add(lhs);
                slList.Add(remain);
                count = 2;
            }
            else
            {
                SL.isChild = true;
                SL.Eval(p);
                count = SL.count + 1;

                slList.Add(lhs);

                foreach(var s in SL.slList)
                {
                    slList.Add(s);
                }
            }

            return this;
        }

        public bool DoSL(Parser p)
        {
            bool flag = false;

            if (!isChild)
            {
                var cList = new List<Cond>();

                foreach (var s in slList)
                {
                    var c = s as Cond;
                    if (c == null)
                    {
                        Console.WriteLine("BIIIGIGIGGGIG ERROR ON " + c.Unparse());

                        continue;
                    }

                    c.Eval(p);
                    if (c.GetBool(p))
                    {
                        cList.Add(c);
                        flag = true;
                    }
                }

                if (flag)
                {
                    int count = cList.Count;
                    Random random = new Random();
                    int rand = random.Next(count);

                    cList[rand].GetValue(p);
                }

            }
            else
            {
                Console.WriteLine("ERRRRRRRROOOOOOOORrrrrrr Child SL!!!!!!!!!!!!");
            }

            return flag;
        }

        public override string Unparse()
        {
            if (remain != null)
            {
                return String.Format("[ SL {0} | {1} ]", lhs.Unparse(), remain.Unparse());
            }
            else
            {
                return String.Format("[ SL {0} ]", remain.Unparse());
            }
        }
    }

    internal class Cond : Node
    {
        Node guard;
        Node expr;

        internal Cond(Node lhs, Node rhs) : base(NodeTag.cond)
        {
            guard = lhs;
            expr = rhs;
        }

        public override Node Eval(Parser p)
        {
            guard.Eval(p);

            return this;
        }

        public bool GetBool(Parser p)
        {
            return (guard as Guard).GetBool(p);
        }

        public bool DoSL(Parser p)
        {
            bool flag = false;

            var g = guard as Guard;
            g.Eval(p);
            if (g.GetBool(p))
            {
                expr.Eval(p);
                flag = true;
            }

            return flag;
        }

        public override double GetValue(Parser p)
        {
            expr.Eval(p);
            return 0;
        }

        public override string Unparse()
        {
            return String.Format("[ Cond {0} -> {1} ]", guard.Unparse(), expr.Unparse());
        }
    }

    internal class Guard : Node
    {
        NodeTag op;
        Node lhs, rhs;

        bool value = false;

        internal Guard(NodeTag t, Node lhs, Node rhs) : base(t)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            op = t;
            
        }

        public bool GetBool(Parser p)
        {
            return value;
        }

        public override Node Eval(Parser p)
        {
            lhs.Eval(p);
            rhs.Eval(p);

            value = Check(op, lhs.GetValue(p), rhs.GetValue(p));

            return this;
        }


        /// <summary>
        /// Not Used
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override double GetValue(Parser p)
        {
            rhs.Eval(p);

            return rhs.GetValue(p);
        }


        public override string Unparse()
        {
            return String.Format("[ GUARD {0} {1} {2} ]", lhs.Unparse(), Convert(Tag), rhs.Unparse());
        }

        static bool Check(NodeTag t, double a, double b)
        {
            switch (t)
            {
                case NodeTag.gt:
                    return a > b;
                case NodeTag.gte:
                    return a >= b;
                case NodeTag.eq:
                    return a == b;
                case NodeTag.neq:
                    return a != b;
                case NodeTag.lt:
                    return a < b;
                case NodeTag.lte:
                    return a <= b;
                default: return true;
            }
        }

        static string Convert(NodeTag t)
        {
            switch (t)
            {
                case NodeTag.gt:
                    return ">";
                case NodeTag.gte:
                    return ">=";
                case NodeTag.eq:
                    return "==";
                case NodeTag.neq:
                    return "!=";
                case NodeTag.lt:
                    return "<";
                case NodeTag.lte:
                    return "<=";
                default: return "<ERROR>";
            }
        }
    }

    internal class AssignNode : Node
    {
        public Node id, expr, remain = null;
        public List<Leaf> idList = new List<Leaf>();
        public List<Node> exprList = new List<Node>();

        public AssignNode NextAssign = null;

        public bool isChild = false;


        internal AssignNode(Node lhs, Node remain, Node rhs) : base(NodeTag.assign)
        {
            this.id = lhs;
            this.remain = remain;
            this.expr = rhs;
        }

        public override Node Eval(Parser p)
        {
            try
            {
                id.Eval(p);
                expr.Eval(p);

                if (remain == null)
                {
                    var l = id as Leaf;
                    idList.Add(l);
                    exprList.Add(expr);
                }
                else
                {
                    NextAssign = remain as AssignNode;
                    NextAssign.isChild = true;
                    NextAssign.Eval(p);

                    List<Leaf> k = new List<Leaf>();
                    List<Node> kk = new List<Node>();

                    k.Add(id as Leaf);

                    for (int i = 0; i < NextAssign.idList.Count; i++)
                    {
                        k.Add(NextAssign.idList[i]);
                    }

                    for (int i = 0; i < NextAssign.idList.Count; i++)
                    {
                        kk.Add(NextAssign.exprList[i]);
                    }

                    kk.Add(expr);

                    idList = k;
                    exprList = kk;
                }

                if (!isChild)
                {
                    Dictionary<string, double> nDic = new Dictionary<string, double>();

                    for (int i = 0; i < idList.Count; i++)
                    {
                        idList[i].Eval(p);
                        exprList[i].Eval(p);

                        nDic[idList[i].name] = exprList[i].GetValue(p);
                    }

                    foreach (var dd in nDic)
                    {
                        p.idInitDic[dd.Key] = true;
                        p.idDic[dd.Key] = dd.Value;
                    }
                }
            }
            catch (Exception e)
            {
            }

            return this;
        }

        public override string Unparse()
        {
            if (remain != null)
            {
                return String.Format("[AssignCtn {0}, {1}, {2} ]", id.Unparse(), remain.Unparse(), expr.Unparse());
            }
            else
            {
                return String.Format("[Assign {0} {1}]", id.Unparse(), expr.Unparse());
            }
        }
    }

    internal class PrintNode : Node
    {
        Leaf node;

        internal PrintNode(Node n) : base(NodeTag.print) { this.node = n as Leaf; }

        public override Node Eval(Parser p)
        {
            node.Eval(p);

            Console.WriteLine(node.GetValue(p));

            p.printArgs.Add(node.GetValue(p).ToString());

            return this;
        }

        public override string Unparse()
        {
            return String.Format("[Print {0}]", node.Unparse());
        }
    }

    internal class Leaf : Node
    {
        public string name;
        double value;
        internal Leaf(string c) : base(NodeTag.name) { this.name = c; }
        internal Leaf(double v) : base(NodeTag.number) { this.value = v; }
        internal Leaf(NodeTag t) : base(t) { }

        public double Value { get { return value; } }

        public override Node Eval(Parser p)
        {
            try
            {
                switch (Tag)
                {
                    case NodeTag.name:
                        if (!p.idDic.ContainsKey(name))
                        {
                            p.idDic.Add(name, 0);
                            p.idInitDic.Add(name, false);
                        }
                        break;
                    case NodeTag.number:
                        break;
                }
            }
            finally
            {
                this.Epilog();
            }

            return this;
        }

        public override double GetValue(Parser p)
        {
            switch (Tag)
            {
                case NodeTag.name:
                    return p.idDic[name];
                case NodeTag.number:
                default:
                    return value;
            }
        }

        public override string Unparse()
        {
            if (Tag == NodeTag.name)
                return this.name;
            else
                return this.value.ToString();
        }
    }

    internal class Unary : Node
    {
        Node child;

        public double value;

        internal Unary(NodeTag t, Node c)
            : base(t)
        { this.child = c; }

        public override Node Eval(Parser p)
        {
            try
            {
                child.Eval(p);
                value = -child.GetValue(p);
                return this;
            }
            finally
            {
                this.Epilog();
            }
        }

        public override double GetValue(Parser p)
        {
            return value;
        }

        public override string Unparse()
        {
            return String.Format("( - {0})", this.child.Unparse());
        }
    }

    internal class Binary : Node
    {
        Node lhs;
        Node rhs;

        double value;

        internal Binary(NodeTag t, Node l, Node r) : base(t)
        {
            this.lhs = l; this.rhs = r;
        }

        public override Node Eval(Parser p)
        {
            try
            {
                lhs.Eval(p);
                rhs.Eval(p);



                double lVal = this.lhs.GetValue(p);
                double rVal = this.rhs.GetValue(p);

                switch (this.Tag)
                {
                    case NodeTag.div: value = lVal / rVal; break;
                    case NodeTag.minus: value = lVal - rVal; break;
                    case NodeTag.plus: value = lVal + rVal; break;
                    case NodeTag.rem: value = lVal % rVal; break;
                    case NodeTag.mul: value = lVal * rVal; break;
                    default: throw new Exception("bad tag");
                }
            }
            finally
            {
                this.Epilog();


            }

            return this;
        }

        public override double GetValue(Parser p)
        {
            return value;
        }

        public override string Unparse()
        {
            string op = "";
            switch (this.Tag)
            {
                case NodeTag.div: op = "/"; break;
                case NodeTag.minus: op = "-"; break;
                case NodeTag.plus: op = "+"; break;
                case NodeTag.rem: op = "%"; break;
                case NodeTag.mul: op = "*"; break;
            }
            return String.Format("({0} {1} {2})", this.lhs.Unparse(), op, this.rhs.Unparse());
        }
    }
    // ==================================================================================
}

