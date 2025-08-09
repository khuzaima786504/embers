﻿using Embers.Compiler;
using Embers.Expressions;
using Crayon;
using Embers.Host;

namespace Embers.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var has_executes = false;

            Machine machine = new();

            machine.InjectFromCallingAssembly();

            foreach (var arg in args)
            {
                has_executes = true;
                machine.ExecuteFile(arg);
                System.Console.WriteLine(Output.Green(string.Format("Loaded {0}", Output.Cyan(arg))));
            }

            if (has_executes) Interative(machine);

            if (args.Length == 0)
            {
                Welcome.Print();

                Interative(machine);
            }
        }

        public static void Interative(Machine machine)
        {
            while (true)
            {
                try
                {
                    System.Console.Write(Output.Magenta("> "));
                    string input = System.Console.ReadLine();

                    // Apply preprocessing to ensure expression is parsable
                    string prepared = PrepareInput(input);

                    Parser parser = new(prepared);
                    IExpression expr = parser.ParseCommand();

                    var result = expr.Evaluate(machine.RootContext);
                    var text = (result == null ? "nil" : result.ToString());
                    System.Console.WriteLine(Output.Yellow($"=> {Output.Cyan(text)}"));
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(Output.Red().Underline(ex.Message));
                    System.Console.WriteLine(Output.Red(ex.StackTrace));
                }
                finally
                {
                    System.Console.WriteLine();
                }
            }
            //Parser parser = new(System.Console.In);

            //while (true)
            //    try
            //    {
            //        IExpression expr = parser.ParseCommand();
            //        var result = expr.Evaluate(machine.RootContext);
            //        var text = (result == null ? "nil" : result.ToString());
            //        System.Console.WriteLine(Output.Yellow(string.Format("=> {0}", Output.Cyan(text))));
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Console.WriteLine(Output.Red().Underline(ex.Message));
            //        System.Console.WriteLine(Output.Red(ex.StackTrace));
            //    }
            //    finally
            //    {
            //        System.Console.WriteLine(" ");
            //        System.Console.Write(Output.Magenta("> "));
            //    }
        }

        private static string PrepareInput(string input)
        {
            string trimmed = input.TrimEnd();

            // Already terminated by user
            if (trimmed.EndsWith("end") || trimmed.EndsWith("}") || trimmed.EndsWith(";"))
                return input + "\n";

            // One-liner block starters
            if (trimmed.StartsWith("if") || trimmed.StartsWith("unless") ||
                trimmed.StartsWith("while") || trimmed.StartsWith("until") ||
                trimmed.StartsWith("for") || trimmed.StartsWith("begin"))
                return input + "\nend";

            return input + "\n";
        }

    }
}
