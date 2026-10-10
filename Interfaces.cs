/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/

namespace DeviceManagement
{
    // Interface quy dinh thiet bi co kha nang ket noi mang
    public interface INetworkable
    {
        string? IpAddress { get; }
        bool IsConnected { get; }

        void Connect(string ipAddress);
        void Disconnect();
    }
}