namespace Orders
{
    using System;
    using System.Linq;

    public class OrdersEngine
    {
        private OrdersDataBase data;

        public OrdersEngine(OrdersDataBase data)
        {
            this.OrdersDataBase = data;
        }

        public OrdersDataBase OrdersDataBase
        {
            get
            {
                return this.data;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Database cannot be null.");
                }

                this.data = value;
            }
        }

        public void PrintMostExpensiveProducts(int numberOfElements = 5)
        {
            var mostExpensiveProducts = data.GetAllProducts()
                .OrderByDescending(p => p.ProductPrice)
                .Take(numberOfElements)
                .Select(p => p.Name);

            Console.WriteLine(string.Join(Environment.NewLine, mostExpensiveProducts));
        }

        public void PrintProductsNumberInEachCategory()
        {
            var productsNumberInEachCategory = data.GetAllProducts()
                .GroupBy(p => p.CategoryId)
                .ToDictionary(grp => data.GetAllCategories().First(c => c.Id == grp.Key).Name, grp => grp.Count());

            foreach (var pair in productsNumberInEachCategory)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }

        public void PrintTopOrderedProducts(int numberOfElements = 5)
        {
            var topOrderedProducts = data.GetAllOrders()
                .GroupBy(order => order.ProductId)
                .Select(grp => new { Product = data.GetAllProducts().First(p => p.Id == grp.Key).Name, Quantities = grp.Sum(grpgrp => grpgrp.Quantity)} )
                .OrderByDescending(q => q.Quantities)
                .Take(numberOfElements);

            foreach (var item in topOrderedProducts)
            {
                Console.WriteLine("{0}: {1}", item.Product, item.Quantities);
            }
        }

        public void PrintMostProfitableCategory()
        {
            var mostProfitableCategory = data.GetAllOrders()
                .GroupBy(order => order.ProductId)
                .Select(grouping => new { catId = data.GetAllProducts().First(p => p.Id == grouping.Key).CategoryId, 
                    price = data.GetAllProducts().First(p => p.Id == grouping.Key).ProductPrice, quantity = grouping.Sum(p => p.Quantity) })
                .GroupBy(subgroup => subgroup.catId)
                .Select(grouping => new { categoryName = data.GetAllCategories().First(c => c.Id == grouping.Key).Name, 
                    totalQuantity = grouping.Sum(g => g.quantity * g.price) })
                .OrderByDescending(g => g.totalQuantity)
                .First();

            Console.WriteLine("{0}: {1}", mostProfitableCategory.categoryName, mostProfitableCategory.totalQuantity);
        }

        public void PrintSeparator()
        {
            string separator = new string('-', 10);
            Console.WriteLine(separator);
        }
    }
}
