namespace Services.OtherServices
{
    public interface IPINCode
    {
        string GeneratedPinCode();

        string PinCodeHtmlContent(string pinCode);
    }
}
