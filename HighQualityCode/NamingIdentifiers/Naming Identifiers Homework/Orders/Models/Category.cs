namespace Orders.Models
{
    using System;

    public class Category
    {
        private int id;
        private string name;
        private string description;

        public Category(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
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
                    throw new ArgumentNullException("Product name cannot be null, empry or white space.");
                }

                this.name = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Products must have description.");
                }

                this.description = value;
            }
        }
    }
}
