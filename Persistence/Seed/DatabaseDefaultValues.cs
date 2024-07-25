namespace Persistence.Seed;

internal static class DatabaseDefaultValues
{
    internal static class ProjectTypes
    {
        internal static string FoundedId = "4337b320-445e-438f-a9fa-ee5c9c86c779";
        internal static string QualifiedId = "a6f7241f-ca03-4daa-a800-b957f1090ec2";
        internal static string ConstructionId = "1e65ec31-1b8a-4e92-bfb0-dda961a6b59c";
        internal static string DesignSupervisionId = "b5815ea4-b8d2-4c6b-a409-9c13a1d0e969";
    }

    internal static class MaintenanceTypes
    {
        internal static Guid NewId = Guid.Parse("4337b320-445e-438f-a9fa-ee5c9c86c779");
        internal static Guid PeriodicId = Guid.Parse("a6f7241f-ca03-4daa-a800-b957f1090ec2");
        internal static Guid UrgentId = Guid.Parse("1e65ec31-1b8a-4e92-bfb0-dda961a6b59c");
    }

    internal static class StorageTypes {
        internal static Guid Store = Guid.Parse("4337b320-445e-438f-a9fa-ee5c9c86c779");
        internal static Guid Request = Guid.Parse("a6f7241f-ca03-4daa-a800-b957f1090ec2");
        internal static Guid Receive = Guid.Parse("1e65ec31-1b8a-4e92-bfb0-dda961a6b59c");
    }

    internal static class TenderTypes
    {
        internal static Guid ExternalId = Guid.Parse("4337b320-445e-438f-a9fa-ee5c9c86c779");
        internal static Guid InternalId = Guid.Parse("a6f7241f-ca03-4daa-a800-b957f1090ec2");
        internal static Guid FurnitureId = Guid.Parse("1e65ec31-1b8a-4e92-bfb0-dda961a6b59c");
    }
}