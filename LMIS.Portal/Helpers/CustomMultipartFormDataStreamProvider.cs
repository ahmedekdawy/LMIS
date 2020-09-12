using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LMIS.Portal.Helpers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private bool _idIncluded;
        private int _acceptedCount, _originalCount;
        private readonly Dictionary<string, string> _fileMap = new Dictionary<string, string>();

        private string AcceptedMediaTypes { get; set; }
        private string AcceptedExtensions { get; set; }
        private long MaxFileSize { get; set; }
        private int MaxFileCount { get; set; }

        public bool IdIncluded { get { return _idIncluded; } }
        public int OriginalCount { get { return _originalCount; } }
        public Dictionary<string, string> FileMap { get { return _fileMap; } }

        public CustomMultipartFormDataStreamProvider(string rootPath, string mediaTypes = "", string extensions = "", long maxFileSize = 524288, int maxFileCount = 0)
            : base(rootPath)
        {
            AcceptedMediaTypes = mediaTypes;
            AcceptedExtensions = extensions;
            MaxFileSize = maxFileSize;
            MaxFileCount = maxFileCount;
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            _originalCount++;
            var contentDisposition = headers.ContentDisposition;
            
            //This is not a file?
            if (contentDisposition == null || string.IsNullOrWhiteSpace(contentDisposition.FileName))
                return base.GetStream(parent, headers);

            //Content type of this file is not accepted?
            var isAccepted = AcceptedMediaTypes.Split(',')
                .Any(mediaType => headers.ContentType.MediaType.ToLower().Contains(mediaType.ToLower()));

            if (!isAccepted) return Stream.Null;

            //Extension of this file is not accepted?
            var extension = Path.GetExtension(contentDisposition.FileName.Replace("\"", ""));
            if (!AcceptedExtensions.ToLower().Contains(extension.ToLower()))
                return Stream.Null;

            //Size of this file is larger than is allowed?
            if (contentDisposition.Size != null && contentDisposition.Size > MaxFileSize)
                return Stream.Null;

            //This file is OK but we reached the MAX number of allowed files?
            if (MaxFileCount != 0 && _acceptedCount >= MaxFileCount)
                return Stream.Null;

            //This is a file that we want to read
            _acceptedCount ++;
            return base.GetStream(parent, headers);
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                ? UtcNowForFileNames() + "_NoName.bin"
                : UtcNowForFileNames() + "_" + headers.ContentDisposition.FileName.Replace("\"", "");

            if (_idIncluded || !string.IsNullOrWhiteSpace(headers.ContentDisposition.Name.Replace("\"", ""))) _idIncluded = true;

            _fileMap.Add(name, headers.ContentDisposition.Name.Replace("\"", ""));

            return name;
        }

        private static string UtcNowForFileNames()
        {
            return string.Format(DateTimeFormatInfo.InvariantInfo, "{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.UtcNow);
        }
    }
}