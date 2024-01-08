namespace IAGTO.Enforcer.Api;
public class UserMember
{
    public DateTime JoinedDate { get; set; }
    public int MembershipNumber { get; set; }
    public Guid MemberId { get; set; }
    public Guid ContactId { get; set; }
    public Guid UserId { get; set; }
    public Guid PersonId { get; set; }
    public string MembershipTypeName { get; set; } = null!;
    public int MembershipTypeId { get; set; }
    public string MembershipCategoryName { get; set; } = null!;
    public int MembershipCategoryId { get; set; }

    public List<Profile> Profiles { get; set; }

    public UserMember()
    {
        Profiles = new List<Profile>();
    }

    public class Profile
    {
        public string BusinessName { get; set; } = null!;
        public string ProfileCategoryName { get; set; } = null!;
        public int ProfileCategoryId { get; set; }
    }
}
