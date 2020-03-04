using LDFCore.Common.FileUpload.Upload;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace LDFCore.Common.FileUpload.Model
{
    public class HttpFileInfo : IFileInfo
    {
        private HttpClient _httpClient;

        public bool Exists { get; private set; }
        public bool IsDirectory { get; private set; }
        public DateTimeOffset LastModified { get; private set; }
        public long Length { get; private set; }
        public string Name { get; private set; }
        public string PhysicalPath { get; private set; }

        public HttpFileInfo(HttpFileDescriptor descriptor, HttpClient httpClient)
        {
            this.Exists = descriptor.Exists;
            this.IsDirectory = descriptor.IsDirectory;
            this.LastModified = descriptor.LastModified;
            this.Length = descriptor.Length;
            this.Name = descriptor.Name;
            this.PhysicalPath = descriptor.PhysicalPath;
            _httpClient = httpClient;
        }

        public Stream CreateReadStream()
        {
            HttpResponseMessage message = _httpClient.GetAsync(this.PhysicalPath).Result;
            return message.Content.ReadAsStreamAsync().Result;
        }
    }
}
