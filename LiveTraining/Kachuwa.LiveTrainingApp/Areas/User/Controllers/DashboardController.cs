using System;
using System.Linq;
using System.Threading.Tasks;
using Kachuwa.Dash.Services;
using Kachuwa.Identity.Extensions;
using Kachuwa.Log;
using Kachuwa.Training.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kachuwa.KLiveApp.Areas.Controllers
{
    [Area("User")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEnrollService _enrollService;
        private readonly IPaymentLogService _paymentLogService;

        public DashboardController(ILogger logger ,IEnrollService enrollService ,IPaymentLogService paymentLogService)
        {
            _logger = logger;
            _enrollService = enrollService;
            _paymentLogService = paymentLogService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("user/video")]
        public IActionResult Video()
        {
            return View();
        }

        [Route("user/playlist")]
        public IActionResult Playlist()
        {
            return View();
        }
        [Route("user/setting")]
        public IActionResult Setting()
        {
            return View();
        }
        [Route("user/order")]
        public async Task<IActionResult> Order()
        {
            var CurrentUserId = Convert.ToInt32(User.Identity.GetIdentityUserId());
            var EnrollCourseList = await _enrollService.EnrollListByUserId(CurrentUserId);
            //if (EnrollCourseList == null)
            //{
            //    return View();
            //}
            return View(EnrollCourseList);
        }

        [Route("user/payment")]
        public async Task<IActionResult> Payment([FromQuery]int pq_curpage = 1, [FromQuery] int pq_rpp = 20)
        {
            var CurrentUserId = Convert.ToInt32(User.Identity.GetIdentityUserId());
            var paymentList = await _paymentLogService.PaymentListbyUserId(CurrentUserId, pq_curpage, pq_rpp);
            return View(paymentList);
        }

        [Route("user/payment/detail/{id}")]
        public async Task<IActionResult> PaymentDetail([FromRoute] int id)
        {
            var paymentdetail = await _paymentLogService.PaymentDetailByPaymentId(id);
            return View(paymentdetail);
        }

    }
}