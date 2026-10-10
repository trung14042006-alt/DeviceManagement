/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement
{
    // May in ho tro mang (Ke thua Printer va INetworkable)
    public class NetworkPrinter : Printer, INetworkable
    {
        public string? IpAddress { get; private set; }
        public bool IsConnected => !string.IsNullOrEmpty(IpAddress);

        public NetworkPrinter(
            string deviceId,
            string deviceName,
            int yearOfManufacture,
            decimal purchasePrice,
            DeviceStatus status,
            string printerType,
            int printedPageCount,
            bool isColorPrinter)
            : base(deviceId, deviceName, yearOfManufacture, purchasePrice, status, printerType, printedPageCount, isColorPrinter)
        {
        }

        public void Connect(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được rỗng.", nameof(ipAddress));

            if (IsConnected)
                throw new InvalidOperationException($"Máy in {DeviceId} đang kết nối IP {IpAddress}.");

            IpAddress = ipAddress;
            Console.WriteLine($"[Network] NetworkPrinter {DeviceId} đã kết nối IP: {IpAddress}");
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                Console.WriteLine($"[Network] NetworkPrinter {DeviceId} chưa kết nối mạng.");
                return;
            }

            Console.WriteLine($"[Network] NetworkPrinter {DeviceId} đã ngắt kết nối IP: {IpAddress}");
            IpAddress = null;
        }

        public override string ToString()
        {
            string netInfo = IsConnected ? $"IP: {IpAddress}" : "Disconnected";
            return $"{base.ToString()} | NetworkPrinter ({netInfo})";
        }
    }
}