using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atividade_Avaliativa_Final.Program.Utils
{
    public class ExportToFile
    {
        public static bool SaveToDelimitedText(string file_name, string file_content, string dir)
        {
            string file_path = @$"{dir}\{file_name}";

            try
            {
                if(!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using(StreamWriter sw = File.CreateText(file_path))
                {
                    sw.Write(file_content);
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}