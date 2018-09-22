using CtrlHack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtrlHack.Services
{
    public interface IVerifyStorage
    {
        Task<IEnumerable<Verify>> GetVerifyesAsync(int year, string orgName = "", string orgn = "", string inn = "", int? subject = null);
    }
}
