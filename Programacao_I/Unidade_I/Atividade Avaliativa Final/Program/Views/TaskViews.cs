using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Avaliativa_Final.Program.Controllers;
using Atividade_Avaliativa_Final.Program.Repository;

namespace Atividade_Avaliativa_Final.Program.Views
{
    public class TaskViews
    {   
        private TaskController taskController;

        public TaskViews( int menu )
        {
            taskController = new TaskController();
            this.Init( menu );
        }

        public void Init(int menu)
        {
            try
            {
                switch( menu )
                {
                    case 1:
                        ListTasks();
                    return;

                    case 2:
                        InsertTask();
                    return;

                    case 3:
                        DeleteTask();
                    return;

                    case 4:
                        ImportExport();
                    return;

                }
            }
            catch (System.Exception ex )
            {
                Console.WriteLine(ex);
                Console.WriteLine("    Erro! Tente novamente.");
            }
        }

        private void ListTasks()
        {
            List<Models.Task> result = taskController.Get();

            Console.WriteLine();
            
            if(result == null || result?.Count == 0)
            {
                Console.WriteLine("    Sem Tarefas.");
                return;
            }

            result.Sort((task1, task2) => task1.id.CompareTo(task2.id));

            foreach( Models.Task task in result )
            {
                if(task.id != 0)
                Console.WriteLine(task.ToString());
            }
            Console.WriteLine();
        }

        private void InsertTask()
        {
            Console.WriteLine("""

                Inserir Tarefa
                **************
            
            """);

            Models.Task task = new Models.Task();

            Console.Write("    Numero (opcional): ");
            string temp = Console.ReadLine();
            if( !string.IsNullOrWhiteSpace(temp) )
                task.id = Convert.ToInt32(temp);
            
            Console.Write("    Titulo: ");
            task.title = Console.ReadLine();

            Console.Write("    Descrição: ");
            task.description = Console.ReadLine();

            Console.Write("    Prazo: ");
            task.due_date = Console.ReadLine();

            Console.Write("    Status: ");
            task.status = Console.ReadLine();

            try
            {
                taskController.Insert(task);
            }
            catch
            {
                Console.WriteLine("    Ops! Ocorreu um erro.");
            }
        }

        public void DeleteTask()
        {
            Console.WriteLine();
            Console.Write("    Digite o numero da tarefa a ser apagada: ");
            int id = Convert.ToInt32( Console.ReadLine());
            taskController.Delete(id);
        }
    
        public void ImportExport()
        {
            Console.WriteLine("""
            
                1 - Exportar
                2 - Importar
                0 - Voltar

            """);
            Console.Write("--> ");

            switch ( Convert.ToInt32( Console.ReadLine() ) )
            {
                case 0:
                break;

                case 1:
                    ExportToDelimited();

                break;

                case 2:
                    ImportFromDelimited();

                break;

                default:
                    Console.WriteLine("    Opção Invalida.");
                break;
            }
        }

        public void ExportToDelimited()
        {
            Console.WriteLine();
            Console.WriteLine("    Informe o caminho do arquivo:");
            string pathFile = Console.ReadLine();

            try
            {
                taskController.ExportToDelimited(pathFile);
                Console.WriteLine("    Exportação concluida.");
            }
            catch
            {
                Console.WriteLine("    Exportação falhou.");
            }
        }

        public void ImportFromDelimited()
        {
            Console.WriteLine();
            Console.WriteLine("    Informe o caminho do arquivo:");
            Console.Write("--> ");
            string pathFile = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("    Informe o caracter delimitador:");
            Console.Write("--> ");
            string delimiter = Console.ReadLine();
            Console.WriteLine();

            string response = taskController.ImportFromDelimited(pathFile, delimiter);

            Console.WriteLine(response);
        }
    }
}