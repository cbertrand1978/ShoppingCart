using CartProcessingService.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TillScanner
{
    class Program
    {
        static HttpClient HttpClient = new HttpClient();
        static ShoppingCart Cart = new ShoppingCart();

        static Product Orange = new Product()
        {
            Description = "Clementine",
            Id = 1,
            Name = "Orange",
            UnitPrice = 0.25M
        };

        static Product Apple = new Product()
        {
            Description = "Red Delicious",
            Id = 2,
            Name = "Apple",
            UnitPrice = 0.60M
        };

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            HttpClient.BaseAddress = new Uri("https://localhost:44390/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                await ShopForItems();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        static async Task ShopForItems()
        {
            var menuChoice = 0;
            var quantity = 0;
            var menuReader = string.Empty;

            while (menuChoice != 3 && menuChoice != 4)
            {
                Console.WriteLine("1) Add oranges to cart.");
                Console.WriteLine("2) Add apples to cart.");
                Console.WriteLine("3) Proceed to checkout.");
                Console.WriteLine("4) Exit.");
                menuReader = Console.ReadLine();

                if (int.TryParse(menuReader, out menuChoice) && (menuChoice != 3 && menuChoice != 4))
                {
                    Console.Write("Enter amount required: ");
                    menuReader = Console.ReadLine();
                }
                else
                {
                    continue;
                }

                if (int.TryParse(menuReader, out quantity))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Cart.AddItemToCart(Orange, quantity);
                            break;
                        case 2:
                            Cart.AddItemToCart(Apple, quantity);
                            break;
                        default: break;
                    }
                }
            }

            if (menuChoice == 3)
            {
                var response = await ProcessShoppingCartAsync(Cart);

                if (response.IsSuccessful)
                {
                    await PrintProcessedCart(response.Result);
                }
            }
        }


        static async Task<CartServiceResponse> ProcessShoppingCartAsync(ShoppingCart shoppingCart)
        {
            CartServiceResponse result = null;

            try
            {

                HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/shoppingcart", shoppingCart);


                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<CartServiceResponse>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return result;
        }

        static async Task PrintProcessedCart(ShoppingCart cart)
        {
            Console.WriteLine();
            Console.WriteLine($"{"Name".PadRight(10)} | {"Unit Price".PadRight(10)} | {"Quantity".PadRight(10)} | {"Line Total".PadRight(10)}");
            Console.WriteLine("".PadRight(49, '-'));

            cart.CartContents.ForEach(x =>
            {
                Console.WriteLine($"{x.Product.Name.PadRight(10)} | {x.Product.UnitPrice.ToString("C").PadRight(10)} | {x.Quantity.ToString().PadRight(10)} | {x.TotalPrice.ToString("C").PadRight(10)}");
            });

            Console.WriteLine();
            Console.WriteLine("".PadRight(25, '-'));
            Console.WriteLine($"Sub Total: {cart.SubTotal.ToString("C")}");
            Console.WriteLine("".PadRight(25, '-'));

            if (cart.Discounts.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{"Amount".PadRight(10)} | {"Discount".PadRight(10)}");
                Console.WriteLine("".PadRight(30, '-'));

                cart.Discounts.ForEach(x =>
                {
                    Console.WriteLine($"-{x.Amount.ToString("C").PadRight(10)} | {x.Description.PadRight(10)}");
                });
            }

            Console.WriteLine();
            Console.WriteLine("".PadRight(25, '-'));
            Console.WriteLine($"Cart Total: {cart.CartTotal.ToString("C")}");
            Console.WriteLine("".PadRight(25, '-'));
        }
    }
}
