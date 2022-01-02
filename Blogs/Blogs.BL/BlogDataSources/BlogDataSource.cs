using Blogs.BL.Abstractions;
using ChinhDo.Transactions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BL.BlogDataSources
{
    public class BlogDataSource : IBlogDataSource<BlogDataSourceDTO>
    {
        private readonly string _sourceFileName;
        private bool isDisposed = false;

        protected string TargetPath { get; set; }
        private IFileManager _fileManager;
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public BlogDataSource(string sourceFileName, string targetPath, IFileManager fileManager)
        {
            _sourceFileName = sourceFileName;
            TargetPath = targetPath;
            _fileManager = fileManager;
        }

        public void Backup()
        {
            ValidateState();

            var filename = String.Concat(TargetPath, Path.GetFileName(_sourceFileName));
            _fileManager.Move(_sourceFileName, filename);
            Dispose();
        }

        protected void ValidateState()
        {
            if (isDisposed)
            {
                throw new InvalidOperationException("Data Source is unvailable");
            }
        }
        public void Dispose()
        {
            if (!isDisposed)
            {
                GC.SuppressFinalize(this);
                isDisposed = true;
            }
        }

        ~BlogDataSource()
        {
            Dispose();
        }

        public IEnumerator<BlogDataSourceDTO> GetEnumerator()
        {
            ValidateState();
            using (StreamReader reader = new StreamReader(_sourceFileName))
            {

                string currentLine = reader.ReadLine();
                while (currentLine != null)
                {
                    ValidateState();
                    var items = currentLine.Split(';');
                    BlogDataSourceDTO current = new BlogDataSourceDTO()
                    {
                        UserName = items[0],
                        BlogName = items[1],
                        CommentTopic = items[2],
                        Session = Id
                    };
                    yield return current;
                    currentLine = reader.ReadLine();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
