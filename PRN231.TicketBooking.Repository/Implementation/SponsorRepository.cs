﻿using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SponsorRepository(IUnitOfWork unitOfWork, IGenericDAO<Sponsor> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Sponsor> CreateSponsor(CreateSponsorDto dto)
        {
			var result = await GetSponsorByName(dto.Name);
            try
            {
				if (result == null)
				{
					var sponsor = new Sponsor
					{
						Id = Guid.NewGuid(),
						Name = dto.Name,
						Description = dto.Description,
						Img = string.Empty
					};
					await _dao.Insert(sponsor);
					result = sponsor;
				}
			}
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

		public async Task<List<Sponsor>> CreateListSponsor(Dictionary<string, CreateSponsorDto> dto)
		{
			List<Sponsor> result = null;
			try
			{
				result = new List<Sponsor>();
				foreach (var item in dto)
				{
					if (await GetSponsorByName(item.Value.Name) == null)
					{
						var sponsor = new Sponsor
						{
							Id = Guid.NewGuid(),
							Name = item.Value.Name,
							Description = item.Value.Description,
							Img = string.Empty
						};
						result.Add(sponsor);
						await _dao.Insert(sponsor);
					}
					else
					{
						result.Add(await GetSponsorByName(item.Value.Name));
					}
				}
				//await _unitOfWork.SaveChangeAsync();
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		public async Task<Sponsor> GetSponsorByName(string name)
        {
            return await _dao.GetByExpression(s => s.Name == name);
        }

        public async Task<PagedResult<Sponsor>> GetAllSponsor(Expression<Func<Sponsor, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Sponsor, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Sponsor, object>>[]? includes)
        {
            PagedResult<Sponsor> result = null;
            try
            {
                result = new PagedResult<Sponsor>();
                result = await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}