using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {

            List<int> mainArray = new List<int>();
            int arrayLength;
            int minDigit, maxDigit;

            List<int> ascendSequence;
            List<int> descendSequence;

            float mediumArithmetic;

            //Call calculation class
            Calculation calc = new Calculation();
            //Read the file
            System.Console.WriteLine("Set the file to read:");

            string pathtofile = System.Console.ReadLine();

            //Count time start
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //Calculations
            calc.ReadArray(pathtofile, mainArray, out arrayLength);
            calc.FindMinMax(mainArray, arrayLength, out minDigit, out maxDigit);
            calc.AscendSequence(mainArray, out ascendSequence);
            calc.DescendSequence(mainArray, out descendSequence);
            calc.MidArithmetic(mainArray,out mediumArithmetic);

            Median median = new Median();
            median.FindMedian(mainArray);

            //Count time stop
            watch.Stop();
            var elapsedTime = watch.Elapsed.Seconds;
            System.Console.WriteLine("Time elapsed: " + elapsedTime + " sec.");

            System.Console.ReadLine();
        }
    }
    class Calculation
    {
        public void ReadArray(string filepath, List<int> arrayToWrite, out int count)
        {
            count = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
                new System.IO.StreamReader(filepath);
            // Read array
            while ((line = file.ReadLine()) != null)
            {
                if (int.TryParse(line, out int digit))
                {
                    arrayToWrite.Add(Convert.ToInt32(digit));
                    count++;
                }
            }
            file.Close();

            System.Console.WriteLine("There were "+count+" lines.");
        }
        public void FindMinMax(List<int> arrayList, int arr_length, out int min, out int max)
        {
            min = max = arrayList[0];
            for(int i = 1; i <= arr_length - 1; i++)
            {
                if (max < arrayList[i]) max = arrayList[i];
                if (min > arrayList[i]) min = arrayList[i];
            }
            System.Console.WriteLine("Lowest number: "+min+", highest number: "+max+"");
        }
        public void AscendSequence(List<int> array, out List<int> ascSequence)
        {
            List<int> checkSequence = new List<int>();
            ascSequence = new List<int>();

            checkSequence.Add(array[0]);
            for(int i=1; i<=array.Count-1; i++)
            {
                if (array[i-1] <= array[i])
                {
                    //continue
                }
                else if (checkSequence.Count > ascSequence.Count)
                {
                    ascSequence = checkSequence;
                    checkSequence = new List<int>();
                }
                else
                {
                    checkSequence = new List<int>();
                }
                checkSequence.Add(array[i]);
            }
            System.Console.WriteLine("Ascending sequence:");
            System.Console.WriteLine("[{0}]", string.Join(", ", ascSequence));
        }
        public void DescendSequence(List<int> array, out List<int> descSequence)
        {
            List<int> checkSequence = new List<int>();
            descSequence = new List<int>();

            checkSequence.Add(array[0]);
            for (int i = 1; i <= array.Count - 1; i++)
            {
                if (array[i - 1] >= array[i])
                {
                    //continue
                }
                else if (checkSequence.Count > descSequence.Count)
                {
                    descSequence = checkSequence;
                    checkSequence = new List<int>();
                }
                else
                {
                    checkSequence = new List<int>();
                }
                checkSequence.Add(array[i]);
            }
            System.Console.WriteLine("Descending sequence:");
            System.Console.WriteLine("[{0}]", string.Join(", ", descSequence));

        }
        public void MidArithmetic(List<int> array, out float midValue)
        {
            int summary=0;
            foreach (int a in array) summary += a;
            midValue = summary / array.Count;
            System.Console.WriteLine("Arithmetic mean: " + midValue + "");
        }
    }
    class Median
    {
        static int a, b;

        // Utility function to swapping of element
        static List<int> swap(List<int> arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            return arr;
        }

        // Returns the correct position of
        // pivot element
        static int Partition(List<int> arr, int l, int r)
        {
            int lst = arr[r], i = l, j = l;
            while (j < r)
            {
                if (arr[j] < lst)
                {
                    arr = swap(arr, i, j);
                    i++;
                }
                j++;
            }
            arr = swap(arr, i, r);
            return i;
        }

        /*  Picks a random pivot element between l and r
         *  and partitions arr[l..r] around the randomly picked
         *  element using partition()
        */
        static int randomPartition(List<int> arr, int l, int r)
        {
            int n = r - l + 1;
            int pivot = (int)(new Random().Next() % n);
            arr = swap(arr, l + pivot, r);
            return Partition(arr, l, r);
        }

        // Utility function to find median
        static int MedianUtil(List<int> arr, int l, int r, int k)
        {

            // if l < r
            if (l <= r)
            {

                // Find the partition index
                int partitionIndex = randomPartition(arr, l, r);

                // If partion index = k, then
                // we found the median of odd
                // number element in []arr
                if (partitionIndex == k)
                {
                    b = arr[partitionIndex];
                    if (a != -1)
                        return int.MinValue;
                }

                // If index = k - 1, then we get
                // a & b as middle element of
                // []arr
                else if (partitionIndex == k - 1)
                {
                    a = arr[partitionIndex];
                    if (b != -1)
                        return int.MinValue;
                }

                // If partitionIndex >= k then
                // find the index in first half
                // of the []arr
                if (partitionIndex >= k)
                    return MedianUtil(arr, l, partitionIndex - 1, k);

                // If partitionIndex <= k then
                // find the index in second half
                // of the []arr
                else
                    return MedianUtil(arr, partitionIndex + 1, r, k);
            }

            return int.MinValue;
        }

        // Function to find Median
        public void FindMedian(List<int> arr)
        {
            int n = arr.Count;
            int answer;
            a = -1;
            b = -1;

            // If n is odd
            if (n % 2 == 1)
            {
                MedianUtil(arr, 0, n - 1, n / 2);
                answer = b;
            }

            // If n is even
            else
            {
                MedianUtil(arr, 0, n - 1, n / 2);
                answer = (a + b) / 2;
            }

            // Print the Median of []arr
            System.Console.WriteLine("Median: " + answer + "");
        }

    }
}
