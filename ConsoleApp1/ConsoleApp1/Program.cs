using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;

// from https://github.com/jmp75/rdotnet-onboarding

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            REngine.SetEnvironmentVariables(@"c:\Program Files\Microsoft\R Client\R_SERVER\bin\x64\", @"c:\Program Files\Microsoft\R Client\R_SERVER\"); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();


            // A somewhat contrived but customary Hello World:
            //CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
            //engine.SetSymbol("greetings", charVec);
            //engine.Evaluate("str(greetings)"); // print out in the console
            //string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
            //Console.WriteLine("R answered: '{0}'", a[0]);
            //Console.WriteLine("Press any key to exit the program");
            //Console.ReadKey();

            // .NET Framework array to R vector.
            NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
            engine.SetSymbol("group1", group1);
            // Direct parsing from R script.
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            // Test difference of mean and get the P-value.
            GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            double p = testResult["p.value"].AsNumeric().First();

            Console.WriteLine("Group1: [{0}]", string.Join(", ", group1));
            Console.WriteLine("Group2: [{0}]", string.Join(", ", group2));
            Console.WriteLine("P-value = {0:0.000}", p);
            Console.ReadKey();
            engine.Dispose();
        }
    }
}
