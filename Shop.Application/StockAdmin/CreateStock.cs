﻿using Shop.Database;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    class CreateStock
    {
        private ApplicationDbContext _ctx;
        public CreateStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> Do(Request request)
        {
            var stock = new Stock
            {
                Description = request.Description,
                Qty = request.Qty,
                ProductId = request.ProductId,
            };
            
            _ctx.Stocks.Add(stock);
            await _ctx.SaveChangesAsync();
            
            return new Response
            {
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty
            };
        }

        public class Request
        {
            public string Description { get; set; }
            public int Qty { get; set; }
            public int ProductId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}