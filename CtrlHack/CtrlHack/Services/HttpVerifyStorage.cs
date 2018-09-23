using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CtrlHack.Models;
using HtmlAgilityPack;

namespace CtrlHack.Services
{
    public class HttpVerifyStorage : IVerifyStorage
    {

        public async Task<IEnumerable<Verify>> GetVerifyesAsync(int year, string orgName = "", string orgn = "", string inn = "", int? subject = null)
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync($"http://inspect.rospotrebnadzor.ru/{year}");
            var fields = doc
                .DocumentNode
                .SelectNodes("//td/b")
                .Skip(1)
                .Select(n => n.InnerText)
                .Where(r => !Regex.IsMatch(r, @"^\d+\D$"))
                .Select(s => s.Replace("&nbsp;", " "))
                .Select(s => s.Replace("&quot;", "\""))
                .ToList();
            var next = fields
                .Select((val, num) => (val, num))
                .GroupBy(vn => vn.num / 9)
                .Select(g => g.Select(vn => vn.val))
                .Select(Parse)
                .ToList();
            return next;
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
            (ver, v) => ver.Duration = v,
            (ver, v) => ver.Form = v,
            (ver, v) => ver.HardField = v
        };
    }
}
