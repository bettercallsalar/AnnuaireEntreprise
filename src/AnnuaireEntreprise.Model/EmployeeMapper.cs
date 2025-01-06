// using AnnuaireEntreprise.Model.Models;

// namespace AnnuaireEntreprise.Model
// {
//     public static class EmployeeMapper
//     {
//         public static EmployeeDTO ToDTO(Employee employee)
//         {
//             return new EmployeeDTO
//             {
//                 Id = employee.Id,
//                 Nom = employee.Nom,
//                 Prenom = employee.Prenom,
//                 Email = employee.Email,
//                 TelephoneFixe = employee.TelephoneFixe,
//                 TelephonePortable = employee.TelephonePortable,
//                 Site = employee.Site?.Ville, // Utilisation du nom du site
//                 Service = employee.Service?.Nom // Utilisation du nom du service
//             };
//         }

//         public static Employee ToEntity(EmployeeDTO dto, Site site, Service service)
//         {
//             return new Employee
//             {
//                 Id = dto.Id,
//                 Nom = dto.Nom,
//                 Prenom = dto.Prenom,
//                 Email = dto.Email,
//                 TelephoneFixe = dto.TelephoneFixe,
//                 TelephonePortable = dto.TelephonePortable,
//                 SiteId = site.Id,
//                 ServiceId = service.Id
//             };
//         }
//     }
// }
