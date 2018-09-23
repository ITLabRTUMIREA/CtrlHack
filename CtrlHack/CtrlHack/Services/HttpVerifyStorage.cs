using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CtrlHack.Models;
using HtmlAgilityPack;

namespace CtrlHack.Services
{
    public class HttpVerifyStorage : IVerifyStorage
    {
        public List<RegionPair> Regions => regions;

        public async Task<IEnumerable<Verify>> GetVerifyesAsync(int year, string orgName = "", string ogrn = "", string inn = "", int? subject = null, CancellationToken token = default)
        {
            var strSubject = subject?.ToString() ?? "";

            orgName = orgName?.ToLong() ?? "";
            var web = new HtmlWeb();
            var url = $"http://inspect.rospotrebnadzor.ru/{year}?action=search&name={orgName}&ogrn={ogrn}&inn={inn}&code_region={strSubject}";
            var doc = await web.LoadFromWebAsync(url);
            var fields = doc
                .DocumentNode
                .SelectNodes("//td/b")
                .Skip(1)
                .Select(n => n.InnerText)
                .Select(n => n.Replace("&quot;", "\"").Replace("&nbsp;", " "))
                //.Select(n => n.First().ToString().ToUpper() + n.Substring(1))
                .Where(r => !Regex.IsMatch(r, @"^\d+\D$"))
                .ToList();
            var next = fields
                .Select((val, num) => (val, num))
                .GroupBy(vn => vn.num / ChunkSize(year))
                .Select(g => g.Select(vn => vn.val))
                .Select(Parse)
                .ToList();
            return next;
        }

        private int ChunkSize(int year)
        {
            switch (year)
            {
                case 2013:
                case 2014:
                    return 8;
                default:
                    return 9;
            }
        }

        private static Verify Parse(IEnumerable<string> fields)
        {
            var verify = new Verify();
            fields
                .Select((v, n) => (v, n))
                .ToList()
                .ForEach(vn => inits[vn.n](verify, vn.v));
            return verify;
        }

