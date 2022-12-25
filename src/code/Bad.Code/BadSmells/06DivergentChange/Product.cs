namespace Bad.Code.BadSmells._06DivergentChange
{
    internal class Product
    {
        private readonly ProductType _type;

        public Product(ProductType type) 
            => this._type = type;
        
        public decimal GetBasePrice()
        {
            switch (this._type)
            {
                case ProductType.Foods:
                    return 10;
                case ProductType.Drinks:
                    return 7;
                case ProductType.Books:
                    return 3;
                default:
                    return 0;
            }
        }

        public int GetTaxPercent()
        {
            switch (this._type)
            {
                case ProductType.Foods:
                case ProductType.Drinks:
                    return 24;
                case ProductType.Books:
                    return 8;
                default:
                    return 0;
            }
        }

        public string GetProductCategory()
        {
            switch (this._type)
            {
                case ProductType.Foods:
                case ProductType.Drinks:
                    return "Foods and Beverages";
                case ProductType.Books:
                    return "Education";
                default:
                    return "-";
            }
        }
    }

    internal enum ProductType
    {
        Foods,
        Drinks,
        Books
    }
}