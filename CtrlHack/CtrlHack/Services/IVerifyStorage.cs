using CtrlHack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CtrlHack.Services
{
    public interface IVerifyStorage
    {
        Task<IEnumerable<Verify>> GetVerifyesAsync(int year, string orgName = "", string ogrn = "", string inn = "", int? subject = null, CancellationToken token = default);
        List<RegionPair> Regions { get; }
    }
}
