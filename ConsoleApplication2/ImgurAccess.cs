using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace ConsoleApplication2
{
    class ImgurAccess
    {
        string clientId = "ppp";
        string clientSecret = "CLIENTSECRET";


        /// <summary>
        /// Uploads an image to Imgur and returns the url for that image
        /// </summary>
        /// <param name="imagePath"> The filepath for your image</param>
        public string UploadImage(string imagePath)
        {
            WebClient wc = new WebClient();

            wc.Headers.Add("Authorization", "Client-ID " + clientId);

            NameValueCollection Keys = new NameValueCollection();

            try
            {
                Keys.Add("image", Convert.ToBase64String(File.ReadAllBytes(imagePath)));
                byte[] responseArray = wc.UploadValues("https://api.imgur.com/3/image", Keys);

                dynamic result = Encoding.ASCII.GetString(responseArray);
                Regex reg = new Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                return url;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                return "Failed";
        }
    }


}
