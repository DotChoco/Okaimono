
namespace Okaimono.src
{
    public class DotRender
    {
        Backend backend = new();
        public void Awake()
        {
            Console.Title = "Okaimono";
            //backend.Start();

            DotTesting();
        }


        void Dot_Menu(string[] Options, Func<bool>[] Funcs, ConsoleColor[] Colors)
        {
            MainLoop(Options, Funcs, Colors);
        }


        void MainLoop(string[] Options, Func<bool>[] Option_Funcs, ConsoleColor[] CMD_Colors)
        {
            ConsoleKey key;
            int counter = 0;
            Console.CursorVisible = false;

            Rendering(counter, Options, CMD_Colors);
            while (true)
            {
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                {
                    Rendering(counter = counter > 0 ? counter - 1 : 0, Options, CMD_Colors);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    counter = counter < Options.Length - 2 ? counter + 1 : Options.Length - 1;
                    Rendering(counter, Options, CMD_Colors);
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Option_Funcs[counter]();
                    Task.Delay(2000).Wait();
                    Rendering(counter, Options, CMD_Colors);
                }
                else if((key == ConsoleKey.Escape) || (key == ConsoleKey.Q)) { 
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }

        }


        void Rendering(int counter_render, string[] Options_Render, ConsoleColor[] CMD_Colors)
        {
            Console.Clear();
            for (int i = 0; i < Options_Render.Length; i++)
            {
                if (i == counter_render)
                {
                    Console.ForegroundColor = CMD_Colors[0];
                    Console.WriteLine($"-> {Options_Render[i]}");
                }
                else
                {
                    Console.ForegroundColor = CMD_Colors[1];
                    Console.WriteLine($"* {Options_Render[i]}");
                }
            }

        }


        void Presentation()
        {
            Console.WriteLine(@"  ____  __        _                    " + "\n" +
                              @" / __ \/ /_____ _(_)_ _  ___  ___  ___" + "\n" +
                              @"/ /_/ /  '_/ _ `/ /  ' \/ _ \/ _ \/ _ \" + "\n" +
                              @"\____/_/\_\\_,_/_/_/_/_/\___/_//_/\___/");
            Console.WriteLine("\n\n\n");
            //if (backend.GetPDoc)
            //{
            //    backend.PrintDoc();
            //    Presentation();
            //}
            //else
            //{
            //    backend.PrintAnimesForToday();
            //}
            //Console.WriteLine("\n\n");

        }


        void DotTesting()
        {
            //User_Testing
            Func<bool> func = () =>
            {
                Console.WriteLine("Washoiii");
                Task.Delay(500).Wait();
                return default;
            };
            Func<bool> funco = () =>
            {
                Console.WriteLine("No Wonder Stage");
                Task.Delay(500).Wait();
                return default;
            };


            Func<bool> func2 = () =>
            {
                Presentation();
                return default;
            };

            Func<bool>[] funcs = new Func<bool>[3];
            funcs[0] = func;
            funcs[1] = funco;
            funcs[2] = func2;
            ConsoleColor[] colors = new ConsoleColor[2] { ConsoleColor.White, ConsoleColor.DarkMagenta };
            Dot_Menu(new string[] { "Yokoso", "Kira kira", "Doki doki", "Mochi mochi" },
                                   funcs, colors);
        }
    }
}
