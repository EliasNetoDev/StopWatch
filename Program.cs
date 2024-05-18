using System.Timers;

namespace StopWatch
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        private static int currentTime;
        private static int targetTime;

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("S = Segundo => 10s = 10 segundos");
            Console.WriteLine("M = Minuto = 1m = 1 minuto");
            Console.WriteLine("0 = Sair do StopWatch");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Quanto tempo deseja contar?");

            string data = Console.ReadLine().ToLower();
            if (data == "0")
            {
                System.Environment.Exit(0);
            }

            char type = char.Parse(data.Substring(data.Length - 1, 1));
            int time = int.Parse(data.Substring(0, data.Length - 1));
            int multiplier = (type == 'm') ? 60 : 1;

            targetTime = time * multiplier;

            PreStart(targetTime);
        }

        static void PreStart(int time)
        {
            Console.Clear();
            Console.WriteLine("Ready...");
            Thread.Sleep(1000);
            Console.WriteLine("Set...");
            Thread.Sleep(1000);
            Console.WriteLine("GO!");
            Thread.Sleep(2500);

            Start(time);
        }

        static void Start(int time)
        {
            currentTime = 0;
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            Thread inputThread = new Thread(CheckForExit);
            inputThread.Start();

        }

        private static void CheckForExit()
        {
            Console.WriteLine("Pressione 'q' para sair do cronômetro.");
            while (Console.Read() != 'q') ;
            aTimer.Stop();
            aTimer.Dispose();
            Menu();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            currentTime++;
            Console.Clear();
            Console.WriteLine(currentTime);

            if (currentTime >= targetTime)
            {
                aTimer.Stop();
                Console.Clear();
                Console.WriteLine("StopWatch Finalizado.");
                Thread.Sleep(2000);
                Menu();
            }
        }
    }
}
