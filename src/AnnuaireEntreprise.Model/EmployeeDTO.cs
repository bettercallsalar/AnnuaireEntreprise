// Author: Salar
// Created: 06/01/2025
using System.ComponentModel.DataAnnotations;

namespace AnnuaireEntreprise.Model.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [Required, MinLength(2)]
        public required string Nom { get; set; }

        [Required, MinLength(2)]
        public required string Prenom { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string TelephonePortable { get; set; }

        public string? TelephoneFixe { get; set; }

        public required SiteDTO Site { get; set; }

        public required ServiceDTO Service { get; set; }
    }
}
