Console.WriteLine("Verilen string ifade içerisindeki karakterleri bir önceki karakter ile yer değiştiren console uygulamasını yazınız.\r\n\r\nÖrnek: Input: Merhaba Hello Question\r\n\r\nOutput: erhabaM elloH uestionQ");
Console.Write("String : ");
string x = Console.ReadLine();

string[] y = x.Split(' ');

for(int i=0; i<y.Length; i++)
{
    string a = y[i];
    char b = a[0];
    a = a + b;
    a = a.Substring(1, a.Length - 1);
    Console.WriteLine(a);
    
}

