using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyDesignPattern.OpenClosePrinciple
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    class Product_Old
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Product_Old(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
        public override string ToString()
        {
            return Name + " " + Color + " " + Size;
        }
    }

    class ProductFilter
    {
        public IEnumerable<Product_Old> FilterBySize(IEnumerable<Product_Old> products, Size size)
        {
            foreach (var product in products)
            {
                if (product.Size == size)
                    yield return product;
            }
        }
        public IEnumerable<Product_Old> FilterByColor(IEnumerable<Product_Old> products, Color color)
        {
            foreach (var product in products)
            {
                if (product.Color == color)
                    yield return product;
            }
        }
        public IEnumerable<Product_Old> FilterBySizeAndColor(IEnumerable<Product_Old> products, Color color, Size size)
        {
            foreach (var product in products)
            {
                if (product.Color == color && product.Size == size)
                    yield return product;
            }
        }
    }

    public class DemoProduct_Old
    {
        static void Main(string[] args)
        {
            var apple = new Product_Old("Apple", Color.Green, Size.Small);
            var tree = new Product_Old("Tree", Color.Green, Size.Large);
            var house = new Product_Old("House", Color.Blue, Size.Large);
            Product_Old[] productsOld = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green product (old):");
            foreach (var product in pf.FilterByColor(productsOld, Color.Green))
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
