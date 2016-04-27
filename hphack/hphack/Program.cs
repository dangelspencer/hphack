namespace hphack
{
    using System;
    using System.Text;
    using System.Net;
    using System.Net.Sockets;

    public class PrnHack
    {
        public static int Main(string[] args)
        {
            if (!ParseArgs(args))
            {
                return -1;
            }

            Console.WriteLine("\nHP Display Hack");
            Console.WriteLine("Host: {0}", args[0]);
            Console.WriteLine("Message: {0}\n", message);

            IPEndPoint ipEndPoint;
            ipEndPoint = new IPEndPoint(Dns.Resolve(args[0]).AddressList[0], PJL_PORT);

            //Console.WriteLine("Host is {0}", ipEndPoint.ToString());

            Socket socket;
            socket = new Socket(
                              AddressFamily.InterNetwork,
                              SocketType.Stream,
                              ProtocolType.Tcp
                           );

            socket.Connect(ipEndPoint);

            byte[] sendData;
            string sendString;

            sendString = String.Format(
                      "\x1B%-12345X@PJL RDYMSG DISPLAY = \"{0}\"\r\n\x1B%-12345X\r\n",
                      message
                 );

            sendData = Encoding.ASCII.GetBytes(sendString);

            int result;
            result = socket.Send(sendData, sendData.Length, 0);

            if (result == 0)
            {
                Console.WriteLine("Could not send on socket");
            }

            socket.Close();

            Console.WriteLine("Finished\n\n");
            return 0;
        }



        protected static bool ParseArgs(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(
                          "HP Display Hack: " +
                          "hphack printername \"message\" "
                    );
                return false;
            }

            if (args[1].Length > 16)
            {
                Console.WriteLine("Message must be <= 16 characters");
                return false;
            }

            if (args[1].CompareTo("random") == 0)
            {
                message = GetRandomMessage();
            }
            else
            {
                message = args[1];
            }

            return true;
        }


        public static string GetRandomMessage()
        {
            string[] Messages = {
                             "NOOOOOOOOO",
                             "SCORE = 3413",
                             "PAT EATS MICE",
                             "FEED ME",
                             "NEED MORE SPACE",
                             "POUR ME A DRINK",
                             "IN DISTRESS",
                             "NICE SHIRT",
                             "READY",
                             "NO PRINT FOR YOU",
                             "RADIATION LEAK",
                             "PRESS MY BUTTON",
                             "TAKE ME HOME",
                             "LOOKS LIKE RAIN",
                             "+1-513-907-9391",
                             "HELLO WORLD",
                             "NICE HAIR",
                             "NEED A MINT?",
                             "BE GENTLE",
                             "BE KIND",
                             "INSERT DISK",
                             "BUY ME LUNCH",
                             "DONT STOP",
                             "COME CLOSER",
                             "TAKE A BREAK",
                             "INSERT QUARTER",
                             "FEED ME CATS",
                             "FEED ME DOGS",
                             "CALL ME",
                             "BRRAAIINNNSSSS",
                             "READY",
                             "PRINT MORE",
                             "SUNNY TODAY",
                             "I'LL BE BACK",
                             "BBQ SAUCE LOW",
                             "SAFTEY GLASSES",
                             "STORMTROOPERS!",
                             "THAT'S NO MOON",
                             "TAKE IT EASY",
                             "FOR FRODO",
                             "NEVER LOOK BACK",
                             "ALL IS WELL",
                             "BE YOURSELF",
                             "BE YOUR BEST",
                             "DO YOUR BEST",
                             "LAUGH OUT LOUD",
                             "LIVE YOUR LIFE",
                             "MAKE PEOPLE GRIN",
                             "CARPE DIEM",
                             "SIEZE THE DAY",
                             "YES YOU CAN",
                             "DO AMAZING THINGS",
                             "HAIL SKYNET",
                             "IS AGILE UP?",
                             ""

            };


            Random r = new Random();
            return Messages[r.Next() % Messages.Length];
        }

        protected const int PJL_PORT = 9100;
        protected static string message = "NO MESSAGE";

    }
}
