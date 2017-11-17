using MarkovGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main()
        { 
            var basePath = @"C:\Users\alexr\Documents\";

            var frankenstein = basePath + "frankenstein.txt";
            var ulysees = basePath + "ulysees.txt";
            var leviathan = basePath + "leviathan.txt";
            var generator = Markov.MakeGenerator(2, (ulysees, 1), (frankenstein, 10));

            //string sentence;

            //SentenceReader reader =
            //    new SentenceReader(@"C:\Users\alexr\Documents\markovInput3.txt");
            //StreamWriter writer =
            //    new StreamWriter(@"C:\Users\alexr\Documents\markovOutput.txt");
            Random random = new Random();
            foreach (int i in Enumerable.Range(1, 15))
            {
                System.Console.WriteLine(generator.MakeMarkovSentence(random));
                System.Console.WriteLine();
            }
            System.Console.ReadLine();
            //var lines = new List<String>();
            //while ((sentence = reader.ReadSentence()) != null)
            //{
            //    lines.Add(sentence);
            //}
            //lines.ForEach(l => writer.WriteLine(l));
            //System.Console.ReadLine();
            //reader.Close();
            //lines.ForEach(l => writer.WriteLine()); 
            //writer.Close();
            //Random random = new Random();
            //var markov = new Generator(lines, 2);
            //foreach (int i in Enumerable.Range(0, 15))
            //{
            //    System.Console.WriteLine(markov.MakeMarkovSentence(random));
            //}
            //System.Console.ReadLine();


        }
    }
}
