using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiskDetect.Formatters
{
    public class ByteCountFormatter
    {
        public enum ByteCountFormatterUnits
        {
            UseBytes = 0,
            UseKB = 1,
            UseMB = 2,
            UseGB = 3,
            UseTB = 4,
            UsePB = 5,
            UseEB = 6,
            UseAll = 9,
        }

        public List<ByteCountFormatterUnits> AllowedUnits { get; set; } = new List<ByteCountFormatterUnits>();

        public string stringFromByteCount(long byteCount)
        {
            var allowedUnits = new List<ByteCountFormatterUnits>();

            if (AllowedUnits.Count == 0 || AllowedUnits.Contains(ByteCountFormatterUnits.UseAll))
            {
                allowedUnits.Add(ByteCountFormatterUnits.UseBytes);
                allowedUnits.Add(ByteCountFormatterUnits.UseKB);
                allowedUnits.Add(ByteCountFormatterUnits.UseMB);
                allowedUnits.Add(ByteCountFormatterUnits.UseGB);
                allowedUnits.Add(ByteCountFormatterUnits.UseTB);
                allowedUnits.Add(ByteCountFormatterUnits.UsePB);
                allowedUnits.Add(ByteCountFormatterUnits.UseEB);
            }
            else
            {
                allowedUnits.AddRange(AllowedUnits);
            }

            var unitStr = "bytes";
            long bytes = byteCount;

            if (AllowedUnits.Contains(ByteCountFormatterUnits.UseBytes))
            {
                unitStr = byteCount != 1 ? "bytes" : "byte";
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UseKB))
            {
                unitStr = "KB";
                bytes /= (long)Math.Pow(10, 3);
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UseMB))
            {
                unitStr = "MB";
                bytes /= (long)Math.Pow(10, 6);
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UseGB))
            {
                unitStr = "GB";
                bytes /= (long)Math.Pow(10, 9);
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UseTB))
            {
                unitStr = "TB";
                bytes /= (long)Math.Pow(10, 12);
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UsePB))
            {
                unitStr = "PB";
                bytes /= (long)Math.Pow(10, 15);
            }
            else if (AllowedUnits.Contains(ByteCountFormatterUnits.UseEB))
            {
                unitStr = "EB";
                bytes /= (long)Math.Pow(10, 18);
            }
            else
            {
                // No allowed units present, choose the closest unit based on byteCount
                var unitsInBytes = new Dictionary<ByteCountFormatterUnits, long>
        {
            { ByteCountFormatterUnits.UseBytes, 1 },
            { ByteCountFormatterUnits.UseKB, (long)Math.Pow(10, 3) },
            { ByteCountFormatterUnits.UseMB, (long)Math.Pow(10, 6) },
            { ByteCountFormatterUnits.UseGB, (long)Math.Pow(10, 9) },
            { ByteCountFormatterUnits.UseTB, (long)Math.Pow(10, 12) },
            { ByteCountFormatterUnits.UsePB, (long)Math.Pow(10, 15) },
            { ByteCountFormatterUnits.UseEB, (long)Math.Pow(10, 18) }
        };

                long closestValue = long.MaxValue;

                foreach (var unit in allowedUnits)
                {
                    if (unitsInBytes.ContainsKey(unit))
                    {
                        var value = unitsInBytes[unit];
                        if (Math.Abs(byteCount - value) < Math.Abs(byteCount - closestValue))
                        {
                            closestValue = value;
                            unitStr = unit.ToString().Substring(3);
                        }
                    }
                }

                if (closestValue != long.MaxValue)
                {
                    bytes /= closestValue;
                }
            }

            return $"{bytes} {unitStr}";
        }
    }
}
