using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public interface ITOSService
    {
        string uploadTOS();

        void createTOS();

        bool isExistedTOS(string filePath);

        string getDocumentFilePath();
    }
}
