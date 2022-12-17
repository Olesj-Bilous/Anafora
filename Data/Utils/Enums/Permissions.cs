namespace AnaforaData.Utils.Enums
{
    [Flags]
    public enum Permissions
    {
        None
            = 0,
        Read
            = 1,
        Update
            = 1 << 1,
        Create
            = 1 << 2,
        Delete
            = 1 << 3,
        RequestRead
            = 1 << 4,
        RequestUpdate
            = 1 << 5,
        RequestCreate
            = 1 << 6,
        RequestDelete
            = 1 << 7,
        DecideRead
            = 1 << 8,
        DecideUpdate
            = 1 << 9,
        DecideCreate
            = 1 << 10,
        DecideDelete
            = 1 << 11
    }
}