using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseOrganizer.Models
{
    public enum EnumScanType
    {
        Item = 0,
        WarehousePlace = 1
    }

    public class ScanResultItem
    {
        
        public String Result { get; set; }

        public String BarCodeFormat { get; set; }

        public EnumScanType ScanType { get; set; }

    }
}
