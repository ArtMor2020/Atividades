using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atividade_Avaliativa_Final.Models
{
    public class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string due_date  { get; set; }
        public string status { get; set; }

        public string PrintToExportDelimited()
        {
            return $"{id}-{title}-{description}-{due_date}-{status}";
        }

        public override string ToString()
        {
            return $"    {id} - {title} - {description} - {due_date} - {status}";
        }
    }
}