using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace UdemyDesignPattern.OpenClosePrinciple.Solution
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
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


    #region Solution 1
    /*
     * ISpecification and IFilter are two main methods that make the solution structure. all other classes implement these to Interfaces and 
     * keep our clases close to modification
     */
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return this.color == t.Color;
        }
    }
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return this.size == t.Size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    public class CustomFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> spec)
        {
            foreach (var product in products)
            {
                if (spec.IsSatisfied(product))
                    yield return product;
            }
        }
    }
    #endregion
    /*
     * Thanks to the Anonymous Method programming using delegate, in BetterFilter Class, FilterHandler 
     */
    #region Solution 2

    public class BetterFilter<T>
    {
        public delegate bool FilterHandler(T t);

        public IEnumerable<T> ApplyFilter(IEnumerable<T> items, FilterHandler filterHandler)
        {
            foreach (var item in items)
            {
                if (filterHandler(item))
                    yield return item;
            }
        }
    }

    #endregion
    public class DemoProduct
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = { apple, tree, house };

            //Solution #1
            Console.WriteLine(" //Solution #1");
            var colorSpec = new ColorSpecification(Color.Green);
            var customFilter = new CustomFilter();
            foreach (var product in customFilter.Filter(products, colorSpec))
            {
                Console.WriteLine(product.ToString());
            }

            //Solution #2 using Ananymous Method
            Console.WriteLine(" //Solution #2");
            var bf = new BetterFilter<Product>();
            foreach (var product in bf.ApplyFilter(products, delegate(Product p) { return p.Color == Color.Green; }))
            {
                Console.WriteLine(product.ToString());
            }

            //Solution #3 using both having complicated filtering
            Console.WriteLine(" //Solution #3");
            var sizeSpec = new SizeSpecification(Size.Large);
            var bfvComplicated = new BetterFilter<Product>();
            BetterFilter<Product>.FilterHandler fh = colorSpec.IsSatisfied;
            fh += sizeSpec.IsSatisfied;
            foreach (var product in bfvComplicated.ApplyFilter(products, fh))
            {
                Console.WriteLine(product.ToString());
            }
        }
    }

}
