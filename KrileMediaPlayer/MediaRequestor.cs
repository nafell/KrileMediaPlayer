using System;
using System.Net;
using System.Threading.Tasks;

namespace KrileMediaPlayer
{
    public class MediaRequestor
    {
        public async Task<byte[]> FetchAsyncWithProgressCallBack(string url, Action<int> progressCallBack)
        {
            var client = new WebClient();

            client.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
            {
                progressCallBack(e.ProgressPercentage);
            };
            
            return await client.DownloadDataTaskAsync(url);
        }
    }
}
