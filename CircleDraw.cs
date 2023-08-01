Console.Write("Dairenin yarıçapını girin: ");
int yaricap = int.Parse(Console.ReadLine());

for (int x = -yaricap; x <= yaricap; x++)
{
    for (int y = -yaricap; y <= yaricap; y++)
    {
        
        if (x * x + y * y <= yaricap * yaricap)
        {
            Console.Write(" ");
        }
        else
        {
            Console.Write("*");
        }
    }
    Console.WriteLine();
}

Console.ReadKey();