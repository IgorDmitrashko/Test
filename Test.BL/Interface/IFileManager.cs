using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BL.Interface
{
    public interface IFileManager
    {
        List<IModelFile> GetCsvFile(string path);
        List<IModelFile> GetDifferenceSum(List<IModelFile> csvFile, List<IModelFile> xlsxFile);
        List<IModelFile> GetXlsxFile(int starti, int starty, int endi, int endy);
    }
}
