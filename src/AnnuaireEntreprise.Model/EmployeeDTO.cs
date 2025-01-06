namespace AnnuaireEntreprise.Model.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Email { get; set; }
        public required string TelephoneFixe { get; set; }
        public required string TelephonePortable { get; set; }
        public required string Site { get; set; }
        public required string Service { get; set; }
    }
}
