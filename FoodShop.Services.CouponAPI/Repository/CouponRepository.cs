using AutoMapper;
using FoodShop.Services.CouponAPI.DBContext;
using FoodShop.Services.CouponAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _db;

        private IMapper _mapper;
        public CouponRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(x=>x.CouponCode == couponCode);
            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}
