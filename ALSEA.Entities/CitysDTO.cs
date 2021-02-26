namespace ALSEA.Entities
{
    public class CitysDTO:BaseValidateBulkOperationDto
    {
        public int CityId { get; set; }
        public string NameCity { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CoordLon { get; set; }
        public string CoordLat { get; set; }
    }
}
