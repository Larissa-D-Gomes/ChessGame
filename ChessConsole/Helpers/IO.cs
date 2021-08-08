using Chess;
using GameBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Helpers
{
    public static class IO
    {

        public static string GetString(string prompt) { Console.Out.Write(prompt); return Console.ReadLine(); }
        public static int GetInt(string prompt, int min, int max) { int temp; while (!int.TryParse(GetString(prompt), out temp) || temp < min || temp > max) ; return temp; }
        public static float GetFloat(string prompt, float min, float max) { float temp; while (!float.TryParse(GetString(prompt), out temp) || temp < min || temp > max) ; return temp; }
        public static bool GetBool(string prompt) { bool temp; while (!bool.TryParse(GetString(prompt), out temp)) ; return temp; }
        public static char GetChar(string prompt) { char temp; while (!char.TryParse(GetString(prompt), out temp)) ; return temp; }
        public static int GetMenuItem(string[] menu, bool allowExit = false)
        {
            var length = menu.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i + 1}) {menu[i]}");
            }
            if (allowExit) { Console.WriteLine("\n0) Exit\n"); };
            return GetInt("Enter Selection: ", allowExit? 0:1, length); 
        }
        public static void SetError(string errorMessage, string resolveMessage)
        {
            ConsoleColor current = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR:\n    { errorMessage}");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(resolveMessage);

            Console.ReadKey();
            Console.ForegroundColor = current;
        }
        public static ChessPosition GetMove(bool From = true)
        {
            Console.Write(From ? "From: " : "To: ");
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse("" + s[1]);
            return new ChessPosition(column, row);
        }

    }
}
