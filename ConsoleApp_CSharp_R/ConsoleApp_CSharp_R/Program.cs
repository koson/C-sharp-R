using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using RDotNet;
using RDotNet.NativeLibrary;

namespace RCalculator
{
    class Program
    {
        public static REngine _engine;
        static void Main(string[] args)
        {
            MyFunctions.InitializeRDotNet();
        }
      
    }

    public static class MyFunctions
    {
        public static Random rnd = new Random();

        public static REngine _engine;

        public static void InitializeRDotNet()
        {
            try
            {
                //  Check for Path and R_Home
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\R-core\R"))
                {
                    string sEnvPath = Environment.GetEnvironmentVariable("Path");
                    string sRInstallPath = (string)registryKey.GetValue("InstallPath");
                    string sRInstallVersion = (string)registryKey.GetValue("Current Version");


                    //string rHome = @"C:\Program Files\R\R-3.4.3";
                    //string rPath = @"C:\Program Files\R\R-3.4.3\bin\x64";
                    ////string rPath = Path.Combine(rHome, @"bin\x64");
                    //REngine.SetEnvironmentVariables(rPath, rHome);
                    //_engine = REngine.GetInstance();

                    var logInfo = RDotNet.NativeLibrary.NativeUtility.GetRHomeEnvironmentVariable();
                    var logInfo2 = RDotNet.NativeLibrary.NativeUtility.GetPlatform();
                    var rLib = RDotNet.NativeLibrary.NativeUtility.GetRLibraryFileName();


                    REngine.SetEnvironmentVariables();
                    _engine = REngine.GetInstance();
                    _engine.Initialize();

                    //Console.WriteLine("R_values: " + _engine.P);

                    //foreach (string sPackages in MyFunctions._engine.Evaluate("installed.packages(.Library)").AsCharacter())
                    //{
                    //    Console.WriteLine("Installed packages on local machine are: " + sPackages);
                    //}

                    // Check to see which instance of R is installed
                    if (!string.IsNullOrEmpty(sRInstallPath))
                    {
                        string sRBinPath = System.Environment.Is64BitProcess ? sRInstallPath + @"bin\x64\" : sRInstallPath + @"bin\i386\";
                        Environment.SetEnvironmentVariable("Path", sEnvPath + sRInstallPath);
                        //Console.WriteLine("Env Path: " + sOrigPath + "\r\n" + "R Bin Path: " + sBinPath + "\r\n" + "R Version: " + sVersion);
                        string sNewPath = Environment.GetEnvironmentVariable("Path");
                        //var logInfo = RDotNet.NativeLibrary.NativeUtility.GetRHomeEnvironmentVariable();
                        //var rLib = RDotNet.NativeLibrary.NativeUtility.GetRLibraryFileName();
                        Console.WriteLine("Original Env Path: " + sEnvPath);
                        Console.WriteLine("     New Env Path: " + sNewPath);


                        REngine.SetEnvironmentVariables(sRBinPath, sRInstallPath);  // Leave these out for system default.
                        _engine = REngine.GetInstance();
                        _engine.Initialize();
                    }
                    else
                    {

                        Console.WriteLine("You do not have the R software installed.  Please install version 3.43.");
                     
                    }
                    //Console.WriteLine("R Home: " + logInfo + "\r\n");
                    //Console.WriteLine("R Library: " + rLib + "\r\n");
                    //foreach (string sPackages in MyFunctions._engine.Evaluate("installed.packages(.Library)").AsCharacter())
                    //    Console.WriteLine("Installed packages on local machine are: " + sPackages);


                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error Initializing RDotNet: " + ex.Message);
            }
        }
    }
}
