using System;
using System.Collections.Generic;

namespace SavulionisFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //sample data
            List<Sample> samples = new List<Sample>
            {
                new Sample { X=1, Y=2, Class = "+" },
                new Sample { X=3, Y=4, Class = "+" },
                new Sample { X=6, Y=4, Class = "+" },
                new Sample { X=2, Y=1, Class = "-" },
                new Sample { X=4, Y=1, Class = "-" },
                new Sample { X=5, Y=2, Class = "-" }
            };

            //classification

            List<Sample> newSamples = new List<Sample>
            {
                new Sample { X=5, Y=3 },
                new Sample { X=6, Y=3 }
            };

            int k = 3;

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
//1. 1,2,+
//2. 3,4,+
//3. 6,4,+
//4. 2,1,-
//5. 4,1,-
//6. 5,2,-

//objektai klasifikavimui
//e7. 5,3
//e8. 6,3