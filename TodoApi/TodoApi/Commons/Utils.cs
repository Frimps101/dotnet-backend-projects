namespace TodoApi.Commons;

public static class Utils
{
    public static int GetOffset(int pageIndex, int pageSize)
    {
        if (pageIndex < 1)
            pageIndex = 1;
        return (pageIndex - 1) * pageSize;
    }
}