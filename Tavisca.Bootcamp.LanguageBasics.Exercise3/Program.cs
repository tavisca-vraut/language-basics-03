using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int n = protein.Length;
            int[] calories = new int[n];

            for (int i = 0; i < n; i++) {
                calories[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
            }
            int[] meal = new int[dietPlans.Length];

            for (int i = 0; i < dietPlans.Length; i++) {
                string plan = dietPlans[i];

                if (plan.Length == 0) {
                    meal[i] = 0;
                    continue;
                }

                List<int> indices = new List<int>();
                List<int> tempIndices = new List<int>();

                for (int j = 0; j < n; j++) indices.Add(j);

                int max, min;
                foreach (char ch in plan) {
                    switch(ch) {
                        case 'P':
                            max = FindMax(protein, indices);
                            indices = FindAllIndices(protein, indices, max);
                            break;
                        case 'p':
                            min = FindMin(protein, indices);
                            indices = FindAllIndices(protein, indices, min);
                            break;
                        case 'C':
                            max = FindMax(carbs, indices);
                            indices = FindAllIndices(carbs, indices, max);
                            break;
                        case 'c':
                            min = FindMin(carbs, indices);
                            indices = FindAllIndices(carbs, indices, min);
                            break;
                        case 'F':
                            max = FindMax(fat, indices);
                            indices = FindAllIndices(fat, indices, max);
                            break;
                        case 'f':
                            min = FindMin(fat, indices);
                            indices = FindAllIndices(fat, indices, min);
                            break;
                        case 'T':
                            max = FindMax(calories, indices);
                            indices = FindAllIndices(calories, indices, max);
                            break;
                        case 't':
                            min = FindMin(calories, indices);
                            indices = FindAllIndices(calories, indices, min);
                            break;
                    }
                }

                meal[i] = indices[0];
            }

            return meal;
        }

        public static List<int> FindAllIndices(int[] arr, List<int> indices_, int elem) {
            List<int> indices = new List<int>();

            foreach (int i in indices_) {
                if (arr[i] == elem) indices.Add(i);
            }

            return indices;
        }

        public static int FindMax(int[] arr, List<int> indices) {
            if (indices.Count == 1) return arr[indices[0]];

            int max = arr[indices[0]];

            for (int i = 1; i < indices.Count; i++) {
                if (arr[indices[i]] > max) max = arr[indices[i]];
            }

            return max;
        }
        public static int FindMin(int[] arr, List<int> indices) {
            if (indices.Count == 1) return arr[indices[0]];

            int min = arr[indices[0]];

            for (int i = 1; i < indices.Count; i++) {
                if (arr[indices[i]] < min) min = arr[indices[i]];
            }

            return min;
        }
    }
}
