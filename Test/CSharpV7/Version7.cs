using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CSharpV7
{
    public class Version7
    {
        public Version7()
        {
            string input = "3";
            //int result; no more needed
            if (int.TryParse(input, out int result)) Console.WriteLine(result.ToString()); //no more needed to declare the variable before using out parameter.

            var letters = ("a", "b"); //tuple
            Console.WriteLine(letters.Item1);
            (string Alpha, string Beta) namedLetters = ("a", "b");
            Console.WriteLine(namedLetters.Alpha);
            (string First, string Second) firstLetters = (Alpha: "a", Beta: "b"); //left wins on right
            Console.WriteLine(firstLetters.First);
            List<int> ints = new List<int>() { 1, 2, 3, 4 };
            (int Max, int Min) values = Range(ints);
            Console.WriteLine("Max: " + values.Max.ToString());
            Console.WriteLine("Min: " + values.Min.ToString());
            var p = new Point(3.14, 2.71);
            (double X, double Y) = p; //combo from parameter out and tuple, you can instanciate and assign X as double getting value from instance of Point p (using deconstruct in Point)
            (double AnotherX, double AnotherY) = p;  //you can use another name for your parameter
            Console.WriteLine(X.ToString());
            Console.WriteLine(AnotherX.ToString());
            var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010); //you can assign to everyone var or the same class before parenthesis
            // with _ you can escape value and discard it if you need only pop1 and pop2

            List<object> diceList = new List<object>()   //list for Pattern Matching sample
            {
                3,
                new List<object>(){1,2,3},
                new List<AGameOfDice.PercentileDice>(){new AGameOfDice.PercentileDice(10,2), new AGameOfDice.PercentileDice(20, 3) },
                new List<object>(){2,1,6},
                new List<object>(),
                0,
            };
            Console.WriteLine("Sum of dice: " + AGameOfDice.DiceSum(diceList).ToString());

            int[,] bigMatrix = new int[4, 2] { { 21, 32 }, { 4, 12 }, { 7, 9 }, { 1, 10 } };
            ref int referencedBack = ref FindInMatrix(bigMatrix, (x) => x.Equals(32)); //to get a reference only of a little part of the entire data
            referencedBack = 100;
            Console.WriteLine("We're changed it: " + bigMatrix[0, 1].ToString());

            IEnumerable<char> alphabetSubset = AlphabetSubset('a', 'g');  //sample of private method in public method, improve understanding of code and flow
            Console.WriteLine("Count: " + alphabetSubset.Count().ToString());

            ExpressionMembersExample expressionMembersExample = new ExpressionMembersExample("Test");  //expression in member, more easy to make a constructor
            Console.WriteLine(expressionMembersExample.ToString());

            ValueTaskSample valueTaskSample = new ValueTaskSample();
            ValueTask<int> valueTask = valueTaskSample.CachedFunc();
            Task<int> valueTask2 = valueTask.AsTask();
            int value = valueTask2.Result;
            int value2 = valueTaskSample.CachedFunc().Result;

            Console.WriteLine("Better Syntax: " + BetterSyntax.AvogadroConstant.ToString());
        }
        //method that returns a tuple
        private static (int Max, int Min) Range(IEnumerable<int> numbers)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var n in numbers)
            {
                min = (n < min) ? n : min;
                max = (n > max) ? n : max;
            }
            return (max, min);
        }
        private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
        {
            int population1 = 0, population2 = 0;
            double area = 0;

            if (name == "New York City")
            {
                area = 468.48;
                if (year1 == 1960)
                {
                    population1 = 7781984;
                }
                if (year2 == 2010)
                {
                    population2 = 8175133;
                }
                return (name, area, year1, population1, year2, population2);
            }

            return ("", 0, 0, 0, 0, 0);
        }
        //method that returns a reference
        public static ref int FindInMatrix(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException(message: "Not found");
        }
        //method with an internal private method, improve understanding of code and flow
        public static IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImplementation();

            IEnumerable<char> alphabetSubsetImplementation()
            {
                for (var c = start; c < end; c++)
                    yield return c;
            }
        }
    }
    public class Point
    {
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X { get; }
        public double Y { get; }
        public void Deconstruct(out double x, out double y)
        {
            x = this.X;
            y = this.Y;
        }
    }
    //class for Pattern Matching sample
    public class AGameOfDice
    {
        public struct PercentileDice
        {
            public int OnesDigit { get; }
            public int TensDigit { get; }

            public PercentileDice(int tensDigit, int onesDigit)
            {
                this.OnesDigit = onesDigit;
                this.TensDigit = tensDigit;
            }
        }
        public static int DiceSum(IEnumerable<object> values)
        {
            var sum = 0;
            foreach (var item in values)
            {
                switch (item)
                {
                    case 0:
                        Console.WriteLine("Zero found");
                        break;
                    case int val:
                        sum += val;
                        Console.WriteLine("int found");
                        break;
                    case PercentileDice dice:
                        sum += dice.TensDigit + dice.OnesDigit;
                        Console.WriteLine("Percentile Die found");
                        break;
                    case IEnumerable<object> subList when subList.Any():  //enter here only if the sublist has items
                        sum += DiceSum(subList);
                        Console.WriteLine("Sublist found");
                        break;
                    case IEnumerable<object> subList:
                        Console.WriteLine("Empty Sublist Found");
                        break;
                    case null:
                        Console.WriteLine("Null found");
                        break;
                    default:
                        Console.WriteLine("Unknown item type");
                        break;
                }
            }
            //var sum = 0;
            //foreach (var item in values)
            //{
            //    if (item is int val)
            //        sum += val;
            //    else if (item is IEnumerable<object> subList)
            //        sum += DiceSum2(subList);
            //}
            //return sum;
            return sum;
        }
    }
    public class ExpressionMembersExample
    {
        public ExpressionMembersExample(string label) => this.Label = label; // Expression-bodied constructor
        ~ExpressionMembersExample() => Console.WriteLine("Finalized!"); // Expression-bodied finalizer
        private string label;
        public string Label // Expression-bodied get / set accessors.
        {
            get => label;
            set => this.label = value ?? "Default label";
        }
        private string name;
        public string Name
        {
            get => name;
            set => name = value ??
                throw new ArgumentNullException(paramName: nameof(value), message: "New name must not be null");
        }
        public override string ToString() => $"{Label}-{Name}";
        private string surname = GetSurname() ?? throw new InvalidOperationException("Could not load surname");  //if null it's possible to throw a new exception
        private static string GetSurname() { return "Surname"; }
    }
    public class ValueTaskSample  //to use ValueTask download from nuget System.Threading.Tasks.Extensions
    {
        public ValueTask<int> CachedFunc()
        {
            Console.WriteLine("Cached Func");
            return (cache) ? new ValueTask<int>(cacheResult) : new ValueTask<int>(LoadCache());
        }
        private bool cache = false;
        private int cacheResult;
        private async Task<int> LoadCache()
        {
            // simulate async work:
            await Task.Delay(100);
            cacheResult = 100;
            cache = true;
            Console.WriteLine("Load Cache");
            return cacheResult;
        }
        //https://stackoverflow.com/questions/43000520/why-would-one-use-taskt-over-valuetaskt-in-c
    }
    //Numeric literal syntax improvements
    public class BetterSyntax
    {
        public const int One = 0b0001;
        public const int Two = 0b0010;
        public const int Four = 0b0100;
        public const int Eight = 0b1000;
        public const int Sixteen = 0b0001_0000;
        public const int OneHundredTwentyEight = 0b_1000_0000; //from C# 7.2 is possible to add _ after 0b
        public const long BillionsAndBillions = 100_000_000_000;
        public const double AvogadroConstant = 6.022_140_857_747_474e23;
        public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
    }
}
