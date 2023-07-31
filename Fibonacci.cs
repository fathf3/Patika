
static int Fibo(int number)
{
    if(number == 0) return 0;
    else if(number == 1) return 1;  
    return Fibo(number-1)+Fibo(number-2);
}

static float Average(int number)
{
    return Fibo(number)/number;
}


Console.WriteLine(Average(14));

    