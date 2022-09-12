namespace delegate_practice
{
    internal class Program
    {
        public delegate float OperationDelegate(float a, float b);
        public delegate bool FilterDelegate(Person p);
        static void Main(string[] args)
        {
            Person p1 = new Person() { Name = "Aiden", Age = 41 };
            Person p2 = new Person() { Name = "Sif", Age = 69 };
            Person p3 = new Person() { Name = "Walter", Age = 12 };
            Person p4 = new Person() { Name = "Anatoli", Age = 25 };

            List<Person> people = new List<Person>() { p1, p2, p3, p4 };

            DisplayPeople("kids", people, IsMinor);
            DisplayPeople("Adults", people, IsAdult);
            DisplayPeople("Seniors", people, IsSenior);

            //This is an anonymous method. It has no title and is just called by the instansiated name.  It is an instansiation of the Filter Delegate.  Dont forget the semicolan
            FilterDelegate filter = delegate (Person p)
            {
                return p.Age >= 20 && p.Age <= 30;
            };

            DisplayPeople("Young Adult", people, filter);

            DisplayPeople("all:", people, delegate (Person p) { return true; });
            //Statment Lamda expression
            string searchKeyword = "A";
            DisplayPeople("age > 20 with search keyword:" + searchKeyword, people, p => {
                if (p.Name.Contains(searchKeyword) && p.Age > 20)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            DisplayPeople("exactly 25", people, p => p.Age == 25);

            Console.WriteLine(ApplyOperator(3, 55, Add));
        }
        static void DisplayPeople(string title, List<Person> people, FilterDelegate filter)
        {
            Console.WriteLine(title);
            foreach (Person p in people)
            {
                if (filter(p))
                {
                    Console.WriteLine("{0}, {1} years old", p.Name, p.Age);
                }
            }
        }
        static bool IsMinor(Person p)
        {
            return p.Age < 18;
        }
        static bool IsAdult(Person p)
        {
            return p.Age >= 18;
        }
        static bool IsSenior(Person p)
        {
            return p.Age >= 65;
        }

        public static float Add(float a, float b)
        {
            return a + b;
        }
        public static float Sub(float a, float b)
        {
            return a - b;
        }
        public static float Multi(float a, float b)
        {
            return a * b;
        }
        public static float Divide(float a, float b)
        {
            return a / b;
        }
        public static float ApplyOperator(float a, float b, OperationDelegate operation)
        {
            float result;
            result = operation(a, b);
            return result;
        }

        public class Run
        {
            static Func<float, float, float> Plus = (a, b) => a + b;
            static Func<float, float, float> Minus = (a, b) => a - b;
            static Func<float, float, float> Multiply = (a, b) => a * b;
            static Func<float, float, float> Divide = (a, b) => a / b;



            static public Dictionary<string, Func<float, float, float>> Operators = new Dictionary<string, Func<float, float, float>>
                {
                {"+", Plus },
                {"-", Minus },
                {"*", Multiply },
                {"/", Divide }
                };

            public static Func<float, float, float>OperationGet(string s)
            {
                if (Operators.ContainsKey(s))
                {
                    return Operators[s];
                }
                return null;
            }
        }
        
        




    }
}