using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.BL;

namespace Test
{
    public interface IMainWindow
    {
        string Path { get; set; }
        List<IModelFile> Csv { get; set; }
        List<IModelFile> Xlsx { get; set; }
        List<IModelFile> Difference { get; set; }

        void DgDbHistirySet(List<IModelFile> value);

        event EventHandler CsvFile;
        event EventHandler XlsxFile;
        event EventHandler FilesDifference;
    }
}
