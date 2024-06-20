using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Avaliativa_Final.Program.Repository;
using Atividade_Avaliativa_Final.Program.Utils;


namespace Atividade_Avaliativa_Final.Program.Controllers
{
    public class TaskController
    {
        private TaskRepository taskRepository;

        public TaskController()
        {
            taskRepository = new TaskRepository();
        }

        public void Insert(Models.Task task)
        {
            bool dupe = false;

            for(int i = 0; i < Data.DataSet.Tasks.Count(); i++)
            {
                if( Data.DataSet.Tasks[i].id == task.id )
                {
                    dupe = true;
                }
            }

            if(!dupe)
            {
                this.taskRepository.Save(task);
                Console.WriteLine();
                Console.WriteLine("    Tarefa inserida com sucesso!.");
            }
            else
            {
                int id = task.id;
                task.id = 0;
                this.taskRepository.Save(task);

                this.taskRepository.Move( Data.DataSet.Tasks.Count(), id);
                Console.WriteLine();
                Console.WriteLine("    Tarefa inserida com sucesso!.");
            }
        }

        public List<Models.Task> Get()
        {
            return taskRepository.Read();
        }

        public void Delete(int id)
        {
            this.taskRepository.Remove(id);
            Console.WriteLine();
            Console.WriteLine($"    Tarefa {id} apagada.");
        }

        public bool ExportToDelimited(string dir)
        {
            List<Models.Task> list = taskRepository.Read();

            string file_content = string.Empty;

            foreach( var c in list )
            {
                file_content += $"{c.PrintToExportDelimited()}\n";
            }

            string file_name = $"Task_{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.txt";
            ExportToFile.SaveToDelimitedText(file_name, file_content, dir);
            return true;
        }

        public void MoveTo(int origin, int destiny)
        {
            this.taskRepository.Move( origin, destiny );
        }

        public string ImportFromDelimited(string file_path, string delimiter)
        {
            bool result = true;
            string msgReturn = string.Empty;
            int lineCountSuccess = 0;
            int lineCountError = 0;
            int lineCountTotal = 0;

            try
            {
                if(!File.Exists(file_path))
                {
                    Console.WriteLine();
                    return "    Erro: Arquivo Não Encontrado.";
                }

                using(StreamReader sr = new StreamReader(file_path))
                {
                    string line = string.Empty;
                    while((line = sr.ReadLine()) != null)
                    {
                        lineCountTotal ++;

                        if(!taskRepository.ImportFromTxt( line, delimiter ))
                        {
                            result = false;
                            lineCountError ++;
                        }
                        else
                        {
                            lineCountSuccess ++;
                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine();
                msgReturn = $"    Erro: {ex.Message}";
                return msgReturn;

            }

            if(result)
                msgReturn = "    Data Importada com Sucesso.";
            else
                msgReturn = "    Data Importada Paricialmente.";
            
            msgReturn += $"\n    Total de linhas: {lineCountTotal}";
            msgReturn += $"\n    Sucessos: {lineCountSuccess}";
            msgReturn += $"\n    Falhas: {lineCountError}";

            return msgReturn;
        }
    }
}