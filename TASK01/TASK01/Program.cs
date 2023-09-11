using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace TASK01

{
    public class program
    {
        public int[] Array { get; internal set; }
        public TimeSpan time { get; set; }

        List<string> SortList = new List<string>();

        static async Task Main(string[] args)

        {
            var array = GenerateRandomArray(30, 1, 1000);
            var sortFunction = new program();
            sortFunction.Array = array;
            double elapsedTime;
            List<string> stringList = new List<string>();
            showArray("The  unSorted arry is :", array);

            // synchrounus 

            // ----------  bubble sort

            var Bubble_sortarr = sortFunction.sBubbleSort();
            elapsedTime = sortFunction.time.TotalSeconds;
            showArray(" Bubble sort , Sorted list", Bubble_sortarr);
            stringList.Add("Time taken by Bubble Sort: " + elapsedTime.ToString("0.000000"));

            // ------------ Quick sort 

            var Quick_sortarr = sortFunction.sQuickSort();
            elapsedTime = sortFunction.time.TotalSeconds;
            showArray("Quick sort , sorted list : ", Quick_sortarr);
            stringList.Add("Time taken by quiksort : " + elapsedTime.ToString("0.000000"));

            //Async & await 

            //---------------Async bubble sort


            var Asyncbubblesorted_Arr = sortFunction.AsyncBubbleSort();
            var AsyncQuicksorted_Arr = sortFunction.AsyncQuickSort(array, 0, array.Length - 1);
            var ListOfTasks = new List<Task> { AsyncQuicksorted_Arr, Asyncbubblesorted_Arr };
            while (ListOfTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(ListOfTasks);

                if (finishedTask == Asyncbubblesorted_Arr)
                {
                    //AsyncSyncronus Bubble Sorting
                    elapsedTime = sortFunction.ts.TotalSeconds;
                    showArray("Array List after Bubble Sorting", await Asyncbubblesorted_Arr);
                    stringList.Add("Time taken by Async Bubble Sort :" + elapsedTime.ToString("0.000000"));

                }
                else
                {
                    //AsyncSyncronus Quick Sorting
                    elapsedTime = sortFunction.time.TotalSeconds;
                    showArray(" Quick sorted list : ", await AsyncQuicksorted_Arr);
                    stringList.Add("Time taken by Async Quick Sort :" + elapsedTime.ToString("0.000000"));
                }

                await finishedTask;
                ListOfTasks.Remove(finishedTask);
            }

            //------------ time comaprison .............
            print_Timecomparelist(stringList);
            Console.ReadLine();



            //..........






        }

        private static void print_Timecomparelist(List<string> stringList)
        {
            foreach(var logMessage in stringList)
            {
                Console.WriteLine(logMessage);
            }
        }
//''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public async Task<int[]> AsyncQuickSort(int[] Array, int right, int left)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var n = Array.Length;


            var i = left;
            var j = right;
            var pivot = Array[left];

            while (i <= j)
            {
                while (Array[i] < pivot)
                {
                    i++;
                }

                while (Array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                    i++;
                    j--;
               
            }
            }

            if (left < j)
                await AsyncQuickSort(Array, left, j);
            if (i < right)
                await AsyncQuickSort(Array, i, right);



            stopWatch.Stop();
            time = stopWatch.Elapsed;

            return Array;
        }




        public async Task<int[]> AsyncBubbleSort()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();


            var n = Array.Length;
            await Task.Run(() =>

            {
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1; j++)

                    {
                        if (Array[j] > Array[j + 1])
                        {
                            var tem = Array[j];
                            Array[j] = Array[j + 1];
                            Array[j + 1] = tem;

                        }


                    }
                }
            }});


    

    public int[] sQuickSort(int[] Array, int left, int right)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var i = left;
            var j = right;
            var pivot = Array[left];
            while (i < j)
            {
                while (Array[i] < pivot)
                {
                    i++;
                }
                while (Array[j] > pivot) {
                    j--;

                }

                if (i <= j)
                {
                    int temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                    i++;
                    j--;

                    return Array;
                }
            }
        }





        public int[] sBubbleSort()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var n = Array.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (Array[j] > Array[j + 1])
                    {
                        var tempVar = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = tempVar;
                    }
            stopWatch.Stop();
            time = stopWatch.Elapsed;
            return Array;
        }



        public static void showArray(string var, int[] array)
        {
            Console.WriteLine(var + "\n");
            foreach (int i in array) Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static int[] GenerateRandomArray(int length, int minValue, int maxValue)
        {
            if (length <= 0 || minValue > maxValue)
            {
                throw new ArgumentException("Invalid input parameters");
            }

            Random random = new Random();
            int[] randomArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                randomArray[i] = random.Next(minValue, maxValue + 1);
            }

            return randomArray;


        }


    }

}

