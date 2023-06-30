using System;
using System.Diagnostics;
using System.Xml.Linq;

Console.WriteLine("Set the price: ");
string price = Console.ReadLine();
string[] Input = price.Split(' ');
int y;
int x;
y = Int32.Parse(Input[0]);

int total = 0;
if (Input.Length > 1)

{
    x = Int32.Parse(Input[2]);
    switch (Input[1])
    {
        case "+":
        case "plus":
        case "add":
        case "added":
            total = y + x;
            break;
        case "*":
        case "multiplied":
        case "times":
            total = y * x;
            break;
        case "-":
        case "minus":
        case "subtract":
            total = y - x;
            break;
        case "/":
        case "divided":
        case "divide":
            total = y / x;
            break;
    }
}
else
{
    total = y;
}
Console.WriteLine($"The price was set to: {total}");






