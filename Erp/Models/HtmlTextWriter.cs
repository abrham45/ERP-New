using System;
using System.IO;

namespace Erp.Models
{
    internal class HtmlTextWriter : IDisposable
    {
        private TextWriter writer;

        public HtmlTextWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal void Write(string v)
        {
            throw new NotImplementedException();
        }
    }
}