        private static readonly Action<Verify, string>[] inits = new Action<Verify, string>[]
        {
            (ver, v) => ver.OrgName = v,
            (ver, v) => ver.OGRN = v,
            (ver, v) => ver.INN = v,
            (ver, v) => ver.Address = v,
            (ver, v) => ver.Purpose = v,
            (ver, v) => ver.Date = v,
            (ver, v) => ver.Duration = v.Replace("\n", ""),
            (ver, v) => ver.Form = v,
            (ver, v) => ver.HardField = v
        };
        private static readonly List<RegionPair> regions = new List<RegionPair>()
        {

            {new RegionPair{Number = null, Title = "Не указано"} },
            {new RegionPair{Number = 1, Title = "Республика Адыгея"} },
            { new RegionPair{Number = 2, Title = "Республика Башкортостан"} },
            { new RegionPair{Number = 3, Title = "Республика Бурятия"} },
            { new RegionPair{Number = 4, Title = "Республика Алтай"} },
            { new RegionPair{Number = 5, Title = "Республика Дагестан"} },
            { new RegionPair{Number = 6, Title = "Республика Ингушетия"} },
            { new RegionPair{Number = 7, Title = "Республика Кабардино-Балкария"} },
            { new RegionPair{Number = 8, Title = "Республика Калмыкия"} },
            { new RegionPair{Number = 9, Title = "Республика Карачаево-Черкесия"} },
            { new RegionPair{Number = 10, Title = "Республика Карелия"} },
            { new RegionPair{Number = 11, Title = "Республика Коми"} },
            { new RegionPair{Number = 12, Title = "Республика Марий Эл"} },
            { new RegionPair{Number = 13, Title = "Республика Мордовия"} },
            { new RegionPair{Number = 14, Title = "Республика Саха (Якутия)"} },
            { new RegionPair{Number = 15, Title = "Республика Северная Осетия-Алания"} },
            { new RegionPair{Number = 16, Title = "Республика Татарстан"} },
            { new RegionPair{Number = 17, Title = "Республика Тыва"} },
            { new RegionPair{Number = 18, Title = "Удмуртская Республика"} },
            { new RegionPair{Number = 19, Title = "Республика Хакасия"} },{new RegionPair{Number = 20, Title = "Чеченская Республика"} },{new RegionPair{Number = 21, Title = "Чувашская Республика"} },{new RegionPair{Number = 22, Title = "Алтайский край"} },{new RegionPair{Number = 23, Title = "Краснодарский край"} },{new RegionPair{Number = 24, Title = "Красноярский край"} },{new RegionPair{Number = 25, Title = "Приморский край"} },{new RegionPair{Number = 26, Title = "Ставропольский край"} },{new RegionPair{Number = 27, Title = "Хабаровский край"} },{new RegionPair{Number = 28, Title = "Амурская область"} },{new RegionPair{Number = 29, Title = "Архангельская область"} },{new RegionPair{Number = 30, Title = "Астраханская область"} },{new RegionPair{Number = 31, Title = "Белгородская область"} },{new RegionPair{Number = 32, Title = "Брянская область"} },{new RegionPair{Number = 33, Title = "Владимирская область"} },{new RegionPair{Number = 34, Title = "Волгоградская область"} },{new RegionPair{Number = 35, Title = "Вологодская область"} },{new RegionPair{Number = 36, Title = "Воронежская область"} },{new RegionPair{Number = 37, Title = "Ивановская область"} },{new RegionPair{Number = 38, Title = "Иркутская область"} },{new RegionPair{Number = 39, Title = "Калининградская область"} },{new RegionPair{Number = 40, Title = "Калужская область"} },{new RegionPair{Number = 41, Title = "Камчатский край"} },{new RegionPair{Number = 42, Title = "Кемеровская область"} },{new RegionPair{Number = 43, Title = "Кировская область"} },{new RegionPair{Number = 44, Title = "Костромская область"} },{new RegionPair{Number = 45, Title = "Курганская область"} },{new RegionPair{Number = 46, Title = "Курская область"} },{new RegionPair{Number = 47, Title = "Ленинградская область"} },{new RegionPair{Number = 48, Title = "Липецкая область"} },{new RegionPair{Number = 49, Title = "Магаданская область"} },{new RegionPair{Number = 50, Title = "Московская область"} },{new RegionPair{Number = 51, Title = "Мурманская область"} },{new RegionPair{Number = 52, Title = "Нижегородская область"} },{new RegionPair{Number = 53, Title = "Новгородская область"} },{new RegionPair{Number = 54, Title = "Новосибирская область"} },{new RegionPair{Number = 55, Title = "Омская область"} },{new RegionPair{Number = 56, Title = "Оренбургская область"} },{new RegionPair{Number = 57, Title = "Орловская область"} },{new RegionPair{Number = 58, Title = "Пензенская область"} },{new RegionPair{Number = 59, Title = "Пермский край"} },{new RegionPair{Number = 60, Title = "Псковская область"} },{new RegionPair{Number = 61, Title = "Ростовская область"} },{new RegionPair{Number = 62, Title = "Рязанская область"} },{new RegionPair{Number = 63, Title = "Самарская область"} },{new RegionPair{Number = 64, Title = "Саратовская область"} },{new RegionPair{Number = 65, Title = "Сахалинская область"} },{new RegionPair{Number = 66, Title = "Свердловская область"} },{new RegionPair{Number = 67, Title = "Смоленская область"} },{new RegionPair{Number = 68, Title = "Тамбовская область"} },{new RegionPair{Number = 69, Title = "Тверская область"} },{new RegionPair{Number = 70, Title = "Томская область"} },{new RegionPair{Number = 71, Title = "Тульская область"} },{new RegionPair{Number = 72, Title = "Тюменская область"} },{new RegionPair{Number = 73, Title = "Ульяновская область"} },{new RegionPair{Number = 74, Title = "Челябинская область"} },{new RegionPair{Number = 75, Title = "Забайкальский край"} },{new RegionPair{Number = 76, Title = "Ярославская область"} },{new RegionPair{Number = 77, Title = "г.Москва "} }, new RegionPair{Number = 78, Title = "г.Санкт - Петербург "}, new  RegionPair{ Number= 79, Title = "Еврейская автономная область"},
            { new RegionPair{Number = 82, Title = "Республика Крым и г.Севастополь"} },
            { new RegionPair{Number = 83, Title = "Ненецкий автономный округ"} },
            { new RegionPair{Number = 86, Title = "Ханты - Мансийский автономный округ"} },
            { new RegionPair{Number = 87, Title = "Чукотский Автономный округ"} },
            { new RegionPair{Number = 89, Title = "Ямало - Ненецкий автономный округ"} },
            { new RegionPair{Number = 90, Title = "Железнодорожный транспорт"} },
            
        };
    }
}
