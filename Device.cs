/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement
{
    // Lop truu tuong dai dien cho thiet bi chung
    public abstract class Device
    {
        public string DeviceId { get; }
        public string DeviceName { get; set; }
        public int YearOfManufacture { get; set; }
        public decimal PurchasePrice { get; set; }
        public DeviceStatus Status { get; set; }

        protected Device(string deviceId, string deviceName, int yearOfManufacture, decimal purchasePrice, DeviceStatus status)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));

            if (purchasePrice <= 0)
                throw new ArgumentException("Giá mua phải lớn hơn 0.", nameof(purchasePrice));

            int currentYear = DateTime.Now.Year;
            if (yearOfManufacture > currentYear)
                throw new ArgumentException($"Năm sản xuất không được lớn hơn năm hiện tại ({currentYear}).", nameof(yearOfManufacture));

            DeviceId = deviceId;
            DeviceName = deviceName;
            YearOfManufacture = yearOfManufacture;
            PurchasePrice = purchasePrice;
            Status = status;
        }

        // Phuong thuc truu tuong tinh chi phi bao tri hang nam
        public abstract decimal CalculateAnnualMaintenanceCost();

        public override string ToString()
        {
            return $"[{DeviceId}] {DeviceName} | SX: {YearOfManufacture} | Giá: {PurchasePrice:N0} VNĐ | Trạng thái: {Status}";
        }
    }
}