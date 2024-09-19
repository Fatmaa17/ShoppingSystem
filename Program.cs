namespace ShoppingSystem
{
    internal class Program
    {
        static public List<string> cartList = new List<string>();
        static public Dictionary<string, double> ItemandPrices = new Dictionary<string, double>()
        {
            {"Camera" , 5000 },
            {"Labtop", 15000 },
            {"TV" , 80000 },
            {"Game" , 500 }

        };
        static Stack<string> actions = new Stack<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("1. Add item to Cart" +
                "\n2. View cart Items" +
                "\n3. Remove item from cart" +
                "\n4. Checkout" +
                "\n5. Undo last action" +
                "\n6. Exit");
            while (true)
            {
                Console.WriteLine("\n\nEnter ur choice number" +
                    "\n========================");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //Add item
                        AddItem();
                        break;
                    case 2:
                        //view
                        ViewItems();
                        break;
                    case 3:
                        //remove
                        RemoveItem();
                        break;
                    case 4:
                        //checkout
                        Checkout();
                        break;
                    case 5:
                        //undo
                        UserAction();
                        break;
                    case 6:
                        //Exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }

            }

        }


        private static void AddItem()
        {
            Console.WriteLine("Available Items:");
            foreach (var item in ItemandPrices)
            {
                Console.WriteLine($"{item.Key} --> {item.Value}LE");
            }
            Console.WriteLine("Please enter product name");
            string newItemTitle = Console.ReadLine();
            if (ItemandPrices.ContainsKey(newItemTitle))
            {
                cartList.Add(newItemTitle);
                actions.Push($"Item {newItemTitle} added to cart");
                Console.WriteLine("Item added to cart");
            }
            else
            {
                Console.WriteLine("Item not available, Sorry!");
            }
        }
        private static void ViewItems()
        {
            Console.WriteLine("Cart items:");
            if (cartList.Any())
            {

                var itemPriceCollection = GetCartPrices();

                foreach (var item in itemPriceCollection)
                {
                    Console.WriteLine($"{item.Item1} -> {item.Item2}");
                }
            }
            else
                Console.WriteLine("Cart is empty, Add items to view them");
        }
        private static IEnumerable<Tuple<string, double>> GetCartPrices()
        {
            var cartPrices = new List<Tuple<string, double>>();
            foreach (var item in cartList)
            {
                double price = 0;
                bool foundItem = ItemandPrices.TryGetValue(item, out price);
                if (foundItem)
                {
                    Tuple<string, double> itemPrice = new Tuple<string, double>(item, price);
                    cartPrices.Add(itemPrice);
                }
            }
            return cartPrices;
        }

        private static void RemoveItem()
        {
            ViewItems();
            if (cartList.Any())
            {
                Console.WriteLine("Select item to remove");
                string itemToRemove = Console.ReadLine();
                if (cartList.Contains(itemToRemove))
                {
                    cartList.Remove(itemToRemove);
                    actions.Push($"Item {itemToRemove} removed from cart");
                    Console.WriteLine("Item removed");
                }
                else
                {
                    Console.WriteLine("Not in cart");
                }
            }

        }
        private static void Checkout()
        {
            if (cartList.Any())
            {
                double totalPrices = 0;
                Console.WriteLine("Ur cart items:");
                var cartItems = GetCartPrices();
                foreach (var item in cartItems)
                {
                    totalPrices += item.Item2;
                    Console.WriteLine(item.Item1 + " " + item.Item2);
                }
                Console.WriteLine($"Total price: {totalPrices}");
                Console.WriteLine("Go to payment, Thank u:))");
                cartList.Clear();
                actions.Push("Checkout");
            }
            else
            {
                Console.WriteLine("Ur cart is empty");
            }
        }
        private static void UserAction()
        {
            if (actions.Count > 0)
            {
                string lastAction = actions.Pop();
                Console.WriteLine($"Ur last action is: {lastAction}");
                var actionArray = lastAction.Split();
                if (lastAction.Contains("added"))
                {
                    cartList.Remove(actionArray[1]);
                }
                else if (lastAction.Contains("removed"))
                {
                    cartList.Add(actionArray[1]);
                }
                else Console.WriteLine("Checkout cannot b undo");
            }
        }


    }
}
