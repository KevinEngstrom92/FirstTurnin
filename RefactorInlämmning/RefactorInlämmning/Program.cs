using System;

namespace RefactorInlämmning
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                InputOutput.ShowMainMenu();
                Engine.HandleMainMenuInput(InputOutput.TakeInput());
            }
        }
    }
}
