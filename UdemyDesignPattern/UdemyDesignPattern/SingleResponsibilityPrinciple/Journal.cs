using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyDesignPattern.SingleResponsibilityPrinciple
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //momento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists((fileName)))
            {
                File.WriteAllText(fileName, j.ToString());
            }

        }
    }

    public class DemoJournal
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I ate an Apple");
            j.AddEntry("I ate an Orange");
            j.AddEntry("I ate an Pineapple");
            Console.WriteLine(j.ToString());

            var p = new Persistence();
            var fileName = @"c:\temp\journal.txt";
            p.SaveToFile(j, fileName, true);
            Process.Start(fileName);
        }
    }
}
