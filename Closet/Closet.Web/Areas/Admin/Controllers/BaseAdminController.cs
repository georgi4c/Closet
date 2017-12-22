using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static Closet.Web.WebConstants;

namespace Closet.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}
