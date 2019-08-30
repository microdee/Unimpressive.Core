using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using md.stdl.Coding;

namespace md.stdl.Network
{
    /// <inheritdoc />
    /// <summary>
    /// High level observable wrapper around WebClient downloading
    /// </summary>
    public class DownloadSlot : ObservableBase<DownloadSlot>, IDisposable
    {
        /// <summary>
        /// Url to file to download
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Destination file path
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Received bytes
        /// </summary>
        public long Received { get; private set; }
        /// <summary>
        /// Total Bytes
        /// </summary>
        public long Total { get; private set; }

        /// <summary>
        /// The task thread of downloading
        /// </summary>
        public Task DownloadTask { get; private set; }

#pragma warning disable CS1591
        public int Percent { get; private set; }
        public bool Ready { get; private set; }
        public bool Success { get; private set; }
        public bool DownloadError { get; private set; }
        public string Message { get; private set; }
        public WebClient Client { get; private set; }
        public DownloadProgressChangedEventArgs LastProgress { get; private set; }
#pragma warning restore CS1591

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="src">Source Url</param>
        /// <param name="dst">Path to destination file</param>
        public DownloadSlot(string src, string dst)
        {
            Client = new WebClient();
            Client.DownloadProgressChanged += (sender, args) =>
            {
                Received = args.BytesReceived;
                Total = args.TotalBytesToReceive;
                Percent = args.ProgressPercentage;
                LastProgress = args;

                Next(this);
            };
            Client.DownloadFileCompleted += (sender, args) =>
            {
                Ready = true;
                if (args.Error != null)
                {
                    DownloadError = true;
                    Message = args.Error.Message;
                    Message += "\n" + args.Error.InnerException?.Message;
                    Error(args.Error.InnerException ?? new Exception("Unknown error"));
                }
                else
                {
                    Success = true;
                    Completed();
                }
            };
            Url = src;
            Destination = dst;
        }
        
        /// <summary>
        /// Invoke to actually start downloading
        /// </summary>
        public void Start()
        {
            var request = 
            DownloadTask = Client.DownloadFileTaskAsync(new Uri(Url), Destination);
        }
        
        /// <summary>
        /// Gets the mime-type of a file from the Url
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMimeType()
        {
            var bytes = await Client.DownloadDataTaskAsync(Url);
            return Client.ResponseHeaders["content-type"];
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
