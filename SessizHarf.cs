Console.WriteLine("Verilen string ifade içerisindeki ilk ve son karakterin yerini değiştirip tekrar ekrana yazdıran console uygulamasını yazınız.\r\n\r\nÖrnek: Input: Merhaba Hello Algoritma x\r\n\r\nOutput: aerhabM oellH algoritmA x \n");
Console.Write("String : ");
string x = Console.ReadLine();

string unsuz = "bcçdfgğhjklmnprsştvyz";
string[] y = x.Split(' ');


for (int i = 0; i < y.Length; i++)
{
    bool sonuc = false;
    for(int j=0; j < y[i].Length-1; j++)
    {

        string s = y[i];
        if (unsuz.Contains(s[j]) && unsuz.Contains(s[j+1]))
        {
            sonuc = true;
            break;
        }
        

    }
    Console.Write(sonuc + " ");

}
Console.ReadLine();