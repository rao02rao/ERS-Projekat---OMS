namespace OutageManagementSystem
{
    // Interfejs koji definiše osnovne karakteristike električnog elementa.
    public interface IElectricalElement
    {
        int ElementId { get; }
        string Name { get; }
        string Type { get; }
        double Latitude { get; }
        double Longitude { get; }
        string VoltageLevel { get; }
        string ToXml();
    }
}
