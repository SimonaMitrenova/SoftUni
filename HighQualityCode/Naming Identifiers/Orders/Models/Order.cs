namespace Orders.Models
{
    using System;

    public class Order
    {
        private int id;
        private int productId;
        private int quantity;
        private decimal discount;

        public Order(int id, int productId, int quantity, decimal discount)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Discount = discount;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Order ID must be non-negative number.");
                }

                this.id = value;
            }
        }

        public int ProductId
        {
            get
            {
                return this.productId;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Product ID must be non-negative number.");
                }

                this.productId = value;
            }
        }

        public int Quantity
        {
            get
            {
                return this.quantity;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Orders quantity must be non-negative number.");
                }

                this.quantity = value;
            }
        }

        public decimal Discount
        {
            get
            {
                return this.discount;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Orders discount cannot be negative number.");
                }

                this.discount = value;
            }
        }
    }
}
