using System.Collections.Generic;
using FubuCore.Descriptions;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Core.Resources.Conneg
{
    [Title("Write output model ToString() as text/html")]
    public class HtmlStringWriter<T> : IMediaWriter<T>
    {
        private readonly IOutputWriter _writer;

        public HtmlStringWriter(IOutputWriter writer)
        {
            _writer = writer;
        }

        public void Write(string mimeType, T resource)
        {
            _writer.WriteHtml(resource.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}