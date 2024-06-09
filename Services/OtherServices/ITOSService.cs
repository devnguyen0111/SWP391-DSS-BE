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
