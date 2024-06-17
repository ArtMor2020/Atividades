using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Atividade_Avaliativa_Final.Program.Data;

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
                task.id = this.GetNextID();

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

        private int GetNextID()
        {
            int n = 0;
            foreach( var task in Data.DataSet.Tasks)
            {
                if ( task.id > n)
                    n = task.id;
            }
            return ++n;        
        }

        public bool ImportFromTxt (string line, string delimiter)
        {
            if ( string.IsNullOrWhiteSpace( line ) )
                return false;
            
            string[] data = line.Split(delimiter);

            if( data.Count() < 1 )
                return false;
            
            Models.Task task = new Models.Task{
                id = Convert.ToInt32( (data[0] == null ? -1 : data[0]) ),
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