using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnuaireEntreprise.Model.Models
{
    public class SearchEmployeeDTO
    {
        [JsonPropertyName("nom")]
        public string? Nom { get; set; } = null;
        [JsonPropertyName("prenom")]
        public string? Prenom { get; set; } = null;
        [JsonPropertyName("email")]
        public string? Email { get; set; } = null;
        [JsonPropertyName("telephone")]
        public string? Telephone { get; set; } = null;
        public int? SiteId { get; set; } = null;
        public int? ServiceId { get; set; } = null;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 45;
    }
}
