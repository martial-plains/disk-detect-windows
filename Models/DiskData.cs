using DiskDetect.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskDetect.Models
{
    public class DiskData
    {
        private ByteCountFormatter _formatter = new ByteCountFormatter();

        public string Type { get; set; }

        public string Format { get; set; }

        private long _totalFreeSpace { get; set; }

        public string TotalFreeSpace
        {
            get
            {
                return _formatter.stringFromByteCount(_totalFreeSpace);
            }
        }

        private long _availableFreeSpace { get; set; }

        public string AvailableFreeSpace
        {
            get
            {
                return _formatter.stringFromByteCount(_availableFreeSpace);
            }
        }


        public string RootDirectory { get; set; }

        private long _totalSize { get; set; }

        public string TotalSize
        {
            get
            {
                return _formatter.stringFromByteCount(_totalSize);
            }
        }

        public string VolumeLabel { get; set; }

        public long Usage
        {
            get { return (long)(((double)(_totalSize - _totalFreeSpace) / (double)_totalSize) * 100); }
        }

        public DiskData(string type, string format, long totalFreeSpace, long availableFreeSpace, string rootDirectory, long totalSize, string volumeLabel)
        {
            Type = type;
            Format = format;
            _totalFreeSpace = totalFreeSpace;
            RootDirectory = rootDirectory;
            _availableFreeSpace = availableFreeSpace;
            _totalSize = totalSize;
            VolumeLabel = volumeLabel;

        }

        public static List<DiskData> GetDiskData()
        {
            var result = new List<DiskData>();
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                result.Add(new DiskData(drive.DriveType.ToString(), drive.DriveFormat, drive.TotalFreeSpace, drive.AvailableFreeSpace, drive.RootDirectory.ToString(), drive.TotalSize, drive.VolumeLabel));
            }

            return result;
        }
    }
}
