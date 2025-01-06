// using AnnuaireEntreprise.Model.Models;

// namespace AnnuaireEntreprise.Model
// {
//     public static class EmployeeMapper
//     {
//         public static EmployeeDTO ToDTO(EmployeeDTO employee)
//         {
//             return new EmployeeDTO
//             {
//                 Id = employee.Id,
//                 Nom = employee.Nom,
//                 Prenom = employee.Prenom,
//                 Email = employee.Email,
//                 TelephoneFixe = employee.TelephoneFixe,
//                 TelephonePortable = employee.TelephonePortable,
//                 Site = employee.Site,
//                 Service = employee.Service
//             };
//         }

//         public static EmployeeDTO ToEntity(EmployeeDTO dto, SiteDTO site, ServiceDTO service)
//         {
//             return new EmployeeDTO
//             {
//                 Id = dto.Id,
//                 Nom = dto.Nom,
//                 Prenom = dto.Prenom,
//                 Email = dto.Email,
//                 TelephoneFixe = dto.TelephoneFixe,
//                 TelephonePortable = dto.TelephonePortable,
//                 SiteId = site.Id,
//                 ServiceId = service.Id,
//                 Site = site,
//                 Service = service
//             };
//         }
//     }
// }
