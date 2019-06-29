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

            // Generate the Calories array
            for (int i = 0; i < n; i++) {
                calories[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
            }

            // Array to hold the meal index for each diet plan
            int[] meal = new int[dietPlans.Length];

            for (int i = 0; i < dietPlans.Length; i++) {
                string plan = dietPlans[i];
                
                // Default meal for no diet plan is meal at index 0
                if (plan.Length == 0) {
                    meal[i] = 0;
                    continue;
                }

                int[] indicesOfPossibleMeals = new int[n];

                // At first, meals at all indices are available
                for (int j = 0; j < n; j++) indicesOfPossibleMeals[j] = j;

                int max, min;
                foreach (char ch in plan) {
                    // Capital letter implies the meal should contain Max of it
                    // Small letter implies the meal should contain Min of it
                    switch(ch) {
                        case 'P':
                            max = FindMaxFromParticularIndices(protein, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(protein, indicesOfPossibleMeals, max);
                            break;
                        case 'p':
                            min = FindMinFromParticularIndices(protein, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(protein, indicesOfPossibleMeals, min);
                            break;
                        case 'C':
                            max = FindMaxFromParticularIndices(carbs, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(carbs, indicesOfPossibleMeals, max);
                            break;
                        case 'c':
                            min = FindMinFromParticularIndices(carbs, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(carbs, indicesOfPossibleMeals, min);
                            break;
                        case 'F':
                            max = FindMaxFromParticularIndices(fat, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(fat, indicesOfPossibleMeals, max);
                            break;
                        case 'f':
                            min = FindMinFromParticularIndices(fat, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(fat, indicesOfPossibleMeals, min);
                            break;
                        case 'T':
                            max = FindMaxFromParticularIndices(calories, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(calories, indicesOfPossibleMeals, max);
                            break;
                        case 't':
                            min = FindMinFromParticularIndices(calories, indicesOfPossibleMeals);
                            indicesOfPossibleMeals = GetIndicesOfPossibleMeals(calories, indicesOfPossibleMeals, min);
                            break;
                    }
                }

                // First index of possible meal is chosen
                meal[i] = indicesOfPossibleMeals[0];
            }

            return meal;
        }

        public static int[] GetIndicesOfPossibleMeals(int[] arr, int[] indices_, int elem) {
            return indices_.Where(i => arr[i] == elem).ToArray();
        }

        // Indices arr specify what values to choose from arr.
        // And return Max value from that sub-array 
        // Similar approach for FindMinFromParticularIndices()
        public static int FindMaxFromParticularIndices(int[] arr, int[] indices) {
            return indices.Select(index => arr[index]).Max();
        }
        public static int FindMinFromParticularIndices(int[] arr, int[] indices) {
            return indices.Select(index => arr[index]).Min();
        }
    }
}
