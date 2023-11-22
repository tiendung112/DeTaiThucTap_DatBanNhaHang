using DatBanNhaHang.Context;
using DatBanNhaHang.Entities;
using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{
    public class UserConverter
    {


        public UserDTO EntityToDTO(User user)
        {
            return new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                Gender = user.Gender,
                LastName = user.LastName

            };
        }
    }
}
