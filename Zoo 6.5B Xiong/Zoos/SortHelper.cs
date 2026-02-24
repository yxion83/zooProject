using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Animals;
using People;

namespace Zoos
{
    /// <summary>
    /// Class used to represent the sort helper.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Method to sort by bubble.
        /// </summary>
        /// <param name="list">List of animals being sorted.</param>
        /// <param name="comparer">comparer delegate.</param>
        /// <returns>Sorting of animal list by bubble weight.</returns>
        public static SortResult BubbleSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int compareCounter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // use a for loop to loop backward through the list
            for (int i = list.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    compareCounter++;
                    if (comparer(list[j], list[j + 1]) > 0)
                    {
                        list.Swap(j, j + 1);
                        swapCounter++;
                    }
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };

            return result;
        }

        /// <summary>
        /// Method to select sorting by weight.
        /// </summary>
        /// <param name="list">List of objects being sorted by selection of weight.</param>
        /// <param name="comparer">comparer delegate.</param>
        /// <returns>Sorted animals lsit by weight selection.</returns>
        public static SortResult SelectionSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int compareCounter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < list.Count - 1; i++)
            {
                object minObject = (object)list[i];

                for (int j = i + 1; j < list.Count; j++)
                {
                    compareCounter++;

                    if (comparer(minObject, list[j]) > 0)
                    {
                        minObject = list[j];
                    }
                }

                if (comparer(list[i], minObject) != 0)
                {
                    list.Swap(i, list.IndexOf(minObject));
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };

            return result;
        }

        /// <summary>
        /// Method to sort insertions by weight.
        /// </summary>
        /// <param name="list">Animal list for sorting by insertion of weight.</param>
        /// <param name="comparer">comparer delegate.</param>
        /// <returns>List of animals sorted by insertion.</returns>
        public static SortResult InsertionSort(this IList list, Func<object, object, int> comparer)
        {
            int swapCounter = 0;
            int compareCounter = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 1; i < list.Count; i++)
            {
                compareCounter++;

                for (int j = i; j > 0 && (comparer(list[j], list[j - 1]) < 0); j--)
                {
                    list.Swap(list.IndexOf(list[j]), list.IndexOf(list[j - 1]));
                    swapCounter += 1;
                }
            }

            stopwatch.Stop();

            SortResult result = new SortResult { SwapCount = swapCounter, Objects = list.Cast<object>().ToList(), CompareCount = compareCounter, ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds };

            return result;
        }

        /// <summary>
        /// Method to sort quickly by weight.
        /// </summary>
        /// <param name="list">Animal list.</param>
        /// <param name="leftIndex">Left index.</param>
        /// <param name="rightIndex">Right index.</param>
        /// <param name="sortResult">The sort result.</param>
        /// <param name="comparer">comparer delegate.</param>
        /// <returns>List sorted by weight.</returns>
        public static SortResult QuickSort(this IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            // left section of the animals list.
            int leftPointer = leftIndex;

            // right section of the animals list.
            int rightPointer = rightIndex;

            // Gets the animal between the index points.
            object pivotAnimal = (object)list[(leftIndex + rightIndex) / 2];
            bool done = false;

            while (!done)
            {
                while (comparer(list[leftPointer], pivotAnimal) < 0)
                {
                    leftPointer++;
                    sortResult.CompareCount++;
                }

                while (comparer(pivotAnimal, list[rightPointer]) < 0)
                {
                    rightPointer--;
                    sortResult.CompareCount++;
                }

                if (leftPointer <= rightPointer)
                {
                    list.Swap(leftPointer, rightPointer);
                    sortResult.SwapCount++;
                    leftPointer++;
                    rightPointer--;
                }

                if (leftPointer > rightPointer)
                {
                    done = true;
                }
            }

            // If the LEFT "section" of the list isn't sorted, sort it.
            if (leftIndex < rightPointer)
            {
                QuickSort(list, leftIndex, rightPointer, sortResult, comparer);
            }

            // If the RIGHT "section" of the list isn't sorted, sort it.
            if (rightIndex > leftPointer)
            {
                QuickSort(list, leftPointer, rightIndex, sortResult, comparer);
            }

            sortResult.Objects = list.Cast<object>().ToList();
            return sortResult;
        }

        /// <summary>
        /// Method to swap.
        /// </summary>
        /// <param name="list">Animal being swaped.</param>
        /// <param name="index1">Current animal.</param>
        /// <param name="index2">Value of second animal.</param>
        private static void Swap(this IList list, int index1, int index2)
        {
            object objects = (object)list[index2];
            list[index2] = list[index1];
            list[index1] = objects;
        }
    }
}
