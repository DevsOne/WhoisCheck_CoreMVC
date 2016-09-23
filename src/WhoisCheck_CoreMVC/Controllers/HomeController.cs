using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhoisCheck_CoreMVC.Models;

namespace WhoisCheck_CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TldServerList()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CheckDomain(DomainViewModels model)
        {
            string whoisText = null;
            var tldServer = Whois.GetWhoisServerName(model.DomainName);

            if (tldServer != null) { whoisText = await Whois.GetWhoisInformation(tldServer, model.DomainName); }
            else { whoisText = await Whois.GetWhoisInformation("whois.verisign-grs.com", model.DomainName + ".com"); }

            whoisText = whoisText.Replace("\r\n", "<br/>");
            model.DomainDetails = whoisText;
            return View("Index", model);
        }
    }
}
