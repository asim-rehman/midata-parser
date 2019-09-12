using System;

namespace MiData.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ============= MiData Statement Reader ============= ");
            try
            {
                Run();
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("Permission denied to directory, please try running as administrator." + uae.Message);
                Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Run();
            }
            finally
            {
                Environment.Exit(0);
            }
            Console.ReadLine();
        }

        static void Run()
        {
            while (true)
            {
                Console.Write("> ");
                Command command = new Command();
                command.Execute(Console.ReadLine());
            }
        }
    }

}

