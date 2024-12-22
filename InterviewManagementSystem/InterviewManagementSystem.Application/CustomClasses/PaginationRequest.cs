namespace InterviewManagementSystem.Application.CustomClasses;

public struct PaginationRequest
{

    private int pageSize = 5;
    private int pageIndex = 1;


    public int PageSize
    {
        get => pageSize;
        set
        {
            if (value > 0)
                pageSize = value;
        }
    }

    public int PageIndex
    {
        get => pageIndex;
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
