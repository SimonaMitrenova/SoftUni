namespace Orders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Orders.Models;

    public class OrdersDataBase
    {
        private string categoriesFileName;
        private string productsFileName;
        private string ordersFileName;

        public OrdersDataBase(string categoriesFileName, string productsFileName, string ordersFileName)
        {
            this.CategoriesFileName = categoriesFileName;
            this.ProductsFileName = productsFileName;
            this.OrdersFileName = ordersFileName;
        }

        public OrdersDataBase()
            : this("../../Data/categories.txt", "../../Data/products.txt", "../../Data/orders.txt")
        {
        }

        public string CategoriesFileName
        {
            get
            {
                return this.categoriesFileName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Categories file name cannot be null, empty, ot white space.");
                }

                this.categoriesFileName = value;
            }
        }

        public string ProductsFileName
        {
            get
            {
                return this.productsFileName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Products file name cannot be null, empty or white space.");
                }

                this.productsFileName = value;
            }
        }

        public string OrdersFileName
        {
            get
            {
                return this.ordersFileName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Orders file name cannot be null, empty or white space.");
                }

                this.ordersFileName = value;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var fileLines = this.ReadFileLines(this.CategoriesFileName, true);
            IEnumerable<Category> categories = fileLines
                .Select(c => c.Split(','))
                .Select(c => new Category(int.Parse(c[0]), c[1], c[2]));

            return categories;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var fileLines = this.ReadFileLines(this.ProductsFileName, true);
            IEnumerable<Product> products = fileLines
                .Select(p => p.Split(','))
                .Select(p => new Product(int.Parse(p[0]), p[1], int.Parse(p[2]), decimal.Parse(p[3]), int.Parse(p[4])));

            return products;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var fileLines = this.ReadFileLines(this.OrdersFileName, true);
            IEnumerable<Order> orders = fileLines
                .Select(p => p.Split(','))
                .Select(p => new Order(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]), decimal.Parse(p[3])));

            return orders;
        }

        private IEnumerable<string> ReadFileLines(string fileName, bool hasHeader)
        {
            var allLines = new List<string>();

            using (var reader = new StreamReader(fileName))
            {
                if (hasHeader)
                {
                    reader.ReadLine();
                }

                string currentLine = reader.ReadLine();

                while (currentLine != null)
                {
                    allLines.Add(currentLine);
                    currentLine = reader.ReadLine();
                }
            }

            return allLines;
        }
    }
}
