using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDFCore.Common.FileUpload.Upload
{
    public class HttpDirectoryContentsDescriptor
    {
        public bool Exists { get; set; }
        public IEnumerable<HttpFileDescriptor> FileDescriptors { get; set; }

        public HttpDirectoryContentsDescriptor()
        {
            this.FileDescriptors = new HttpFileDescriptor[0];
        }

        public HttpDirectoryContentsDescriptor(IDirectoryContents directoryContents, Func<string, string> physicalPathResolver)
        {
            this.Exists = directoryContents.Exists;
            this.FileDescriptors = directoryContents.Select(_ => new HttpFileDescriptor(_, physicalPathResolver));
        }
    }
}
