Console.Write("Kac Adet sayi gireceksiniz :  ");
int n = int.Parse(Console.ReadLine());
List<int> x = new List<int>();
for (int i =0; i<n; i++)
{
    Console.Write((i+1)+". sayi : ");
    x.Add(int.Parse(Console.ReadLine()));
    Console.WriteLine();
}

int fark = 0;
double buyukFark = 0;

for (int i = 0; i < n; i++)
{
    if (x[i] < 67)
        fark += 67 - x[i];
    else
    {
        buyukFark += Math.Pow(x[i] - 67, 2);
    }
}

Console.WriteLine("Fark " + fark);
Console.WriteLine("Buyuk Fark " + buyukFark);






