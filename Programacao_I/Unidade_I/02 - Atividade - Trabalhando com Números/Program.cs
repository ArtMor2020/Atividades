//Calculadora
using System;

Console.WriteLine("Calculadora" + "\n");
Console.Write("Primeiro Numero: "); float FirstNumber = float.Parse(Console.ReadLine());  // ou Convert.ToDouble(Console.ReadLine())
Console.Write("Operador: "); string? Operator = Console.ReadLine();
Console.Write("Segundo Numero: "); float SecondNumber = float.Parse(Console.ReadLine());
float result = 0;

Console.WriteLine(" ");

if(Operator == "+"){result = FirstNumber + SecondNumber;}
else if(Operator == "-"){result = FirstNumber - SecondNumber;}
else if(Operator == "x" || Operator == "*"){result = FirstNumber * SecondNumber;}
else if(Operator == "/"){result = FirstNumber / SecondNumber;}

Console.WriteLine($"{FirstNumber} {Operator} {SecondNumber} = {result} \n");
