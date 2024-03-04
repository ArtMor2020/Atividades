﻿Console.WriteLine( "\n" + "FICHA CADASTRARIA" + "\n" + "DIGITE OS DADOS REQUISITADOS ABAIXO:"+ "\n" );

Console.Write("NOME: "); string? NOME = Console.ReadLine();
Console.Write("DATA DE NASCIMENTO: "); string? DN = Console.ReadLine();
Console.Write("SEXO/GENERO: "); string? SG = Console.ReadLine();

Console.WriteLine( "\n" + "DADOS DO ENCEREÇO" + "\n" );

Console.Write("CEP: "); string? CEP = Console.ReadLine();
Console.Write("RUA: "); string? RUA = Console.ReadLine();
Console.Write("NUMERO: "); string? NUMERO = Console.ReadLine();
Console.Write("BAIRRO: "); string? BAIRRO = Console.ReadLine();
Console.Write("CIDADE: "); string? CIDADE = Console.ReadLine();
Console.Write("ESTADO: "); string? ESTADO = Console.ReadLine();
Console.Write("PAÍS: "); string? PAIS = Console.ReadLine();

Console.WriteLine(
    "\n" + "\n" +
    $""" 
    FICHA CADASTRARIA
    
    NOME: {NOME}, DATA DE NASCIMENTO: {DN}, SEX0/GENERO: {SG}
    CEP: {CEP}, ENDEREÇO: {RUA} - {NUMERO}, {BAIRRO}
    CIDADE: {CIDADE}, ESTADO: {ESTADO}, PAÍS: {PAIS}
    """
    + "\n" + "\n" 
);