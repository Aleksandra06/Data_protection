using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using DataProtection.PageModels;
using Microsoft.AspNetCore.Components;

namespace DataProtection.Pages.RgzTwo
{
    public class RgzTwoViewModel : ComponentBase
    {
        protected List<string> FileErrorsList { get; set; }
        protected bool ShowProgressBar { get; set; } = false;
        protected double ProgressValue { get; set; }
        protected DocumentModel Document { get; set; }

        protected bool mIsReadFile = false;
        //-----
        protected int n;
        protected int m;

        private static List<Edge> edges = new List<Edge>();
        //-----

        protected async void HandleFileSelected(IFileListEntry[] files)
        {
            FileErrorsList = new List<string>();
            mIsReadFile = false;
            edges = new List<Edge>();

            if (files != null && files.Count() > 0)
            {
                double step = (double)1 / files.Count();
                var file = files.FirstOrDefault();
                try
                {
                    var doc = await UploadFile(file);
                    if (doc != null)
                    {
                        Document = doc;
                        string textFromFile = System.Text.Encoding.Default.GetString(Document.Data);
                        GetDataFromFile(textFromFile);
                    }
                    mIsReadFile = true;
                }
                catch (Exception e)
                {
                    FileErrorsList.Add($"Файл: {file.Name} не загружен, потому что {e.Message}");
                }
                finally
                {
                    ProgressValue += step;
                    StateHasChanged();
                }
            }
            StateHasChanged();
        }

        private async Task<DocumentModel> UploadFile(IFileListEntry file)
        {
            if (file != null)
            {
                byte[] result;
                MemoryStream sourceStream = await file.ReadAllAsync((int)file.Size);
                result = new byte[file.Data.Length];
                await sourceStream.ReadAsync(result, 0, (int)file.Data.Length);

                DocumentModel doc = new DocumentModel()
                {
                    FileName = file.Name,
                    Data = result,
                    ContentType = file.Type
                };

                return doc;

            }
            return null;
        }
        private void GetDataFromFile(string str)
        {
            str = str.Replace("\r", "");
            var strTmp = str.Substring(0, str.IndexOf("\n"));
            var indexNumber = str.IndexOf(",") >= 0 ? str.IndexOf(",") : str.IndexOf(" ");
            var strNumber = str.Substring(0, indexNumber);
            n = Convert.ToInt32(strNumber);
            strNumber = strTmp.Substring(indexNumber + 1).Replace(" ", "");
            m = Convert.ToInt32(strNumber);
            if (n >= 1001 || m >= Math.Pow(n, 2) || n <= 0 || m <= 0)
            {
                throw new Exception("ошибочные данные в первой строке!");
            }

            strTmp = str.Substring(str.IndexOf("\n") + 1).Replace(" ", "");
            while (!string.IsNullOrEmpty(strTmp.Replace(" ", "").Replace(",", "")))
            {
                string strStr;
                var indexStr = strTmp.IndexOf("\n");
                if (indexStr >= 0)
                {
                    strStr = strTmp.Substring(0, indexStr).Replace(" ", "");
                    strTmp = strTmp.Substring(indexStr + 1);
                }
                else
                {
                    strStr = strTmp;
                    strTmp = "";
                }
                var tmpMas = strStr.Split(",");
                edges.Add(new Edge()
                {
                    vertex1 = Convert.ToInt32(tmpMas[0]),
                    vertex2 = Convert.ToInt32(tmpMas[1]),
                    color1 = Convert.ToInt32(tmpMas[2]),
                    color2 = Convert.ToInt32(tmpMas[3]),
                });
            }
        }
    }
}