using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
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
                if (data.Length > 0)
                {
                    data.Append('&');
                }
                data.Append(HttpUtility.UrlEncode(kv.Key)).Append('=').Append(HttpUtility.UrlEncode(kv.Value));
            }
            string queryString = data.ToString();
            string signData = queryString;
            string vnp_SecureHash = VnPayHelper.Sha256(signData + vnp_HashSecret);
            return baseUrl + "?" + queryString + "&vnp_SecureHash=" + vnp_SecureHash;
        }

        public bool ValidateSignature(string queryString, string vnp_HashSecret)
        {
            var queryData = HttpUtility.ParseQueryString(queryString);
            SortedList<string, string> data = new SortedList<string, string>(new VnPayCompare());
            foreach (string key in queryData.AllKeys)
            {
                if (!key.Equals("vnp_SecureHash"))
                {
                    data.Add(key, queryData[key]);
                }
            }
            string rawData = string.Join("&", data.Select(d => $"{d.Key}={d.Value}"));
            string checkSum = VnPayHelper.Sha256(rawData + vnp_HashSecret);
            return checkSum.Equals(queryData["vnp_SecureHash"]);
        }
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.CompareOrdinal(x, y);
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
