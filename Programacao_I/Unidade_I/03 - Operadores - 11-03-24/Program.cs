Write("Operação: ");
string? operador = ReadLine();

float operacao(float i, string? op, float j){
    if(op == "*"){return i * j;}
    else if(op == "/"){return MathF.Round(i / j, 2);}
    else if(op == "+"){return i + j;}
    else if(op == "-"){return i - j;}
    else return -1;
}
WriteLine("");

for(int i = 1; i < 10; i++){
    for(int j = 1; j < 10; j++){
        Write($"{j}{operador}{i} = {operacao(j, operador, i), -4}   ");
    }
    WriteLine("");
};
WriteLine("");