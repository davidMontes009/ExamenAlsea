namespace ALSEA.Entities
{
    public class CitysByUserDTO : BaseValidateBulkOperationDto
    {
        public int UserId { get; set; }
        public int CityId { get; set; }
    }
}
