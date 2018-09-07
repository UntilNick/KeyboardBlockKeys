namespace BlockKeyboardKeys
{
    using System;
    using System.Windows.Forms;

    internal partial class Program
    {
        private static void Main()
        {
            Console.Title = "Keyboard Block Keys 2018 || by Antlion";
            using (var shh = new KeyBlockHook())
            {
                shh.InstallHook();
                Application.Run();
            }
        }
    }
}