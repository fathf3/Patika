
Console.WriteLine("Ekrandan girilen n tane integer ikililerin toplamını alan, eğer sayılar birbirinden farklıysa toplamlarını ekrana yazdıran, sayılar aynıysa toplamının karesini ekrana yazdıran console uygulamasını yazınız.\r\n\r\nÖrnek Input: 2 3 1 5 2 5 3 3\r\n\r\nOutput: 5 6 7 81\n");

Console.Write("Kac Adet sayi gireceksiniz :  ");
int n = int.Parse(Console.ReadLine());
List<int> x = new List<int>();
for (int i =0; i<n; i++)
{
    Console.Write((i+1)+". sayi : ");
    x.Add(int.Parse(Console.ReadLine()));
    Console.WriteLine();
}


List<int> y = new List<int>();
for (int i = 0; i < n; i += 2)
{
    if (x[i] == x[i + 1])
        y.Add((int)Math.Pow(x[i]*2,2));
    else
    {
        y.Add(x[i] + x[i+1]);
    }
}

foreach (int i in y)
    Console.Write(" " + i);






