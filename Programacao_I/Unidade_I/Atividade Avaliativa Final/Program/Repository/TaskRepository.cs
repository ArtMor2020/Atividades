using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Avaliativa_Final.Program.Data;
using Atividade_Avaliativa_Final.Program.Controllers;

namespace Atividade_Avaliativa_Final.Program.Repository
{
    public class TaskRepository
    {
        public void Create (Models.Task task)
        {
            Data.DataSet.Tasks.Add(task);
        }

        public void Save (Models.Task task)
        {
            if(task.id < 1)
                task.id = this.GetFirstAvailableID();

                Data.DataSet.Tasks.Add(task);
        }

        public List<Models.Task> Read()
        {
            return Data.DataSet.Tasks;
        }

        public void Remove(int id)
        {
            for (int t = Data.DataSet.Tasks.Count - 1; t >= 0; t--)
            {
                if (Data.DataSet.Tasks[t].id == id)
                {
                    Data.DataSet.Tasks.RemoveAt(t);
                }
            }
        }

        private int GetFirstAvailableID()
        {
            List<Models.Task> tasks = Data.DataSet.Tasks;
            
            tasks.Sort((task1, task2) => task1.id.CompareTo(task2.id));

            int firstAvailable = 1; 

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].id > firstAvailable)
                {
                    break;
                }
                firstAvailable = tasks[i].id + 1;
            }
        return firstAvailable;
        }

        public void Move(int origin, int destiny)
        {
            List<Models.Task> tasks = Data.DataSet.Tasks;
            Models.Task task = new Models.Task();

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].id == origin)
                {
                    task = tasks[i];
                    tasks.RemoveAt(i);
                    break; 
                }
            }

            for (int i = destiny - 1; i < tasks.Count; i++)
            {
                tasks[i].id = tasks[i].id + 1;
            }

            task.id = destiny;
            tasks.Insert(destiny - 1, task);

            Data.DataSet.Tasks = tasks;
        }

        public bool ImportFromTxt (string line, string delimiter)
        {
            if ( string.IsNullOrWhiteSpace( line ) )
                return false;
            
            string[] data = line.Split(delimiter);

            if( data.Count() < 1 )
                return false;
            
            Models.Task task = new Models.Task{
                id = GetFirstAvailableID(),
                title = (data[1] == null ? string.Empty : data [1]),
                description = (data[2] == null ? string.Empty : data [2]),
                due_date = (data[3] == null ? string.Empty : data[3]),
                status = (data[4] == null ? string.Empty : data[4])
            };

            Save(task);

            return true;
        }
    }
}