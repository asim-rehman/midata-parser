using MiData.Core;

namespace MiData.Models
{
    public class SettingsModel
    {
        public string OutputPath { get; set; } = "C:\\MiData-Out\\";
        public string InputPath { get; set; } = "C:\\MiData.csv";
        public FileFormat Format { get; set; } = FileFormat.Excel;
        public char Delimiter { get; set; } = ';';
        public bool Log { get; set; } = false;
        public bool Display { get; set; } = true;
        public string LogDirectory { get; set; } = "C:\\MiData-Log.txt";
        public string ModelFile { get; set; } = "Model.json";
    }
}
