using System;

namespace RefactorInlämmning
{
    class InputOutput
    {
        static string idString = "ID: ";
        static string nameString = "Name: ";
        static string rowString = "Row: ";
        static string colString = "Col: ";
        public static void ShowCreateNewShelf()
        {
            Console.Clear();
            Console.Write(String.Format("{3} \n{0} \n {1} \n {2} ", nameString, rowString, colString, idString));

            Console.SetCursorPosition(idString.Length + 2, 0);
            int shelfId = InputOutput.TakeInputNumber();
            if (shelfId == -1) return;
            
            Console.SetCursorPosition(nameString.Length + 2, 1);
            string shelfName = Console.ReadLine();

            Console.SetCursorPosition(rowString.Length + 2, 2);
            int shelfRow = InputOutput.TakeInputNumber();
            if (shelfRow == -1) return;

            Console.SetCursorPosition(colString.Length + 2, 3);
            int shelfCol = InputOutput.TakeInputNumber();
            if (shelfCol == -1) return;

            for(int i = 0; i < Engine.shelfHolder.GetLength(0); i++)
            {
                if(Engine.shelfHolder[i] == null)
                {
                    continue;
                }
                if(Engine.shelfHolder[i].id == shelfId)
                {
                    Console.Clear();
                    Console.WriteLine("A shelf already exists with that ID");
                    Console.ReadKey();
                    return;
                }

            }

            /*if (shelfRow == -1 || shelfCol == -1)
            {
                Console.WriteLine("Please Try Again");
                Console.ReadKey();
               
            }
            
            else
            {*/
          
                Shelf newShelf = new Shelf(shelfName, shelfCol, shelfRow, shelfId);
                Console.Clear();
                Console.WriteLine("Shelf successfully created");
                Console.ReadKey();
            //}

        }
        
        public static void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("1. Create shelf\n2. Print shelf\n3. Place package\n4. Fetch package\n5. Exit");
        }
        public static ConsoleKey TakeInput()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            return input.Key;


        }
        public static int TakeInputNumber()
        {
            string inputString = Console.ReadLine();

            int tempHolder;

            try
            {
                tempHolder = int.Parse(inputString);
                return tempHolder;
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine($"There was an error parsing your numerical input: {inputString} \n" + e.Message);
                Console.ReadKey();
                return -1;
            }
            
        }
       
    }
}
