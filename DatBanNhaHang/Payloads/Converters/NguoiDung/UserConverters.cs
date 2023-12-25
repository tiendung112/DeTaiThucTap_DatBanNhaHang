using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{
    public class UserConverters
    {

        public UserDTO EntityToDTO(User user)
        {
            return new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Name = user.Name,
                Gender = user.Gender,
                UserID=user.id
            };
        }
    }
}
