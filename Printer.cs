/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

namespace DeviceManagement
{
    // Lop co so cho may in
    public class Printer : Device
    {
        public string PrinterType { get; set; } // Laser hoac Phun
        public int PrintedPageCount { get; set; }
        public bool IsColorPrinter { get; set; }

        public Printer(
            string deviceId,
            string deviceName,
            int yearOfManufacture,
            decimal purchasePrice,
            DeviceStatus status,
            string printerType,
            int printedPageCount,
            bool isColorPrinter)
            : base(deviceId, deviceName, yearOfManufacture, purchasePrice, status)
        {
            PrinterType = printerType;
            PrintedPageCount = printedPageCount;
            IsColorPrinter = isColorPrinter;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.04m; // Co ban 4%

            if (PrintedPageCount > 100000)
                cost += 500000m; // Cong 500k neu in > 100k trang

            if (IsColorPrinter)
                cost += 300000m; // Cong 300k neu la may in mau

            return cost;
        }

        public override string ToString()
        {
            string colorInfo = IsColorPrinter ? "In mau" : "In den trang";
            return $"{base.ToString()} | Loai: {PrinterType} | So trang: {PrintedPageCount:N0} | {colorInfo}";
        }
    }
}