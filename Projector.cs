/****************/
/* 202419003 */
/* Nguyễn Thành Trung */
/****************/


namespace DeviceManagement
{
    // Lop quan ly may chieu
    public class Projector : Device
    {
        public int BrightnessLumens { get; set; }
        public int LampHoursUsed { get; set; }

        public Projector(
            string deviceId,
            string deviceName,
            int yearOfManufacture,
            decimal purchasePrice,
            DeviceStatus status,
            int brightnessLumens,
            int lampHoursUsed)
            : base(deviceId, deviceName, yearOfManufacture, purchasePrice, status)
        {
            BrightnessLumens = brightnessLumens;
            LampHoursUsed = lampHoursUsed;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.03m; // Co ban 3%

            if (LampHoursUsed > 3000)
                cost += 1500000m; // Cong 1.5 trieu neu den dung > 3000 gio

            return cost;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Do sang: {BrightnessLumens} lm | Gio den: {LampHoursUsed}h";
        }
    }
}