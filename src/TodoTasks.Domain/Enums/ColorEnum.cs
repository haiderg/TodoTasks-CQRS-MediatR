namespace TodoTasks.Domain.Enums;

public enum TaskColorEnum
{
    Green,
    White,
    Red,
    Yellow
}


public static class TaskColorExtensions
{
    public static string ToHexCode(this TaskColorEnum color)
    {
        return color switch
        {
            TaskColorEnum.Green => "#28a745",
            TaskColorEnum.White => "#ffffff",
            TaskColorEnum.Red => "#dc3545",
            TaskColorEnum.Yellow => "#ffc107",
            _ => "#000000"
        };
    }
}


