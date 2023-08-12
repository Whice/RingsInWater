using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace RingInWater.Utility
{
    
        public class TSVLoader
        {
            public enum Format
            {
                tsv=0,
                csv
            }

            private Format format;
            private string googleSheetUrl;
            private string result = string.Empty;
            public void SetUrl(string googleSheetUrl)
            {
                this.googleSheetUrl = googleSheetUrl;
            }
            public TSVLoader(string googleSheetUrl, Format format = Format.tsv)
            {
                SetUrl(googleSheetUrl);
                this.format = format;
            }
            public void Load()
            {
                string format = this.format.ToString();

                string gid = "0";

                string end = $"/export?format={format}&gid={gid}";

                googleSheetUrl += end;

                using (WebClient client = new WebClient())
                {
                    byte[] fileData = client.DownloadData(googleSheetUrl);

                    MemoryStream memStream = new MemoryStream(fileData);

                    StreamReader reader = new StreamReader(memStream);

                    result = reader.ReadToEnd();
                }
            }
            public string GetResult()
            {
                return this.result;
            }
            public string LoadAndGetResult()
            {
                Load();
                return this.result;
            }
        }
}