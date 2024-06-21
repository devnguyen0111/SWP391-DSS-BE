using Model.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Services.EmailServices;

namespace Services.OtherServices
{
    public class EmailRelatedService : IEmailRelatedService
    {
        public string GeneratedPinCode()
        {
            Random random = new Random();
            int pin = random.Next(0, 1000000);
            return pin.ToString("D6");
        }

        public string PinCodeHtmlContent(string email, string pinCode)
        {
            return $@"<table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6"">
            <tbody>
            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                    valign=""top"">
                    <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;"">
                        <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;""
                               bgcolor=""#fff"">
                            <tbody>
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <td class="""" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 16px; vertical-align: top; color: #fff; font-weight: 500; text-align: center; border-radius: 3px 3px 0 0; background-color: #d8b993; margin: 0; padding: 20px;""
                                    align=""center"" bgcolor=""#71b6f9"" valign=""top"">
                                    <a href=""https://cosmodiamond.xyz/"" style=""font-size:32px;color:#fff;"">COSMOS Diamond Shop</a> <br>
                                    <span style=""margin-top: 10px;display: block;"">Warning: You have requested for PIN code. Do not share this PIN to anyone, or you will likely regret it!</span>
                                </td>
                            </tr>
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                        <tbody>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                Hello <strong style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">{email}</strong>,
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                Here is your <strong style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">6-digit PIN CODE</strong> to access to the next step.
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                You have requested for PIN code. Please copy or take note of the following 6-digit PIN code to the specific page for further elaborating.
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                <span class=""btn-primary"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #d8b993; margin: 0; border-color: #d8b993; border-style: solid; border-width: 8px 16px;"">{pinCode}</span>
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                Thanks for choosing <b>Cosmos Diamond Shop</b>! We hope you have a good day.
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            </tbody>
                        </table>
                        <div class=""footer"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;"">
                            <table width=""100%"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <tbody>
                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                    <td class=""aligncenter content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;"" align=""center"" valign=""top""><a href=""#"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; color: #999; text-decoration: underline; margin: 0;"">Contact us</a> through email.
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
                <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
            </tr>
            </tbody>
        </table>";
        }

        public string ResetPasswordContent(string email, string resetUrl)
        {
            return $@"<table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6"">
            <tbody>
            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
                <td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;""
                    valign=""top"">
                    <div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;"">
                        <table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;""
                               bgcolor=""#fff"">
                            <tbody>
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <td class="""" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 16px; vertical-align: top; color: #fff; font-weight: 500; text-align: center; border-radius: 3px 3px 0 0; background-color: #d8b993; margin: 0; padding: 20px;""
                                    align=""center"" bgcolor=""#71b6f9"" valign=""top"">
                                    <a href=""https://cosmodiamond.xyz/"" style=""font-size:32px;color:#fff;"">COSMOS Diamond Shop</a> <br>
                                    <span style=""margin-top: 10px;display: block;"">Warning: You have requested to change you password. If this isn't you, please check again!</span>
                                </td>
                            </tr>
                            <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                        <tbody>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                Hi <strong style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">{email}</strong>,
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                You have <strong style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">registered</strong> to our system.
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                You have requested to reset your password. If this is you, please click to the button below to redirect to the reset password page!
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                <a href=""{resetUrl}"" class=""btn-primary"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #d8b993; margin: 0; border-color: #d8b993; border-style: solid; border-width: 8px 16px;"">Reset Password</a>
                                            </td>
                                        </tr>
                                        <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                            <td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
                                                Thanks for choosing <b>Cosmos Diamond</b> Admin!
                                            </td>
                                        </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            </tbody>
                        </table>
                        <div class=""footer"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;"">
                            <table width=""100%"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                <tbody>
                                <tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
                                    <td class=""aligncenter content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;"" align=""center"" valign=""top""><a href=""#"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; color: #999; text-decoration: underline; margin: 0;"">Contact us</a> through email.
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
                <td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
            </tr>
            </tbody>
        </table>";
        }

