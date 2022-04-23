using System;

namespace _4380_Project
{
    internal class MainTest
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Please input your assembly file");
            var argument = args[0];
            Virtual_Machine vm = new Virtual_Machine(argument);
        }
    }
}