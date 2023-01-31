using System;
using System.Collections.Generic;
using System.IO;

namespace SavulionisFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //sample data
            List<Sample> samples = new List<Sample>();
            using (StreamReader sr = new StreamReader("C:\\Users\\smics\\Desktop\\SavulionoDarbai\\SavulionisFirst\\SavulionisFirst\\samples.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    samples.Add(new Sample
                    {
                        X = int.Parse(line[0]),
                        Y = int.Parse(line[1]),
                        Class = line[2]
                    });
                }
            }

            //classification

            List<Sample> newSamples = new List<Sample>();
            using (StreamReader sr = new StreamReader("C:\\Users\\smics\\Desktop\\SavulionoDarbai\\SavulionisFirst\\SavulionisFirst\\newSamples.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    newSamples.Add(new Sample
                    {
                        X = int.Parse(line[0]),
                        Y = int.Parse(line[1]),
                        Class = ""
                    });
                }
            }

            Console.Write("input neighbors: ");
            int k = Convert.ToInt32(Console.ReadLine());


            foreach (Sample newSample in newSamples)
            {
                List<Tuple<Sample, double>> distances = new List<Tuple<Sample, double>>();
                foreach (Sample sample in samples)
                {
                    double distance = Math.Sqrt(Math.Pow(sample.X - newSample.X, 2) + Math.Pow(sample.Y - newSample.Y, 2));
                    distances.Add(new Tuple<Sample, double>(sample, distance));
                }

                //sort distances
                distances.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                //count the num of samples
                Dictionary<string, int> classCount = new Dictionary<string, int>();

                for (int i = 0; i < k; i++)
                {
                    Sample sample = distances[i].Item1;
                    if (!classCount.ContainsKey(sample.Class))
                    {
                        classCount[sample.Class] = 0;
                    }
                    classCount[sample.Class]++;
                }

                //find class with the highest count

                string className = "";
                int maxCount = 0;
                foreach (var item in classCount)
                {
                    if (item.Value > maxCount)
                    {
                        className = item.Key;
                        maxCount = item.Value;
                    }
                    else if (item.Value == maxCount)
                    {
                        className = "";
                    }
                }


                //asign the class with the highest count to new sampl

                newSample.Class = className;

                Console.WriteLine("X = " + newSample.X + " and Y = " + newSample.Y + ": " + newSample.Class);
            }
        }
    }

    class Sample
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Class { get; set; }
    }
}
