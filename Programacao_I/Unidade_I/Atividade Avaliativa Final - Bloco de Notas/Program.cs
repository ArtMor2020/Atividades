// Sistema de gerenciamento de tarefas.

using Atividade_Avaliativa_Final.Models;
using Atividade_Avaliativa_Final.Program.Views;

bool aux = true;

do
{
    Console.WriteLine("""

        Organizador de Tarefas
        **********************

        1 - Ver Tarefas
        2 - Adicionar Tarefas
        3 - Mover Tarefa
        4 - Remover Tarefas
        5 - Importar/Exportar Lista de Tarefas
        0 - Sair

    """);

    int menu;

    try
    {
        Console.Write("--> ");
        menu = Convert.ToInt32(Console.ReadLine());

        switch(menu)
        {

        case 1 or 2 or 3 or 4 or 5:
            TaskViews taskViews = new TaskViews ( menu );
        break;

        case 0:
            Console.WriteLine("""

                Saíndo do Programa

            """);

            aux = false;
        break;
        }
    }
    catch
    {
        Console.WriteLine("""

            Opção Invalida! Tente Novamente. 
        
        """);
    }
} 
while (aux);