﻿using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace booru_downloader {
    static internal class WebHelper {
        public static async Task<string> GetAsync(string uri) {
            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return await reader.ReadToEndAsync();
            }
        }

        public static string FormatGet(string uri, params KeyValuePair<string, string>[] args) {
            string argstr = "";

            foreach (KeyValuePair<string, string> kvp in args) {
                argstr += "&" + kvp.Key + "=" + kvp.Value;
            }

            return uri + "?" + argstr.Substring(1);
        }
    }
}