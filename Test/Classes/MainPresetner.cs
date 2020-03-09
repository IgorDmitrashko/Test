using System;
using Test.BL.Interface;

namespace Test
{
    class MainPresetner
    {
        IMainWindow mainWindow;
        IFileManager fileManager;

        public MainPresetner(IMainWindow mainWindow, IFileManager fileManager) {
            this.mainWindow = mainWindow;
            this.fileManager = fileManager;

            mainWindow.CsvFile += GetCsvFile;
            mainWindow.XlsxFile += GetXlsxFile;
            mainWindow.FilesDifference += Difference;

        }

        private void Difference(object sender, EventArgs e) {
            mainWindow.Difference = fileManager.GetDifferenceSum(mainWindow.Csv, mainWindow.Xlsx);
            mainWindow.DgDbHistirySet(mainWindow.Difference);
        }

        private void GetCsvFile(object sender, EventArgs e) {
            mainWindow.Csv = fileManager.GetCsvFile(mainWindow.Path);
        }

        private void GetXlsxFile(object sender, EventArgs e) {
            mainWindow.Xlsx = fileManager.GetXlsxFile(1, 1, 452, 4);
            /*
               Не смог получить фактические числа таблицы.  
               Я понимаю, что если изменить колличество строк, то будут не вcе данные.
               Не смог решить эту проблему.
            */
        }

    }
}
