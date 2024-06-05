using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Services.Charge
{
    public class VnPayLibrary
    {
        private SortedList<string, string> requestData = new SortedList<string, string>(new VnPayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                requestData.Add(key, value);
            }
        }

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {

                signData = signData.Remove(data.Length - 1, 1);
            }
            string vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }

        public bool ValidateSignature(string queryString, string vnp_HashSecret)
        {
            var queryData = HttpUtility.ParseQueryString(queryString);
            SortedList<string, string> data = new SortedList<string, string>(new VnPayCompare());
            if (queryData.AllKeys != null)
            {
                foreach (string key in queryData.AllKeys)
                {
                    if (!key.Equals("vnp_SecureHash"))
                    {
                        if (key != null && queryData[key] != null)
                        {
                            data.Add(key, queryData[key]);
                        }
                    }
                }
            }
            string rawData = string.Join("&", data.Select(d => $"{d.Key}={d.Value}"));
            string checkSum = VnPayHelper.Sha256(rawData + vnp_HashSecret);
            return checkSum.Equals(queryData["vnp_SecureHash"]);
        }
        }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            return string.CompareOrdinal(x, y);
        }
    }

    public class Utils
    {
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }

    public static class VnPayHelper
    {
        public static string Sha256(string rawData)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}