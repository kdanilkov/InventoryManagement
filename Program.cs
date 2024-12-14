namespace InventoryManagement;

internal class Program
{
    static void Main(string[] args)
    {
        ProductCatalog productCatalog = new ProductCatalog();
            
            
        // control the application based on the user input
        bool isRunning = true;
        while(isRunning)
        {
           
            Console.WriteLine();
            Console.WriteLine("Enter command: Add, Remove, Update, Reporting, Exit");
            Console.WriteLine();
            string inputCommand = Console.ReadLine()!.ToLowerInvariant();
           switch (inputCommand)
           {
               case "add":
                  string productName = ReadProductName();
                   Console.WriteLine("Enter stock count");
                   int stockCount = Convert.ToInt32(Console.ReadLine()); //TODO: add validation for number
                   productCatalog.AddProduct(productName, stockCount);
                   break;
               
               case "remove":
                   string productNameToRemove =  ReadProductName();
                   productCatalog.RemoveProduct(productNameToRemove);
                   break;
               
               case "update":
                   string productNameToUpdate =  ReadProductName();
                   Console.WriteLine("Enter stock count");
                   int stockCountToUpdate = Convert.ToInt32(Console.ReadLine()); //TODO: add validation for number
                   productCatalog.UpdateProduct(productNameToUpdate, stockCountToUpdate);
                   break;
               case "reporting":
                   Reporting.GenerateReport(productCatalog);
                   break;
               case "exit":
                   isRunning = false;
                   break;
               default:
                   Console.WriteLine("Invalid command");
                   break;
           }
            
        }
        Environment.Exit(0);
    }

    private static string ReadProductName()
    {
        string productName;
        do
        {
            Console.WriteLine("Enter product name. Product name cannot be empty");
            productName = Console.ReadLine()!;
            
        }while (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName));
        return productName;
    }
    private static class Reporting
    {
        public static void GenerateReport(ProductCatalog productCatalog)
        {
            var products = productCatalog.GetAllProducts();
            
            // possible to make this with teh for loop, but this way is faster and for loop already presented in the GetAllProducts method
            Console.WriteLine("Product catalog:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.ProductName}, Stock Count: {product.StockCount}");
            }
        }
    }

   
    public class Product
    {
        public string ProductName { get; set; }
        //we assume no decimal points for simplicity
        public int StockCount { get; set; }
    }
    
    public class ProductCatalog
    {
        private List<Product?> Products { get; set; }
        
        public ProductCatalog()
        {
            Products = new List<Product?>();
        }
        
        public void AddProduct(string productName, int stockCount)
        {
            //check if product already exists
            if(GetProduct(productName)==null)
            {
                Products.Add(new Product { ProductName = productName, StockCount = stockCount });
                Console.WriteLine("Product added successfully");
            }
            else
            {
                Console.WriteLine("Product already exists");
            }
           
        }
       
        
        public void RemoveProduct(string productName)
        {
            var product = GetProduct(productName);
            if(product != null)
            {
                Products.Remove(product);
                Console.WriteLine("Product removed successfully");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }  
        
        
        /// <summary>
        /// this method updates the stock count of a product
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="stockCount"></param>
        public void UpdateProduct(string productName, int stockCount)
        {
            Product? product = GetProduct(productName);
            if(product != null)
            {
                GetProduct(productName).StockCount = stockCount;
                Console.WriteLine("Product updated successfully");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        
        public List<Product?> GetAllProducts()
        {
            return Products;
        }
        public Product? GetProduct(string productName)
        {
            var count  = Products.Count;
            for (int i = 0; i < count; i++)
            {
                if(Products[i].ProductName == productName)
                {
                    return Products[i];
                }
            }
            
            return null;
        }
        
    }
}