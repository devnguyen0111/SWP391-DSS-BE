using Microsoft.Extensions.Configuration;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public class TOSService : ITOSService
    {
        private readonly IConfiguration _config;
        public TOSService(IConfiguration config)
        {
            this._config = config;
        }

        public string uploadTOS()
        {
            string filePath = getDocumentFilePath();
            return File.ReadAllText(filePath);
        }

        public void createTOS()
        {
            string filePath = getDocumentFilePath();

            if(!isExistedTOS(filePath))
            {
                // create doc's context
                DocumentCore dc = new DocumentCore();
                dc.Content.End.Insert(_config.GetSection("TOSCreateFileSubject").Value);

                dc.Save(filePath, new TxtSaveOptions()
                {
                    Encoding = Encoding.UTF8,
                    ParagraphBreak = Environment.NewLine
                });

                // Open the result for demonstration purposes.
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }

        public bool isExistedTOS(string filePath)
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }  
        }

        public string getDocumentFilePath()
        {
            //get the environment path and document
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Define the file name
            string fileName = _config.GetSection("TOSFileName").Value;

            // Combine the path and file name
            string filePath = Path.Combine(documentsPath, fileName);

            return filePath;
        }
    }
}
