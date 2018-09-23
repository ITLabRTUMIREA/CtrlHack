using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlHack.Models
{
    public class Verify
    {
        public const int MaxCompactLength = 80;
        public string CompactOrgName => ZipedOrgName.Length > MaxCompactLength ?
            ZipedOrgName.Substring(0, MaxCompactLength - 3) + "..."
            : ZipedOrgName;
        public string ZipedOrgName => OrgName
            .Replace("МУНИЦИПАЛЬНОЕ БЮДЖЕТНОЕ ОБЩЕОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ", "МБОУ")
            .Replace("СРЕДНЯЯ ОБЩЕОБРАЗОВАТЕЛЬНАЯ ШКОЛА", "СОШ")
            .Replace("МУНИЦИПАЛЬНОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ДОПОЛНИТЕЛЬНОГО ОБРАЗОВАНИЯ", "МБУ ДО")
            .Replace("МУНИЦИПАЛЬНАЯ БЮДЖЕТНАЯ ДОШКОЛЬНАЯ ОБРАЗОВАТЕЛЬНАЯ ОРГАНИЗАЦИЯ", "МБДОО")
            .Replace("ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ", "ООО")
            
            ;
        public string OrgName { get; set; }
        public string OGRN { get; set; }
        public string INN { get; set; }
        public string Address { get; set; }
        public string Purpose { get; set; }
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Form { get; set; }
        public string HardField { get; set; }
    }
}
