namespace Electric.Application.Contracts.Dto.Identity.Users
{
    /// <summary>
    /// 获取所有用户接口信息
    /// </summary>
    public class UserAllDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 全名：姓名
        /// </summary>
        public string? FullName { get; set; }
    }
}
