using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CtrlHack.Models
{
    public static class Shorts
    {
        public static List<(string longName, string shortName)> List =
            new List<(string longName, string shortName)>
            {
                {("АКЦИОНЕРНОЕ ОБЩЕСТВО", "АО")},
                {("МУНИЦИПАЛЬНОЕ БЮДЖЕТНОЕ ОБЩЕОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ", "МБОУ")},
                {("СРЕДНЯЯ ОБЩЕОБРАЗОВАТЕЛЬНАЯ ШКОЛА", "СОШ")},
                {("МУНИЦИПАЛЬНОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ДОПОЛНИТЕЛЬНОГО ОБРАЗОВАНИЯ", "МБУ ДО")},
                {("МУНИЦИПАЛЬНАЯ БЮДЖЕТНАЯ ДОШКОЛЬНАЯ ОБРАЗОВАТЕЛЬНАЯ ОРГАНИЗАЦИЯ", "МБД ОО")},
                {("ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ", "ГБОУ")},
                {("ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБЩЕОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ", "ГБОУ")},
                {("ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ", "ГБУ")},
                {("ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ", "ООО")},
                {("ИНДИВИДУАЛЬНЫЙ ПРЕДПРИНИМАТЕЛЬ", "ИП")},
                {("ИДИВИДУАЛЬНЫЙ ПРЕДПРИНИМАТЕЛЬ", "ИП")},
                {("ГОСУДАРСТВЕННОЕ АВТОНОМНОЕ УЧРЕЖДЕНИЕ ЗДРАВООХРАНЕНИЯ", "ГАУЗ")},
                {("АВТОНОМНАЯ НЕКОММЕРЧЕСКАЯ ОРГАНИЗАЦИЯ", "АНО")},
                {("ДОПОЛНИТЕЛЬНОГО ОБРАЗОВАНИЯ", "ДО")},
                {("ДОПОЛНИТЕЛЬНОГО ПРОФЕССИОНАЛЬНОГО ОБРАЗОВАНИЯ", "ДПО")},
                {("ВЫСШЕГО ОБРАЗОВАНИЯ", "ВО")}
            };

        public static string ToShort(this string input)
        {
            List.ForEach(l => input = Regex.Replace(input, l.longName, l.shortName, RegexOptions.IgnoreCase));
            return input;
        }
        public static string ToLong(this string input)
        {
            List.ForEach(l => input = Regex.Replace(input, l.shortName, l.longName, RegexOptions.IgnoreCase));
            return input;
        }
    }
}
