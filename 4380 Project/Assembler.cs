using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _4380_Project
{
    public partial class Assembler
    {
        private static Hashtable Ltable;

        private static int address;

        public static int PC;
        public static List<byte> list;
        private static FileStream fs;

        private enum Labels 
        {
            MAIN,ENDWHILE, POSTFLUSH,UNDERFLOW,OVERFLOW,WHILENAT,ENDPROGRAM,PRINT,SIGNROOM,WHILENK,
            TOOBIG, TOOBIGSTART,ENDGETDATA, COMPAREPLUS,ENDCOMPARE,COMPAREMINUS,ENDCOMPARE1,WHILEDATA,POSTMAINLOOP,GETDATALOOP,whileflush,FLAG1,ENDFLAGCHECK,IFNFLAG,ENDOPD,STOREINT,ELSEFLAG,PRINTVALID,ENDVALID,WHILENFLAG
            ,ISZERO,ISONE,ISTWO,ISTHREE,ISFOUR,ISFIVE,ISSIX,ISSEVEN,ISEIGHT,ISNINE,NOT,END,
            //Function names here. Need to keep these in the same enum based on how my assembler works at the moment.
            flush, getdata, opd, reset,
        }
        
        public Assembler(string file)
        {
            Ltable = new Hashtable(50);
            address = 4; //Just like project 2, this is to account for the PC for the first four bytes.
            list = new List<byte>();
            fs = new FileStream("ByteCode.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            FirstPass(file);
            SecondPass(file);

            //okay
            foreach (var i in list)
            {
                fs.WriteByte(i);
            }
            fs.Close();
            while (list.Count < 1000001)
            {

                list.Add(BitConverter.GetBytes(0)[0]);

            }
        }

        private static void SecondPass(string file)
        {
            try
            {
                bool isnot = false;
                byte[] b;
                bool isOp = false;
                char[] delimiters = { ' ', ',', '\t', '(',')', '\0'};

                foreach (var i in File.ReadLines(file))
                {
                    if (i == string.Empty)
                    {
                        continue;
                    }
                    foreach (var s in i.Split(new[]{" ",", ","\t","( ",") ","\0"},StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (isOp && (s == ".BYT" || s == ".INT"))
                        {
                            throw new Exception(
                                "Directive is not placed properly. Should be at the beginning, not where op codes are.");
                        }
                        if (s == "")
                        {
                            continue;
                        }

                        
                        if (s.Contains(";"))
                        {
                            break;
                        }
                        if (s == string.Empty)
                        {
                            continue;
                        }
                        if (s == ".BYT" || s == ".INT")
                        {
                            continue;
                        }

                        if (s == "START")
                        {
                            ;
                        }
                        //This is for register indirect values.
                        if (i.Contains("("))
                        {
                            var rex = new Regex(@"/\(([^)]+)\)/");
                            switch (s)
                            {
                                case "STR":
                                    b = BitConverter.GetBytes(22);
                                    list.AddRange(b);
                                    continue;
                                case "LDR":
                                    b = BitConverter.GetBytes(23);
                                    list.AddRange(b);
                                    continue;
                                case "STB":
                                    b = BitConverter.GetBytes(24);
                                    list.AddRange(b);
                                    continue;
                                case "LDB":
                                    b = BitConverter.GetBytes(25);
                                    list.AddRange(b);
                                    continue;
                            }

                           
                        }

                        switch (s)
                        {
                            case "R0":
                                b = BitConverter.GetBytes(0);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R1":
                                b = BitConverter.GetBytes(1);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R2":
                                b = BitConverter.GetBytes(2);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R3":
                                b = BitConverter.GetBytes(3);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R4":
                                b = BitConverter.GetBytes(4);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R5":
                                b = BitConverter.GetBytes(5);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R6":
                                b = BitConverter.GetBytes(6);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R7":
                                b = BitConverter.GetBytes(7);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R8":
                                b = BitConverter.GetBytes(8);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R9":
                                b = BitConverter.GetBytes(9);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R10":
                                b = BitConverter.GetBytes(10);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R11":
                                b = BitConverter.GetBytes(11);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R12":
                                b = BitConverter.GetBytes(12);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R13":
                                b = BitConverter.GetBytes(13);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R14":
                                b = BitConverter.GetBytes(14);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "R15":
                                b = BitConverter.GetBytes(15);
                                list.AddRange(b);
                                if (i.Contains("JMR"))
                                {
                                    list.AddRange(BitConverter.GetBytes(0));
                                }
                                continue;

                            case "FP":
                                b = BitConverter.GetBytes(20);
                                list.AddRange(b);
                                continue;
                            case "SL":
                                b = BitConverter.GetBytes(17);
                                list.AddRange(b);
                                continue;
                            case "SB":
                                b = BitConverter.GetBytes(18);
                                list.AddRange(b);
                                continue;

                            case "SP":
                                b = BitConverter.GetBytes(19);
                                list.AddRange(b);
                                continue;
                            case "ADD":
                                b = BitConverter.GetBytes(13);
                                list.AddRange(b);
                                isOp = true;
                                continue;

                            case "OR":
                                b = BitConverter.GetBytes(19);
                                list.AddRange(b);
                                isOp = true;
                                continue;

                            case "AND":
                                b = BitConverter.GetBytes(18);
                                list.AddRange(b);
                                isOp = true;
                                continue;
                            case "SUB":
                                b = BitConverter.GetBytes(15);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "MUL":
                                b = BitConverter.GetBytes(16);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "DIV":
                                b = BitConverter.GetBytes(17);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "MOV":
                                b = BitConverter.GetBytes(7);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "LDR":
                                b = BitConverter.GetBytes(10);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "LDB":
                                b = BitConverter.GetBytes(12);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "TRP":
                                b = BitConverter.GetBytes(21);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "JMP":
                                b = BitConverter.GetBytes(1);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "PC":
                                b = BitConverter.GetBytes(16);
                                isOp = true;
                                list.AddRange(b);
                                continue;
                            case "JMR":
                                b = BitConverter.GetBytes(2);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "BNZ":
                                b = BitConverter.GetBytes(3);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "BGT":
                                b = BitConverter.GetBytes(4);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "BLT":
                                b = BitConverter.GetBytes(5);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "BRZ":
                                b = BitConverter.GetBytes(6);
                                isOp = true;
                                list.AddRange(b);
                                continue;
                            case "ADI":
                                b = BitConverter.GetBytes(14);
                                isOp = true;
                                list.AddRange(b);
                                continue;
                            case "LDA":
                                b = BitConverter.GetBytes(8);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "STR":
                                b = BitConverter.GetBytes(9);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "STB":
                                b = BitConverter.GetBytes(11);
                                isOp = true;
                                list.AddRange(b);
                                continue;

                            case "CMP":
                                b = BitConverter.GetBytes(20);
                                isOp = true;
                                list.AddRange(b);
                                continue;
                        }
                        //Gets immediate value. Since first is always a register, when this gets hit, it will always be an immediete.
                        if (i.Contains("ADI"))
                        {
                            int.TryParse(s, out int t3);
                            b = BitConverter.GetBytes(t3);
                            list.AddRange(b);
                            continue;
                        }

                        if (isOp && i.Contains("TRP") &&  (s == "3" || s == "1" || s == "0" ||s == "4" || s == "99" || s == "2"))
                        {
                            switch (s)
                            {
                                case "3":
                                    b = BitConverter.GetBytes(3);
                                    break;
                                case "2":
                                    b = BitConverter.GetBytes(2);
                                    break;
                                case "1":
                                    b = BitConverter.GetBytes(1);
                                    break;
                                case "99":
                                    b = BitConverter.GetBytes(99);
                                    break;
                                case "4":
                                    b = BitConverter.GetBytes(4);
                                    break;
                                default:
                                    b = BitConverter.GetBytes(0);
                                    break;
                            }
                            //if (s == "3")
                            //{
                            //    b = BitConverter.GetBytes(3);
                            //}
                            //if (s == "2")
                            //{
                            //    b = BitConverter.GetBytes(2);
                            //}
                            //else if (s == "1")
                            //{
                            //    b = BitConverter.GetBytes(1);
                            //}
                            //else if (s == "99")
                            //{
                            //    b = BitConverter.GetBytes(99);
                            //}
                            //else if (s == "4")
                            //{
                            //    b = BitConverter.GetBytes(4);
                            //}
                            //else
                            //{
                            //    b = BitConverter.GetBytes(0);
                            //}

                            list.AddRange(b);
                            b = BitConverter.GetBytes(0);
                            list.AddRange(b);
                        }

                        //This will account for all characters such as -, ',', Space, Newline, etc.
                        if (!isOp && !Ltable.Contains(s) && s != ".INT" && s != ".BYT" && i.Contains(".BYT"))
                        {
                            
                            var x = int.TryParse(s, out int t1);
                            
                            if (t1 == 48)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            //This accounts for empty bytes.
                            if (t1 == 49)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 50)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 51)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 52)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 53)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 54)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 55)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 56)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 57)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 32)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            //This accounts for empty bytes.
                            if (t1 == 0 && i.Contains("0"))
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 43)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 64)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 3)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 61)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 60)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 62)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 45)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                            if (t1 == 44)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }

                            if (t1 == 10)
                            {
                                b = BitConverter.GetBytes(t1);
                                var w = b[0];
                                list.Add(w);
                                break;
                            }
                        }

                        if (Ltable.Contains(s) && isOp)
                        {
                            //if (s == "COM")
                            //{
                            //    var o = Ltable["COM"];
                            //    var op = (int)o;
                            //    b = BitConverter.GetBytes(op);
                            //    var w = b[0];
                            //    list.Add(w);
                            //    list.Add(0);
                            //    list.Add(0);
                            //    list.Add(0);
                            //    break;
                            //}
                            if (s == "SPA")
                            {
                                var o = Ltable["SPA"];
                                var op = (int)o;
                                b = BitConverter.GetBytes(op);
                                var w = b[0];
                                list.Add(w);
                                list.Add(0);
                                list.Add(0);
                                list.Add(0);
                                break;
                            }
                            if (s == "NL")
                            {
                                var o = Ltable["NL"];
                                var op = (int)o;
                                b = BitConverter.GetBytes(op);
                                var w = b[0];
                                list.Add(w);
                                list.Add(0);
                                list.Add(0);
                                list.Add(0);
                                break;
                            }
                        }

                       
                        //This is a value from the directives
                        if (isOp == false && !Enum.IsDefined(typeof(Labels), s) && !Ltable.Contains(s) && int.TryParse(s, out int t)) // These last two are too account for the trap 1 and 3
                        {
                            b = BitConverter.GetBytes(t);
                            list.AddRange(b);
                            break;
                        }
                        //This converts the char to the ascii value.
                        if (isOp == false && !Enum.IsDefined(typeof(Labels), s) && !Ltable.Contains(s) && !int.TryParse(s, out int u))
                        {
                            bool isspecial = false;
                            foreach (var c in s)
                            {
                                if (c == '\\')
                                {
                                    isspecial = true;
                                    continue;
                                }
                                if (isspecial)
                                {
                                    switch (c)
                                    {
                                        case 'n':
                                            var ascii = BitConverter.GetBytes(10)[0];
                                            list.Add(ascii);
                                            break;
                                        case 't':
                                            var ascii1 = BitConverter.GetBytes(9)[0];
                                            list.Add(ascii1);
                                            break;
                                        case 'r':
                                            var ascii2 = BitConverter.GetBytes(13)[0];
                                            list.Add(ascii2);
                                            break;
                                    }
                                    break;
                                }
                                if ((char.IsLetter(c) || char.IsSymbol(c) || char.IsNumber(c) || char.IsPunctuation(c)) && c != '\'')
                                {
                                    var ascii = ((int)c);
                                    var Ascii = BitConverter.GetBytes(ascii)[0];
                                    list.Add(Ascii);
                                    break;
                                }
                            }
                        }
                        //ADD, SUB, DIV, MUL, LDR, TRP, LDB, MOV, START, EVEN, ENDWHILE, ENDIF, BCMP, LT, GT,EQ,FINISH,print,ps,eps,reset,for0,ef0,getdata,eps2,isroom,gdendif,opd,ELSE1, ELSE2, ELSE3, ELSE4, ELSE5, ELSE6, ELSE7, ELSE8, ELSE9,
                        // ELSEF,ps1,eps1,SPLUSELSE,ENDSPLUS,ENDIFFLAG,flush,flw1,whi1,if2,el1,eif1,whi3,ewh3,prf,eprf,els2,ewh2,ewh1,nodbz,UNDERFLOW,OVERFLOW
                        var enums = Enum.GetNames(typeof(Labels));
                                    isnot = enums.Any(mystr => i.StartsWith("$") || ((i.StartsWith(mystr) && (Enum.GetNames(typeof(Labels)).Contains(s)))));

                        if (isnot)
                        {
                            continue;
                        }

                        
                        //Checks if the operand is a label, if
                        if (Ltable.Contains(s) && isOp)
                        {
                            Regex myrRegex = new Regex("[a-zA-z]");
                            var p = Ltable[s];
                            var ptoadd = (int)p;
                            var offset = BitConverter.GetBytes(ptoadd);
                            list.AddRange(offset);
                            if (i.Contains("JMP") || i.Contains("JMR"))
                            {
                                list.AddRange(BitConverter.GetBytes(0));
                            }
                        }
                    }
                }

               
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void FirstPass(string file)
        {
            bool Labels = true;
            foreach (var i in File.ReadLines(file))
            {
                foreach (var s in i.Split())
                {
                    if (s == "")
                    {
                        continue;
                    }
                    if ((s == "ADD" || s == "SUB" || s == "DIV" || s == "MUL" || s == "LDR" || s == "TRP" || s == "MOV" || s == "LDB" || s == "JMP" || s == "STR") && (Labels == true))
                    {
                        PC = address;
                        list.AddRange(BitConverter.GetBytes(PC)); //First four bytes needs to be PC
                        Labels = false;
                        address += 12;
                        break;
                    }
                    if (s == ".INT" || s == ".BYT" || s == "ADD" || s == "SUB" || s == "DIV" || s == "MUL" || s == "LDR" || s == "TRP" || s == "MOV" || s == "LDB" || s == "ADI" || s == "JMP"
                        || s == "JMR" || s == "BNZ" || s == "BGT" || s == "BLT" || s == "BRZ" || s == "LDA" || s == "STR" || s == "STB" || s == "CMP" || s == "ADI" || s == "OR" || s == "AND")
                    {
                        switch (s)
                        {
                            case ".INT":
                                address += 4;
                                break;

                            case ".BYT":
                                address += 1;
                                break;

                            default:
                                address += 12;
                                break;
                        }

                        break;
                    }

                    if (s.Contains(";"))
                    {
                        break;
                    }
                    if (s == ";")
                    {
                        break;
                    }
                    Ltable.Add(s, address);
                }
            }
        }
    }
}