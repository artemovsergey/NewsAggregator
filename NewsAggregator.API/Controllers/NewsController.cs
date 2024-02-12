using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Domen.Models;
using NewsAggregator.Infrastucture.Data;

namespace NewsAggregator.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{

    private const string lentarss = "https://lenta.ru/rss";
    private const string habrss = "https://habr.com/ru/rss/articles/";

    private readonly NewsAggregationContext _context;
    private readonly INewsService _newsService;

    public NewsController(NewsAggregationContext context, INewsService newsService)
    {
        _context = context;
        _newsService = newsService;
    }

    // GET: api/News
    [HttpGet]
    public async Task<ActionResult<ApiResult<News>>> GetNewsList(
                                                        string? sortColumn = null,
                                                        string? sortOrder = null,
                                                        int pageIndex = 0,
                                                        int pageSize = 10,
                                                        string? filterColumn = null,
                                                        string? filterQuery = null)
    {
        var users = await _newsService.GetNews(pageIndex,
                                             pageSize,
                                             sortColumn,
                                             sortOrder,
                                             filterColumn,
                                             filterQuery);

        return new ApiResult<News>((List<News>)users,
                                    _context.NewsList.Count(),
                                    pageIndex,
                                    pageSize,
                                    sortColumn,
                                    sortOrder,
                                    filterColumn,
                                    filterQuery);

    }


    [HttpGet("fetchdata")]
    public async Task<IActionResult> FetchDataLentaRss()
    {
        await _newsService.FetchNews(lentarss);
        return Ok();
    }

    [HttpGet("habrss")]
    public async Task<IActionResult> FetchDataHabrRss()
    {
        await _newsService.FetchNews(habrss);
        return Ok();
    }

}
