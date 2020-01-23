using System;

namespace Hash_Table_Class
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hash table OF Names");
            HashTable hashTable = new HashTable();
            bool close = false;
            do
            {
                if (Menu(hashTable) == 6)
                {
                    close = true;
                }
            } while (!close);
            
        }

        static int Menu(HashTable table)
        {
            int menuOption = 0;
            bool okOption;

            Console.WriteLine("1. Add name 2. Search name 3. Delete name 4. Print names 5. Show load factor 6. Close");
            do
            {
                try
                {
                    menuOption = Int16.Parse(Console.ReadLine());
                    if (menuOption < 1 || menuOption > 6)
                    {
                        Console.WriteLine("Invalid option");
                        okOption = false;
                    }
                    else
                        okOption = true;
                }
                catch
                {
                    Console.WriteLine("Invalid number.");
                    okOption = false;
                }
            } while (okOption == false);

            switch (menuOption)
            {
                case 1:
                    Console.WriteLine("Type the name you want to add.");
                    string addName = Console.ReadLine();
                    table.Add(addName);
                    break;
                case 2:
                    Search(table);
                    break;
                case 3:
                    Console.WriteLine("Type the name you want to delete.");
                    string delName = Console.ReadLine();
                    table.Delete(delName);
                    break;
                case 4:
                    table.Print();
                    break;
                case 5:
                    table.GetLoadFactor();
                    break;
                case 6:
                    Console.WriteLine("Closing...");
                    break;
                
                default:
                    break;
            }
            return menuOption;
        }
        static void Search(HashTable table)
        {
            Console.WriteLine("Type the name you want to search for.");
            string findName = Console.ReadLine();
            int found = table.Search(findName);

            if (found == -1)
                Console.WriteLine("Name not in hash table.");
            else
                Console.WriteLine("Name is in hash table.");
        }
    }

    class HashTable
    {
        private string[] table;
        private int maxSize;
        private int LoadFactor()
        {
            int count = 0;
            foreach (string name in table)
            {
                if (name != null && name != "000")
                {
                    count++;
                }
            }

            int LF = (count * 10000 / maxSize);
            return LF;
        }

        public HashTable()
        {
            maxSize = 500;
            table = new string[maxSize];
        }
        
        private int Algorithm(string name)
        {
            int nameValue = 0;
            foreach (char letter in name)
            {
                nameValue += letter;
            }
            nameValue = nameValue*7/5;
            nameValue += 383;
            nameValue = nameValue % (maxSize - 1);

            return nameValue;
        }

        public void Add(string name)
        {
            int index = Algorithm(name);
            
            if (table[index] == null || table[index] == "000")
            {
                table[index] = name;
            }
            else
            {
                while (table[index] != null && table[index] != "000")
                {
                    index = (index + 1) % maxSize;
                }
                table[index] = name;
            }
        }

        public int Search(string name)
        {
            int index = Algorithm(name);

            
            if(table[index] == null)
            {
                return -1;
            }
            else if (table[index] == name)
            {
                return index;
            }
            else
            {
                while (table[index] != name)
                {
                    index = (index + 1) % maxSize;
                    if (table[index] == null)
                        return -1;
                }
                return index;
            }
        }

        public void Delete(string name)
        {
            int index = Search(name);

            if (index == -1)
            {
                Console.WriteLine("Name not in hash table.");
            }
            else
            {
                table[index] = "000";
            }
        }

        public void Print()
        {
            foreach (string name in table)
            {
                if (name != null && name != "000")
                {
                    Console.Write("[{0}] ", name);
                }
            }
            Console.WriteLine();
        }

        public void GetLoadFactor()
        {
            float LF = LoadFactor();
            Console.WriteLine("Hash table load factor = {0}%", LF/100);
        }
    }
}
