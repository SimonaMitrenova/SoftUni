namespace Orders
{
    using System.Globalization;
    using System.Threading;

    public class OrdersMain
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var ordersData = new OrdersDataBase();
            OrdersEngine engine = new OrdersEngine(ordersData);

            engine.PrintMostExpensiveProducts();
            
            engine.PrintSeparator();

            engine.PrintProductsNumberInEachCategory();
            
            engine.PrintSeparator();

            engine.PrintTopOrderedProducts();

            engine.PrintSeparator();

            engine.PrintMostProfitableCategory();
        }
    }
}
