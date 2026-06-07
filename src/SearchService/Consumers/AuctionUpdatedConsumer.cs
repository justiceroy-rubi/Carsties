using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
    {
        private readonly IMapper _mapper;

        public AuctionUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine("--> Consuming auction created: " + context.Message.Id);

            var auction = _mapper.Map<AuctionUpdated>(context.Message);
            var result = await DB.Update<Item>()
                .Match(a => a.ID == auction.Id)
                .Modify(a => a.Make, auction.Make)
                .Modify(a => a.Model, auction.Model)
                .Modify(a => a.Year, auction.Year)
                .Modify(a => a.Color, auction.Color)
                .Modify(a => a.Mileage, auction.Mileage)
                .ExecuteAsync();

            if(!result.IsAcknowledged)
            {
                throw new MessageException(typeof(AuctionUpdated), "Problem updating mongodb");
            }
        }
    }
}