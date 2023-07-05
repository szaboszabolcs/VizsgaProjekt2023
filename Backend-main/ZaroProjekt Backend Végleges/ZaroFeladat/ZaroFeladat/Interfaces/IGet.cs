using Microsoft.AspNetCore.Mvc;

namespace ZaroFeladat.Interfaces
{
    public interface IGet
    {
        IActionResult Get(string uId);
    }
}