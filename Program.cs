/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Khởi tạo thiết bị mẫu
            var pc1 = new Computer("PC01", "Máy tính Văn phòng", 2022, 12000000m, DeviceStatus.Active, 16, "Intel i5", false);
            var pc2 = new Computer("PC02", "Máy trạm Đồ họa", 2018, 28000000m, DeviceStatus.UnderMaintenance, 32, "Intel i7", true);

            var printer1 = new Printer("PR01", "Máy in Laser đơn sắc", 2023, 4500000m, DeviceStatus.Active, "Laser", 45000, false);
            var printer2 = new NetworkPrinter("PR02", "Máy in mạng cao cấp", 2019, 16000000m, DeviceStatus.Active, "Laser", 120000, true);

            var proj1 = new Projector("PJ01", "Máy chiếu hội trường", 2020, 20000000m, DeviceStatus.Active, 4000, 3200);

            // 2. Khởi tạo phòng thực hành
            var lab1 = new LabRoom("LAB101", "Phòng Thực Hành Mạng", 35);
            var lab2 = new LabRoom("LAB102", "Phòng Thực Hành Đồ Họa", 25);

            // 3. Thêm thiết bị vào phòng
            lab1.AddDevice(pc1);
            lab1.AddDevice(printer1);
            lab1.AddDevice(printer2);

            lab2.AddDevice(pc2);
            lab2.AddDevice(proj1);

            // 4. Thử thêm thiết bị trùng mã (Bắt lỗi)
            try
            {
                var duplicatePc = new Computer("PC01", "Trùng mã", 2024, 10000000m, DeviceStatus.Active, 8, "i3", false);
                lab1.AddDevice(duplicatePc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            Console.WriteLine();

            // 5. In danh sách thiết bị
            lab1.PrintDeviceList();
            Console.WriteLine();
            lab2.PrintDeviceList();
            Console.WriteLine();

            // 6. Tính tổng chi phí bảo trì (Đa hình)
            Console.WriteLine($"Tổng chi phí bảo trì phòng {lab1.RoomName}: {lab1.CalculateAnnualMaintenanceCost():N0} VNĐ");
            Console.WriteLine($"Tổng chi phí bảo trì phòng {lab2.RoomName}: {lab2.CalculateAnnualMaintenanceCost():N0} VNĐ");
            Console.WriteLine();

            // 7. Liệt kê thiết bị cần bảo trì
            var allLabs = new List<LabRoom> { lab1, lab2 };
            foreach (var lab in allLabs)
            {
                Console.WriteLine($"Thiết bị cần bảo trì tại {lab.RoomName}:");
                var reqList = lab.GetDevicesRequiringMaintenance();
                if (reqList.Count == 0)
                {
                    Console.WriteLine("  Không có.");
                }
                else
                {
                    foreach (var d in reqList)
                    {
                        Console.WriteLine($"  - [{d.DeviceId}] {d.DeviceName} (Trạng thái: {d.Status}, SX: {d.YearOfManufacture})");
                    }
                }
            }
            Console.WriteLine();

            // 8. Kết nối mạng qua INetworkable
            var netDevices = new List<INetworkable>();
            foreach (var lab in allLabs)
            {
                foreach (var d in lab.Devices)
                {
                    if (d is INetworkable net)
                        netDevices.Add(net);
                }
            }

            if (netDevices.Count >= 2)
            {
                netDevices[0].Connect("192.168.1.50");
                netDevices[1].Connect("192.168.1.80");
            }

            Console.WriteLine("\nTrạng thái thiết bị mạng:");
            foreach (var item in netDevices)
            {
                Console.WriteLine($"  - Đã kết nối: {item.IsConnected} | IP: {item.IpAddress ?? "Chưa có"}");
            }
        }
    }
}