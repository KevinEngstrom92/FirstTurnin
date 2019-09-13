using System;
using System.Threading;

namespace RefactorInlämmning
{
    class Shelf
    {
        Box[,] box;
        public int id;
        string name;
        int row;
        int col;

        struct Box
        {
            public string desc;
        }
        public Shelf(string name, int row, int col, int id)
        {
            this.id = id;
            this.name = name;
            this.row = row;
            this.col = col;

            this.box = new Box[row, col];
            
            Engine.shelfHolder = Engine.MustNotUseListUseThis(ref Engine.shelfHolder);
            Engine.shelfHolder[Engine.shelfCounter] = this;

            Engine.shelfCounter += 1;
        }

        public static void PlaceItem()
        {
            Console.Clear();
            Console.WriteLine("Shelf ID:");
            Console.WriteLine("Row:");
            Console.WriteLine("Column:");
            Console.WriteLine("Content");
            Console.SetCursorPosition(11, 0);
            int shelfIdInput = InputOutput.TakeInputNumber();
            
            Console.SetCursorPosition(6, 1);
            int shelfRow = InputOutput.TakeInputNumber();
            
            Console.SetCursorPosition(9, 2);
            int shelfCol = InputOutput.TakeInputNumber();
            
            Console.SetCursorPosition(10, 3);
            string shelfContent = Console.ReadLine();

            for(int i = 0; i < Engine.shelfHolder.Length; i++)
            {
                if (Engine.shelfHolder[i] == null) continue;
                if(shelfIdInput == Engine.shelfHolder[i].id)
                {

                    if(shelfRow > Engine.shelfHolder[i].box.GetLength(0) || shelfCol > Engine.shelfHolder[i].box.GetLength(1))
                    {
                        Console.Clear();
                        Console.WriteLine($"You attempted to place your item *{shelfContent}* out of bounds");
                        Console.ReadKey();
                        return;
                    }
                    if(Engine.shelfHolder[i].box[shelfRow-1, shelfCol-1].desc == null)
                    {
                        Engine.shelfHolder[i].box[shelfRow-1, shelfCol-1].desc = shelfContent;
                        Console.Clear();
                        Console.WriteLine("Package placed");
                        Console.ReadKey();
                        return;
                        
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Slot occupied");
                        Console.ReadLine();
                        return;
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No Shelf was found with that ID");
                    Console.ReadLine();
                    return;
                }
            }

        }

        public static void RecoverItem()
        {
            Console.Clear();
            Console.WriteLine("Shelf ID:");
            Console.WriteLine("Row:");
            Console.WriteLine("Column:");

            Console.SetCursorPosition(11, 0);
            int shelfIdInput = InputOutput.TakeInputNumber();

            Console.SetCursorPosition(6, 1);
            int shelfRow = InputOutput.TakeInputNumber();

            Console.SetCursorPosition(9, 2);
            int shelfCol = InputOutput.TakeInputNumber();

            for (int i = 0; i < Engine.shelfHolder.Length; i++)
            {
                if (Engine.shelfHolder[i] == null) continue;

                if (shelfIdInput == Engine.shelfHolder[i].id)
                {

                    continue;
                }
                else
                {

                    Console.Clear();
                    Console.WriteLine("No Shelf was found with that ID");
                    Console.ReadLine();
                    return;
                }

            } 




                    for (int i = 0; i < Engine.shelfHolder.Length; i++)
            {
                if (Engine.shelfHolder[i] == null) return;
                if (shelfIdInput == Engine.shelfHolder[i].id)
                {
                    if (shelfRow >= Engine.shelfHolder[i].box.GetLength(0) + 1 || shelfCol >= Engine.shelfHolder[i].box.GetLength(1) + 1) 
                    {
                        Console.WriteLine("You tried to access a box out of bounds");
                        Console.WriteLine("<Press any key to continue>");
                        Console.ReadKey(true);
                        return;
                    }
                        if (Engine.shelfHolder[i].box[shelfRow - 1, shelfCol - 1].desc == null)
                    {
                        
                        Console.Clear();
                        Console.WriteLine("Slot empty");
                        Thread.Sleep(2000);

                        return;

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Package successfully retrieved\nContent: {Engine.shelfHolder[i].box[shelfRow - 1, shelfCol - 1].desc}");
                        Engine.shelfHolder[i].box[shelfRow - 1, shelfCol - 1].desc = null;
                        Console.WriteLine("<Press any key to continue>");
                        Console.ReadKey(true);
                    }
                }
            }
        }

        public static void ListShelfs()
        {
            Console.Clear();
            int shelfCount = Engine.shelfHolder.Length;
            int currentPage = 0;
            int counterSlots = 0;
            int availableSlot = 0;
            bool hasPressedEsc = false;

            //Count the pages
            for (int i = 0; i < shelfCount; i++)
            {
                if (Engine.shelfHolder[i] != null) counterSlots += 1;
                if (Engine.shelfHolder[i] == null) continue;
            }


            while (!hasPressedEsc)
            {
                availableSlot = 0;
                if (currentPage >= counterSlots) currentPage -= 1;
                if (currentPage <= -1) currentPage = 0;
                if(Engine.shelfCounter == 0)
                {
                    Console.Clear();
                    Console.WriteLine("There are no shelves.");
                    Console.ReadKey(true);
                    return;
                }
           
                for (int j = 0; j < Engine.shelfHolder[currentPage].box.GetLength(0); j++)
                {
                    Console.Write("|");
                    for (int k = 0; k < Engine.shelfHolder[currentPage].box.GetLength(1); k++)
                    {

                        
                        if (Engine.shelfHolder[currentPage].box[j, k].desc == null)
                        {
                            Console.Write("   |");
                            availableSlot += 1;
                        }
                        else
                        {
                            Console.Write(" X |");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"{currentPage + 1}({counterSlots})");
                Console.WriteLine();
                Console.WriteLine($"Name: {Engine.shelfHolder[currentPage].name}");
                Console.WriteLine($"Available slots: {availableSlot}");
                Console.WriteLine();
                Console.WriteLine("(<) Previous shelf (>) Next shelf");
                ConsoleKey input = InputOutput.TakeInput();
                switch (input)
                {
                    case ConsoleKey.RightArrow:
                        currentPage += 1;
                        Console.Clear();
                        break;
                    case ConsoleKey.LeftArrow:
                        currentPage -= 1;
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        hasPressedEsc = true;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
