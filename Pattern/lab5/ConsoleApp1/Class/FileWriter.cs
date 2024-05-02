using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab5Lib.Class {
    internal class FileWriter : IWriter {
        private string _filename;
        public string FileName { get => _filename; }

        public FileWriter(string? filename = null) {
            _filename = filename ?? Constant.FileName;
        }

        public string? Save(string? message) {
            using (StreamWriter sw = new(_filename, true)) {
                sw.WriteLine(message);
            }
            return _filename;
        }
    }
}