        public string GIAContent()
        {
            return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>GIA Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f8f8;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 100%;
            max-width: 600px; /* Set maximum width for better email client support */
            margin: 20px auto;
            border: 1px solid #ccc;
            background-color: white;
        }
        .column {
            width: 33.33%;
            vertical-align: top;
            border-right: 1px solid #ccc;
            padding: 10px;
            box-sizing: border-box;
        }
        .column:last-child {
            border-right: none;
        }
        .h5 {
            background-color: #d8b993;
            color: white;
            padding: 5px;
            font-family: 'Mukta', Arial, sans-serif;
            margin-top: 0;
        }
        p {
            font-family: ""Roboto"", sans-serif;
            margin-top: 5px;
            margin-bottom: 5px;
        }
        img {
            max-width: 100%;
            height: auto;
            display: block;
        }
        .text-center {
            text-align: center;
        }
        .list-inline {
            list-style: none;
            padding: 0;
            text-align: center;
        }
        .list-inline-item {
            display: inline-block;
            margin-right: 10px;
        }
    </style>
</head>
<body>

<table class=""container"" cellpadding=""0"" cellspacing=""0"" border=""0"">
    <tr>
        <!-- First Column -->
        <td class=""column"">
            <div>
                <img src=""https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/GIA.png?alt=media&token=c208c241-01e5-4b79-bcd1-d353e7f55bf5"" alt=""GIA banner"">
            </div>
            <div class=""border-top pt-3"">
                <h3 class=""h5"">GIA NATURAL DIAMOND GRADING REPORT</h3>
                <p><strong>January 01, 2014</strong></p>
                <p>GIA Report Number: <strong>214438167</strong></p>
                <p>Shape and Cutting Style: <strong>Round Brilliant</strong></p>
                <p>Measurements: <strong>6.41 - 6.43 x 3.97 mm</strong></p>
            </div>
            <div class=""border-top pt-3"">
                <h3 class=""h5"">GRADING RESULTS</h3>
                <p>Carat Weight: <strong>1.01 carat</strong></p>
                <p>Color Grade: <strong>F</strong></p>
                <p>Clarity Grade: <strong>SI1</strong></p>
                <p>Cut Grade: <strong>Excellent</strong></p>
            </div>
            <div class=""border-top pt-3"">
                <h3 class=""h5"">ADDITIONAL GRADING INFORMATION</h3>
                <p>Polish: <strong>Excellent</strong></p>
                <p>Symmetry: <strong>Excellent</strong></p>
                <p>Fluorescence: <strong>None</strong></p>
                <p>Inscription(s): <strong>GIA 2141438167, I Love You</strong></p>
                <p>Comments: <strong>""SAMPLE""-""SAMPLE""-""SAMPLE""-""SAMPLE""</strong></p>
            </div>
        </td>
        
        <!-- Second Column -->
        <td class=""column"">
            <div class=""text-center mb-2"">
                <h2 class=""h4"">GIA REPORT</h2>
                <p class=""font-weight-bold"">214438167</p>
                <p>Verify this report at <a href=""https://www.gia.edu/"">GIA.edu</a></p>
            </div>
            <div class=""pt-3"">
                <h3 class=""h5"">GRADING SCALES</h3>
                <div class=""text-center"">
                    <h4 class=""h6"">GIA COLOR SCALE</h4>
                    <p>D E F G H I J K L M N O P Q R S T U V W X Y Z</p>
                </div>
                <div class=""text-center"">
                    <h4 class=""h6"">GIA CLARITY SCALE</h4>
                    <p>FLAWLESS<br>INTERNALLY FLAWLESS<br>VVS<sub>1</sub> VVS<sub>2</sub><br>VS<sub>1</sub> VS<sub>2</sub><br>SI<sub>1</sub> SI<sub>2</sub><br>I<sub>1</sub> I<sub>2</sub> I<sub>3</sub></p>
                </div>
                <div class=""text-center"">
                    <h4 class=""h6"">GIA CUT SCALE</h4>
                    <p>EXCELLENT<br>VERY GOOD<br>GOOD<br>FAIR<br>POOR</p>
                </div>
            </div>
            <div class=""border-top pt-3 text-center"">
                <h3 class=""h5"">CLARITY CHARACTERISTICS</h3>
                <p><img src=""https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/Bezel%20Set%20Solitaire%20Pendant%20Setting%20In%2014k%20White%20Gold.jpg?alt=media&token=95fe918c-0198-4206-a381-ac28ca2f53d7"" alt=""Clarity Characteristics"" class=""scaled-image""></p>
                <p><strong>KEY TO SYMBOLS:</strong></p>
                <ul class=""list-inline"">
                    <li class=""list-inline-item"">Crystal</li>
                    <li class=""list-inline-item"">Cloud</li>
                    <li class=""list-inline-item"">Feather</li>
                    <li class=""list-inline-item"">Natural</li>
                </ul>
            </div>
        </td>
        
        <!-- Third Column -->
        <td class=""column"">
            <div class=""pt-3"">
                <p>The results documented in this report refer only to the diamond described, and were obtained using the techniques and equipment available to GIA at the time of examination. This report is not a guarantee or valuation. For additional information and important limitations and disclaimers, please see gia.edu/terms.</p>
                <p>For additional information, contact us at <strong>800-421-7250</strong> or <strong>760-603-4500</strong>.</p>
            </div>
            <div class=""pt-3 text-right"">
                <img src=""https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/GIA-seal.png?alt=media&token=ef7e7eb0-fcc0-4767-ac47-85ed041bee5f"" alt=""GIA Seal"" style=""max-width: 100px;"">
            </div>
        </td>
    </tr>
</table>

</body>
</html>
";
        }
    }
}
