namespace InterviewManagementSystem.Application.Shared;

public struct PaginationRequest
{

    private int pageSize = 5;
    private int pageIndex = 1;


    public int PageSize
    {
        readonly get => pageSize;
        set
        {
            if (value > 0)
                pageSize = value;
        }
    }

    public int PageIndex
    {
        readonly get => pageIndex;
        set
        {
            if (value > 0)
                pageIndex = value;
        }
    }

    public PaginationRequest()
    {
    }
}
