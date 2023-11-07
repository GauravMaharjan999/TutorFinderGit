﻿using Kachuwa.Log;
using Kachuwa.Training.Service;
using Kachuwa.Web.API;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kachuwa.Training.Api
{
    [Route("api/v1/course")]
    public   class CourseApiController : BaseApiController
    {
        private readonly ICourseService _courseService;
        private ILogger _logger;

        public CourseApiController(ICourseService courseService,ILogger logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [Route("gridList")]
        public async Task<object> CourseList([FromQuery]int pq_curpage = 1, [FromQuery] int pq_rpp = 20)
        {

            try
            {
                var data = await _courseService.CourseCrudService.GetListPagedAsync(pq_curpage, pq_rpp, 1, "Where IsDeleted=@isDel", "Id asc", new { @isDel = false });
                return HttpResponse(200, "Success", data, pq_curpage);
            }
            catch (Exception ex)
            {

                _logger.Log(LogType.Error, () => ex.Message, ex);
                return HttpResponse(500, ex.Message, "");
            }
        }
    }
}
