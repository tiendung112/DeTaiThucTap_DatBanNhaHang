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
        public ProfileUserDTOs EntityToProfileUserDTOs(User user)
        {
            return new ProfileUserDTOs
            {
                address = user.address,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = user.Gender,
                Name = user.Name,
                SDT = user.SDT,
                UserID = user.id,
                UserName= user.UserName
            };
        }
    }
}
