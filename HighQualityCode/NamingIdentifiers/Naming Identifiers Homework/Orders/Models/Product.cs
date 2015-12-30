namespace Orders.Models
{
    using System;

    public class Product
    {
        private int id;
        private string name;
        private int categoryId;
        private decimal productPrice;
        private int unitsInStock;

        public Product(int id, string name, int categoryId, decimal productPrice, int unitsInStock)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryId;
            this.ProductPrice = productPrice;
            this.UnitsInStock = unitsInStock;
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
                    throw new ArgumentOutOfRangeException("Product Id must be non-negative number.");
                }

                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Product name cannot be null, empty, or white space.");
                }

                this.name = value;
            }
        }

        public int CategoryId
        {
            get
            {
                return this.categoryId;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Category Id must be non-negative number.");
                }

                this.categoryId = value;
            }
        }

        public decimal ProductPrice
        {
            get
            {
                return this.productPrice;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Product price cannot be null or negative number.");
                }

                this.productPrice = value;
            }
        }

        public int UnitsInStock
        {
            get
            {
                return this.unitsInStock;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cannot have negative units in stock.");
                }

                this.unitsInStock = value;
            }
        }
    }
}
