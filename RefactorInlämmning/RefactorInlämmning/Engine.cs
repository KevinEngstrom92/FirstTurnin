using System;

namespace RefactorInlämmning
{
    class Engine
    {
        public static int shelfCounter = 0;
        public static Shelf[] shelfHolder = new Shelf[0];
        public static Shelf[] MustNotUseListUseThis(ref Shelf[] shelf)
        {
            if(Engine.shelfCounter > shelf.Length - 2)
            {
                Shelf[] tempShelf = new Shelf[shelf.Length + 10];

                if(shelf.Length > 0)
                {
                    shelf.CopyTo(tempShelf, 0);
                    return tempShelf;
                }
                return tempShelf;
            }
            return shelf;
        }
    
        public static void HandleMainMenuInput(System.ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case System.ConsoleKey.D1:
                    InputOutput.ShowCreateNewShelf();
                    break;
                case System.ConsoleKey.D2:
                    Shelf.ListShelfs();
                    break;
                case System.ConsoleKey.D3:
                    Shelf.PlaceItem();
                    break;
                case System.ConsoleKey.D4:
                    Shelf.RecoverItem();
                    break;
                case System.ConsoleKey.D5:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}
