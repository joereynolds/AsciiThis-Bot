using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


/* ImgurInterface should be able to do the following at very least;
 * Log-in
 * Upload an image
 * Obtain the link for an image 
 * Return an image for use in other classes
 * Save an image to desktop
 */
namespace ConsoleApplication2
{
    class ImgurInterface
    {
        string clientId = "CLIENTID";
        string clientSecret = "CLIENTSECRET";


        void Login()
        {

        }

        /// <summary>
        /// Uploads an image to Imgur
        /// </summary>
        /// <param name="imagePath"> The filepath for your image</param>
        void UploadImage(string imagePath)
        {

            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                    { "key", clientId },
                    { "image", Convert.ToBase64String(File.ReadAllBytes(@"c:/users/joe reynolds/desktop/result.bmp")) }
                };

                byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);

                //Console.WriteLine(XDocument.Load(new MemoryStream(response)));
            }

        }

        /// <summary>
        /// Returns True if a user is logged in to Imgur
        /// </summary>
        /// <returns>Boolean</returns>
        bool IsLoggedIn()
        {
            return true;
        }

        string GetImageUrl()
        {
            return " ";
        }



    }


}
