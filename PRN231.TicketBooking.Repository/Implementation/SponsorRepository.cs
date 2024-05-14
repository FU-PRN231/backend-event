using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
    {
        public readonly IUnitOfWork _unitOfWork;
        public SponsorRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Sponsor>> CreateSponsor(Dictionary<string, SponsorDto> dto)
        {
            List<Sponsor> result = null;
            try
            {
                result = new List<Sponsor>();
                foreach(var item in dto)
                {
                    if((await GetSponsorByName(item.Value.Name)) == null)
                    {
                        var sponsor = new Sponsor
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Value.Name,
                            Description = item.Value.Description,
                            Img = string.Empty,
                            AccountId = item.Key
                        };
                         result.Add(sponsor);
                        await this.Insert(sponsor);
                    } else
                    {
                        result.Add(await GetSponsorByName(item.Value.Name));
                    }
                }
                //await _unitOfWork.SaveChangeAsync();
            }catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<Sponsor> GetSponsorByName(string name)
        {
            return await this.GetByExpression(s => s.Name == name);
        }

       
    }
}
