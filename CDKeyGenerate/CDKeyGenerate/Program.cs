using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKeyGenerate
{
    class Program
    {
        private int[] digitList;
        private String randomList;
        
        /* Creates 29 digits code */
        public void GenerateList()
        {
           
            digitList = new int[29]; // list is an array of 29 integers

            Random random = new Random();
         
            int index = 0;
            while(index < 29)
            {
                digitList[index] = random.Next(0, 9);
              
                index++;

            }
            randomList = string.Join("", digitList);
          
        }
        

        /* Divides code line into 5 digits segments */
        public String segments()
        {
            GenerateList();


            var builder = new StringBuilder();
            int count = 0;
            foreach (var segment in randomList)
            {
                builder.Append(segment);
                if ((++count % 5) == 0)
                {
                    builder.Append('-');
                }
            }
            randomList = builder.ToString();
            Console.WriteLine(randomList);
            return randomList;
        }

        /*Clears inputs */
        public void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }




    class ExecuteProgram
    {

       

        static void Main(string[] args)
        {
            String generatedCode = "";
            Program p = new Program();

            /* Menu */ 
            Console.WriteLine(" ");
            Console.WriteLine("===========================================================");
            Console.WriteLine("===   Welcome in CD-key generator.                     === ");
            Console.WriteLine("===========================================================");
            Console.WriteLine("===  Press 'G' to generate CD-key.                     === ");
            Console.WriteLine("===  Press 'S' to save CD-key to file.                 === ");
            Console.WriteLine("===  Press 'O' to open CD-keys file.                   === ");
            Console.WriteLine("===  Press 'D' to delete last added CD-key from file.  === ");
            Console.WriteLine("===  Press 'Esc' to quit CD-key generator.             === ");
            Console.WriteLine("===========================================================");

            ConsoleKeyInfo cki;

            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;
                        
            do
            {
                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.G)
                {
                    p.ClearLine();
                    Console.WriteLine("Your a new code is: ");
                    
                    generatedCode = p.segments();
                    
                }
                else if (cki.Key == ConsoleKey.S) 
                {
                    p.ClearLine();
                    
                    string path = @"C:\Users\Pawel\Documents\GitHub\CiSharp\CDKeyGenerate\genCode.txt";

                    // Creates file genCode.txt if it is not created yet, and saves code if there is some to save, if not then leave empty file.
                    if (!File.Exists(path))
                    {
                        File.Create(path).Dispose();
                        using (TextWriter tw = new StreamWriter(path))
                        {
                            if (generatedCode != "")
                            {
                                tw.WriteLine(generatedCode);
                                Console.WriteLine("File created and saved!");
                            }
                            else
                            {
                                tw.Close();
                                Console.WriteLine("File created!");
                            }
                        }
                    }
                    // Goes to genCode.txt and saves changes.
                    else if (File.Exists(path))
                    {
                        using (var tw = new StreamWriter(path, true))
                        {
                            if (generatedCode != "")
                            {
                                tw.WriteLine(generatedCode);
                                Console.WriteLine("File changes saved!");
                            }
                            else
                            {
                                tw.Close();
                            }
                        }
                    }
                }
                else if (cki.Key == ConsoleKey.O) // open a genCode.txt file
                {
                    p.ClearLine();
                    Console.WriteLine("genCode is Open");

                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents=false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.FileName = "notepad";

                    proc.StartInfo.Arguments = @"""C:\Users\Pawel\Documents\GitHub\CiSharp\CDKeyGenerate\genCode.txt""";
                    proc.Start();
                   // proc.WaitForExit();

                }
                else if (cki.Key == ConsoleKey.D) // delete last added line from genCode.txt file 
                {
                    p.ClearLine();
                    Console.WriteLine("delete last line from genCode.txt file. ");

                    string pathToFile = @"C:\Users\Pawel\Documents\GitHub\CiSharp\CDKeyGenerate\genCode.txt";

                    if (File.Exists(pathToFile))
                    {
                        var lines = File.ReadAllLines(pathToFile);
                        File.WriteAllLines(pathToFile, lines.Take(lines.Length - 1));
                        p.ClearLine();
                        Console.WriteLine("last line from genCode.txt file has been DELETED! ");
                    }
                    else
                    {
                        Console.Write("There is no file. Create file and add some content first!");
                    }
                }
            } while (cki.Key != ConsoleKey.Escape);
            

        }
    }
}

