using BlogExample.BL.CSVParsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExample.BL.Custom
{
    public class CsvDTOParser : IDataSource<CSVDTO>, IBackupable
    {
        StreamReader Reader { get; set; }
        string _fileName;
        string _backupFolder;
        public CsvDTOParser(string fileName, string backupFolder)
        {
            _fileName = fileName;
            _backupFolder = backupFolder;
            Reader = new StreamReader(fileName);
        }

        public IEnumerator<CSVDTO> GetEnumerator()
        {
            while (Reader!=null && !Reader.EndOfStream)
            {
                var items = Reader
                    .ReadLine()
                    .Split(new char[] { ';', ',' })
                    .Select(x => x.Trim())
                    .ToArray();
                yield return new CSVDTO() { NickName = items[0], Topic = items[1] };
            }
            Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            if (Reader != null)
            {
                Reader.Dispose();
                GC.SuppressFinalize(this);
                Reader = null;
            }
        }

        public void BackUp()
        {
            try
            {
                Reader?.Dispose();
                FileInfo f = new FileInfo(_fileName);
                File.Move(_fileName, $"{_backupFolder}{f.Name}");
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("cannot backup file", e);
            }
        }

        public void Close()
        {
            Reader?.Close();
        }

        ~CsvDTOParser()
        {
            Dispose();
        }
    }
}
