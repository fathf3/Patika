
Console.Write("Sayi Giriniz : ");
int a = int.Parse(Console.ReadLine());
int j, k;
for (int i = 0; i < a; i++)
{

    for (j = 1; j <=a-i ; j++)
    {
        Console.Write(" ");
    }
    for (k = 1; k<=i+1; k++) 
    { 
        Console.Write("*"); 
    }

    Console.WriteLine();

}