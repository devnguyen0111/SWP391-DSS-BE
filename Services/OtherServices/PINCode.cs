using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public class PINCode : IPINCode
    {
        public string GeneratedPinCode()
        {
            Random random = new Random();
            int pin = random.Next(0, 1000000);
            return pin.ToString("D6");
        }

        public string PinCodeHtmlContent(string pin)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Display PIN</title>
    <style>
        body {{
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            background-size: cover;
            background-position: center;
        }}
        .container {{
            display: flex;
            justify-content: center;
            align-items: center;
            border: 2px solid #000;
            padding: 20px;
            background-color: rgba(255, 255, 255, 0.8);
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .pin {{
            font-size: 48px;
            letter-spacing: 10px;
            font-family: Arial, sans-serif;
            color: red;
            font-weight: bold;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='pin'>{pin}</div>
    </div>
</body>
</html>";
        }
    }
}
