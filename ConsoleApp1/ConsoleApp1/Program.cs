using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            REngine.SetEnvironmentVariables(@"c:\Program Files\Microsoft\R Client\R_SERVER\bin\x64\", @"c:\Program Files\Microsoft\R Client\R_SERVER\"); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();
            // A somewhat contrived but customary Hello World:
            CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
            engine.SetSymbol("greetings", charVec);
            engine.Evaluate("str(greetings)"); // print out in the console
            string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
            Console.WriteLine("R answered: '{0}'", a[0]);
            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            engine.Dispose();
        }
    }
}
