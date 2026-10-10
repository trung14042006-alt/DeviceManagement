/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    // Quan he ket tap (Aggregation) giua LabRoom va Device
    public class LabRoom
    {
        public string RoomId { get; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public List<Device> Devices { get; } = new List<Device>();

        public LabRoom(string roomId, string roomName, int capacity)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Không chấp nhận thiết bị null.");

            foreach (var d in Devices)
            {
                if (d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException($"Trùng mã thiết bị '{device.DeviceId}' trong phòng {RoomId}.");
            }

            Devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            var device = FindDevice(deviceId);
            if (device != null)
            {
                Devices.Remove(device);
                return true;
            }
            return false;
        }

        public Device? FindDevice(string deviceId)
        {
            foreach (var d in Devices)
            {
                if (d.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase))
                    return d;
            }
            return null;
        }

        // Tinh tong chi phi bao tri su dung da hinh thuan tuy
        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal totalCost = 0;
            foreach (var d in Devices)
            {
                totalCost += d.CalculateAnnualMaintenanceCost();
            }
            return totalCost;
        }

        public List<Device> GetDevicesRequiringMaintenance()
        {
            var list = new List<Device>();
            int currentYear = DateTime.Now.Year;

            foreach (var d in Devices)
            {
                bool isUnderMaintenance = (d.Status == DeviceStatus.UnderMaintenance);
                bool isOld = (currentYear - d.YearOfManufacture) > 5;

                if (isUnderMaintenance || isOld)
                {
                    list.Add(d);
                }
            }
            return list;
        }

        public void PrintDeviceList()
        {
            Console.WriteLine($"=== Danh sách thiết bị phòng {RoomName} ({RoomId}) ===");
            if (Devices.Count == 0)
            {
                Console.WriteLine("  (Phòng chưa có thiết bị nào)");
                return;
            }

            foreach (var dev in Devices)
            {
                Console.WriteLine($"  - {dev}");
                Console.WriteLine($"    -> Phí bảo trì: {dev.CalculateAnnualMaintenanceCost():N0} VNĐ");
            }
        }
    }
}