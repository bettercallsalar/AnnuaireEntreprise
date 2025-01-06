namespace AnnuaireEntreprise.Model.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Email { get; set; }
        public string? TelephoneFixe { get; set; }
        public required string TelephonePortable { get; set; }
        public required int SiteId { get; set; }
        public required int ServiceId { get; set; }
    }
}
