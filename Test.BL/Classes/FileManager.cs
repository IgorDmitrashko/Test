using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using Test.BL.Interface;
using _Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace Test.BL
{
    public class FileManager : IFileManager
    {
        private List<string[]> _modelFiles;
        private List<IModelFile> _difference;
        private _Application _excel = new _Excel.Application();

        ModelFile model;
        Workbook wb;
        Worksheet ws;

        public FileManager() { }

        public FileManager(string path, int sheet) {
            wb = _excel.Workbooks.Open(path);
            ws = wb.Worksheets[1];
        }
        public List<IModelFile> GetXlsxFile(int starti, int starty, int endi, int endy) {

            try
            {
                Range range = (Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
                object[,] holder = range.Value2;
                string[,] xlsxFile = new string[endi - starti, endy - starty];


                for(int i = 1;i <= endi - starti;i++)
                {

                    for(int j = 1;j <= endy - starty;j++)
                    {
                        if(holder[i, j] != null)
                        {
                            xlsxFile[i - 1, j - 1] = holder[i, j].ToString();
                        }

                    }
                }

                double sum;
                List<IModelFile> content = new List<IModelFile>();
                for(int i = 0;i < endi - 1;i++)
                {
                    model = new ModelFile();
                    if(xlsxFile[i, 0] != null)
                    {
                        model.AccountNumber = xlsxFile[i, 0];
                        model.Currency = xlsxFile[i, 1];

                        if(double.TryParse(xlsxFile[i, 2], out sum))
                        {
                            model.Sum = double.Parse(xlsxFile[i, 2]);
                        }

                        content.Add(model);
                    }
                }
                wb.Close();
                return content;
            }
            catch(Exception ex)
            {
                List<IModelFile> models = new List<IModelFile>();
                model = new ModelFile();
                model.AccountNumber = ex.Message;
                model.Currency = ex.Message;

                return models;
            }

        }     

        public List<IModelFile> GetCsvFile(string path) {
            using(StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                _modelFiles = new List<string[]>();
                string[] headers = sr.ReadLine().Split(',');
                _modelFiles.Add(headers);

                while(!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    string[] content = new string[rows.Length];
                    for(int i = 0;i < headers.Length;i++)
                    {
                        content[i] = rows[i];
                    }
                    _modelFiles.Add(content);
                }
            }
            List<IModelFile> mf = new List<IModelFile>();
            ModelFile model;
            double difference;

            foreach(var item in _modelFiles)
            {
                model = new ModelFile();

                if(double.TryParse(item[0], out difference))
                {
                    model.Sum = double.Parse(item[0]);
                }
                model.AccountNumber = item[1];
                model.Currency = item[2];
                mf.Add(model);
            }
            return mf;
        }

        public List<IModelFile> GetDifferenceSum(List<IModelFile> csvFile, List<IModelFile> xlsxFile) {
            ModelFile model;
            List<IModelFile> model1 = new List<IModelFile>();
            _difference = new List<IModelFile>();
            for(int i = 0;i < csvFile.Count;i++)
            {

                if(csvFile[i].Sum - xlsxFile[i].Sum > 0)
                {
                    model = new ModelFile();

                    model.Sum = csvFile[i].Sum - xlsxFile[i].Sum;
                    model.AccountNumber = csvFile[i].AccountNumber;
                    model.Currency = csvFile[i].Currency;
                    model1.Add(model);
                }
            }
            return model1;
        }
    }
}
