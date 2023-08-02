

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# Console uygulaması oluşturarak aşağıdaki gereksinimleri karşılayan uygulamayı yazınız.\r\n\r\nİşlem yapılmak istenen geometrik şekli kullanıcıdan alınmalı (Daire, Üçgen, Kare, Dikdörtgen vb..)\r\nSeçilen şekle göre, kenar bilgilerin kullanıcıdan alınmalı\r\nHesaplanmak istenen boyutu kullanıcıdan alınmalı (Çevre, Alan, Hacim vb..)\r\nHesap sonucunu anlaşılır şekilde geri döndürmeli.\r\nDikkat Edilmesi Gereken Noktalar :\r\n\r\nKod tekrarından kaçınılmalı\r\nSingle Responsibility kuralına uygun şekilde, uygulama sınıflara ve metotlara bölünmeli. \n\n");
            
            Console.Write("Şekil Seçiniz (Daire, Üçgen, Kare, Dikdörtgen): ");

            string shapeType = Console.ReadLine();

            IShape shape;
            switch (shapeType.ToLower())
            {
                case "daire":
                    shape = new Circle();
                    break;
                case "üçgen":
                    shape = new Triangle();
                    break;
                case "kare":
                    shape = new Square();
                    break;
                case "dikdörtgen":
                    shape = new Rectangle();
                    break;
                default:
                    Console.WriteLine("Geçersiz geometrik şekil seçimi!");
                    return;
            }

            Console.Write("\n\nŞeklin kenar bilgisini giriniz:");
            double[] sides = new double[shape.NumOfSides];
            for (int i = 0; i < shape.NumOfSides; i++)
            {
                Console.Write($"Kenar {i + 1}: ");
                sides[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.Write("Hesaplamak istediğiniz değeri seçiniz (Çevre, Alan) : ");
            string calculationType = Console.ReadLine();

            double result = shape.Calculate(calculationType.ToLower(), sides);
            Console.WriteLine($"Sonuç: {result}");
        }
    }

    interface IShape
    {
        int NumOfSides { get; }
        double Calculate(string calculationType, double[] sides);
    }

    class Circle : IShape
    {
        public int NumOfSides => 1;

        public double Calculate(string calculationType, double[] sides)
        {
            double radius = sides[0];

            switch (calculationType)
            {
                case "çevre":
                    return 2 * Math.PI * radius;
                case "alan":
                    return Math.PI * radius * radius;
                default:
                    throw new ArgumentException("Geçersiz hesaplama tipi!");
            }
        }
    }

    class Triangle : IShape
    {
        public int NumOfSides => 3;

        public double Calculate(string calculationType, double[] sides)
        {
            double a = sides[0];
            double b = sides[1];
            double c = sides[2];

            switch (calculationType)
            {
                case "çevre":
                    return a + b + c;
                case "alan":
                    double s = (a + b + c) / 2;
                    return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                default:
                    throw new ArgumentException("Geçersiz hesaplama tipi!");
            }
        }
    }

    class Square : IShape
    {
        public int NumOfSides => 1;

        public double Calculate(string calculationType, double[] sides)
        {
            double side = sides[0];

            switch (calculationType)
            {
                case "çevre":
                    return 4 * side;
                case "alan":
                    return side * side;
                default:
                    throw new ArgumentException("Geçersiz hesaplama tipi!");
            }
        }
    }

    class Rectangle : IShape
    {
        public int NumOfSides => 2;

        public double Calculate(string calculationType, double[] sides)
        {
            double a = sides[0];
            double b = sides[1];

            switch (calculationType)
            {
                case "çevre":
                    return 2 * (a + b);
                case "alan":
                    return a * b;
                default:
                    throw new ArgumentException("Geçersiz hesaplama tipi!");
            }
        }
    }

