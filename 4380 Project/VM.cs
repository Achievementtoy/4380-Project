using System;
using System.Globalization;

namespace _4380_Project
{
    public class Virtual_Machine
    {
        public Assembler assembler;

        public Virtual_Machine(string filename)
        {
            Console.WriteLine("Now loading the Assembler for project 3");
            assembler = new Assembler(filename);
            Console.WriteLine("Assembler has finished, running virtual machine");

            //1397178692
            //Console.WriteLine(Convert.ToInt32("DAGS"));
            int[] registers = new int[21];

            registers[19] = 1000000;
            registers[20] = 0;
            registers[18] = 1000000;
            registers[17] = 500000;
            var ProgramCounter = Assembler.PC;
            bool running = true;
            registers[16] = ProgramCounter;
            try
            {
                while (running)
                {
                    
                    //This code starts at ProgramCounter. It then grabs the next 4 bytes and parses, then the next 4, and the next 4. This makes the 12 byte instructions and parses it correctly according to the opcode.
                    var final = Assembler.list.GetRange(registers[16], 4);
                    var iropcode = BitConverter.ToInt32(final.ToArray());
                    var final1 = Assembler.list.GetRange(registers[16] + 4, 4);
                    var final2 = Assembler.list.GetRange(registers[16] + 8, 4);
                    var irop1 = BitConverter.ToInt32(final1.ToArray());
                    var irop2 = BitConverter.ToInt32(final2.ToArray());
                    switch (iropcode)
                    {
                        //LDR
                        case 10:
                            //Grabs 4 bytes from memory from the address of op2.
                            var ldr = Assembler.list.GetRange(irop2, 4);
                            var ld = BitConverter.ToInt32(ldr.ToArray());
                            registers[irop1] = ld;
                            registers[16] += 12;
                            break;
                        //LDB
                        case 12:
                            //Grabs one byte from memory where op2 says the address is.
                            registers[irop1] = Assembler.list[irop2];
                            registers[16] += 12;
                            break;
                        //TRP
                        case 21:
                            switch (irop1)
                            {
                                case 0:
                                    running = false;
                                    break;

                                case 1:
                                    Console.Write(registers[3]);
                                    break;
                                case 2:
                                    var t = Console.ReadLine();
                                    var myint = Convert.ToInt32(t);
                                    registers[3] = myint;
                                    break;
                                case 3:
                                    Console.Write((char)registers[3]);
                                    break;

                                case 4:
                                    int readin;
                                    var s = Console.Read();
                                    //var getridofwhitespace = Console.Read();
                                    //This needed modification to account for stop symbol. C# was not as easy in accounting for this.
                                    //while (getridofwhitespace != '\n')
                                    //{
                                    //    getridofwhitespace = Console.Read();
                                    //}
                                    //This could be simplified, but I don't care enough right now too. Just need this to work.
                                    if (s == '\r')
                                    {
                                        Console.Read(); //I have to do this since windows will use \r and \n. I need to eat up the \r for this to work so this is why the console.read is used twice if it encounters the carriage return.
                                        registers[3] = '\n';
                                        
                                    }

                                    else if (s == '@')
                                    {
                                        registers[3] = '@';
                                    }
                                    else if (s == '+')
                                    {
                                        registers[3] = '+';
                                    }
                                    else if (s == '-')
                                    {
                                        registers[3] = '-';
                                    }
                                    else
                                    {
                                        var i = s.ToString();
                                        int.TryParse(i, out readin);
                                        registers[3] = readin;
                                    }

                                    
                                    break;
                                case 99:
                                    break;
                            }

                            registers[16] += 12;
                            break;
                        //STRI
                        case 22:
                            var str1 = BitConverter.GetBytes(registers[irop1]);
                            Assembler.list[registers[irop2]] = str1[0];
                            Assembler.list[registers[irop2] + 1] = str1[1];
                            Assembler.list[registers[irop2] + 2] = str1[2];
                            Assembler.list[registers[irop2] + 3] = str1[3];
                            registers[16] += 12;
                            break;
                        //LDRI
                        case 23:
                            var ldr1 = Assembler.list.GetRange(registers[irop2], 4);
                            var ld1 = BitConverter.ToInt32(ldr1.ToArray());
                            registers[irop1] = ld1;
                            registers[16] += 12;
                            break;
                        //STBI
                        case 24:
                            Assembler.list[registers[irop2]] = (byte)registers[irop1];
                            registers[16] += 12;
                            break;
                        //LDBI
                        case 25:
                            registers[irop1] = Assembler.list[registers[irop2]];
                            registers[16] += 12;
                            break;
                        //ADI
                        case 14:
                            registers[irop1] += irop2;
                            registers[16] += 12;
                            break;
                        //ADD
                        case 13:
                            registers[irop1] += registers[irop2];
                            registers[16] += 12;
                            break;
                        //SUB
                        case 15:
                            registers[irop1] -= registers[irop2];
                            registers[16] += 12;
                            break;
                        //MUL
                        case 16:
                            registers[irop1] *= registers[irop2];
                            registers[16] += 12;
                            break;
                        //DIV
                        case 17:
                            registers[irop1] /= registers[irop2];
                            registers[16] += 12;
                            break;
                        //STR
                        case 9:
                            var str = BitConverter.GetBytes(registers[irop1]);
                            Assembler.list[irop2] = str[0];
                            Assembler.list[irop2 + 1] = str[1];
                            Assembler.list[irop2 + 2] = str[2];
                            Assembler.list[irop2 + 3] = str[3];
                            registers[16] += 12;
                            break;
                        //STB
                        case 11:
                            //Assembler.list[irop2] = Assembler.list[registers[irop1]];
                            Assembler.list[irop2] = (byte)registers[irop1];
                            registers[16] += 12;
                            break;
                        //JMP
                        case 1:
                            registers[16] = irop1;
                            break;
                        //JMR
                        case 2:

                            registers[16] = registers[irop1];
                            break;
                        //BNZ
                        case 3:
                            if (registers[irop1] != 0)
                            {
                                registers[16] = irop2;
                            }
                            else
                            {
                                registers[16] += 12;
                            }
                            break;
                        //BGT
                        case 4:
                            if (registers[irop1] > 0)
                            {
                                registers[16] = irop2;
                            }
                            else
                            {
                                registers[16] += 12;
                            }
                            break;
                        //BLT
                        case 5:
                            if (registers[irop1] < 0)
                            {
                                registers[16] = irop2;
                            }
                            else
                            {
                                registers[16] += 12;
                            }
                            break;
                        //BRZ
                        case 6:
                            if (registers[irop1] == 0)
                            {
                                registers[16] = irop2;
                            }
                            else
                            {
                                registers[16] += 12;
                            }
                            break;
                        //MOV
                        case 7:
                            registers[irop1] = registers[irop2];
                            registers[16] += 12;
                            break;
                        //AND
                        case 18:
                            if ((registers[irop1] != 0 && registers[irop1] > 0) && (registers[irop2] != 0 && registers[irop2] > 0))
                            {
                                registers[irop1] = 1;
                            }
                            else
                            {
                                registers[irop1] = 0;
                            }

                            registers[16] += 12;
                            break;
                        //OR
                        case 19:
                            if ((registers[irop1] != 0 && registers[irop1] > 0) || (registers[irop2] != 0 && registers[irop2] > 0))
                            {
                                registers[irop1] = 1;
                            }
                            else
                            {
                                registers[irop1] = 0;
                            }
                            registers[16] += 12;

                            break;
                        //LDA
                        case 8:
                            registers[irop1] = irop2;
                            registers[16] += 12;
                            break;
                        //CMP
                        case 20:
                            if (registers[irop1] == registers[irop2])
                            {
                                registers[irop1] = 0;
                            }
                            else if (registers[irop1] < registers[irop2])
                            {
                                registers[irop1] = -1;
                            }
                            else
                            {
                                registers[irop1] = 1;
                            }

                            registers[16] += 12;
                            break;
                    }

                    //Use trap 3 for char printing and trap 1 for printing ints
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                throw;
            }
        }
    }
}
//Switchsuccess = false
//switchoptions = 0
//default = true;