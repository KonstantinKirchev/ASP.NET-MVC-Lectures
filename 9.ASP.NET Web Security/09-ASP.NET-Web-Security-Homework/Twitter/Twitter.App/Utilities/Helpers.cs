namespace Twitter.App.Utilities
{
    using System;
    using System.IO;
    using System.Web;

    public class Helpers
    {
        public static string ConvertImage(HttpPostedFileBase image)
        {
            string imageString;
            using (var memoryStream = new MemoryStream())
            {
                image.InputStream.CopyTo(memoryStream);
                var byteArray = memoryStream.ToArray();
                imageString = "data:" + image.ContentType + ";base64," + Convert.ToBase64String(byteArray);
            }

            return imageString;
        }
    }
}