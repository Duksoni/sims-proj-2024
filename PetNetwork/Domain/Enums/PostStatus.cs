namespace PetNetwork.Domain.Enums;

public enum PostStatus
{
    PendingApproval, // interpret it as post request
    Rejected, // volunteer rejected the request
    Active,
    Deleted
}

