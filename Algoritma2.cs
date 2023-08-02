Console.WriteLine("Verilen string ifade içerisindeki ilk ve son karakterin yerini değiştirip tekrar ekrana yazdıran console uygulamasını yazınız.\r\n\r\nÖrnek: Input: Merhaba Hello Algoritma x\r\n\r\nOutput: aerhabM oellH algoritmA x \n");
Console.Write("String : ");
string x = Console.ReadLine();

string[] y = x.Split(' ');


for (int i = 0; i < y.Length; i++)
{
    string a = "";
    if (y[i].Length == 1)
    {
        a = y[i];
    }
    else { 
    a = y[i];
    char b = a[0];
    char c = a[a.Length - 1];
    a = c+ a.Substring(1,a.Length-1)+b;
    }
    Console.WriteLine(a);

}
Console.ReadLine();