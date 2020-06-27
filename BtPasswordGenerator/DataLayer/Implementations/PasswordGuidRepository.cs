using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BtPasswordGenerator.DataLayer.Interfaces;

namespace BtPasswordGenerator.DataLayer.Implementations
{
    public class PasswordGuidRepository : IPasswordGuidRepository
    {
        const string _guidFile = "guidStore.txt";
        private readonly string _guidFilePath;

        public PasswordGuidRepository()
        {
            var folderPath = AppDomain.CurrentDomain.BaseDirectory;
            _guidFilePath = string.IsNullOrEmpty(folderPath) ? null : Path.Combine(folderPath, _guidFile);
        }
        public bool StoreGuid(string guid)
        {
            if(!string.IsNullOrEmpty(_guidFilePath))
                File.AppendAllText(_guidFilePath, guid + Environment.NewLine);
            return true;
        }

        public IEnumerable<string> ReadActiveGuid()
        {
            if (!string.IsNullOrEmpty(_guidFilePath))
                return File.ReadLines(_guidFilePath);
            return new List<string>();

        }
    }
}
