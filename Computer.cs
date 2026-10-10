/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement
{
    // May tinh ke thua Device va thuc thi INetworkable
    public class Computer : Device, INetworkable
    {
        public int RamCapacityGb { get; set; }
        public string CpuType { get; set; }
        public bool HasDedicatedGpu { get; set; }

        public string? IpAddress { get; private set; }
        public bool IsConnected => !string.IsNullOrEmpty(IpAddress);

        public Computer(
            string deviceId,
            string deviceName,
            int yearOfManufacture,
            decimal purchasePrice,
            DeviceStatus status,
            int ramCapacityGb,
            string cpuType,
            bool hasDedicatedGpu)
            : base(deviceId, deviceName, yearOfManufacture, purchasePrice, status)
        {
            RamCapacityGb = ramCapacityGb;
            CpuType = cpuType;
            HasDedicatedGpu = hasDedicatedGpu;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal rate = 0.05m; // Co ban 5%

            if (HasDedicatedGpu)
                rate += 0.02m; // Them 2% neu co GPU roi

            int yearsInUse = DateTime.Now.Year - YearOfManufacture;
            if (yearsInUse > 5)
                rate += 0.01m; // Them 1% neu dung tren 5 nam

            return PurchasePrice * rate;
        }

        public void Connect(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.", nameof(ipAddress));

            if (IsConnected)
                throw new InvalidOperationException($"Máy tính {DeviceId} đang kết nối IP {IpAddress}.");

            IpAddress = ipAddress;
            Console.WriteLine($"[Network] Computer {DeviceId} đã kết nối IP: {IpAddress}");
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                Console.WriteLine($"[Network] Computer {DeviceId} chưa kết nối mạng.");
                return;
            }

            Console.WriteLine($"[Network] Computer {DeviceId} đã ngắt kết nối IP: {IpAddress}");
            IpAddress = null;
        }

        public override string ToString()
        {
            string gpuInfo = HasDedicatedGpu ? "Co GPU" : "Khong GPU";
            string netInfo = IsConnected ? $"IP: {IpAddress}" : "Disconnected";
            return $"{base.ToString()} | RAM: {RamCapacityGb}GB | CPU: {CpuType} | {gpuInfo} | {netInfo}";
        }
    }
}