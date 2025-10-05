namespace Henshi.Flashcards.Domain.ValueObjects;

public enum Grade
{
    /// <summary>
    /// Complete failure to recall - card needs immediate review
    /// </summary>
    Again,

    /// <summary>
    /// Difficult to recall but eventually remembered
    /// </summary>
    Hard,

    /// <summary>
    /// Normal recall with appropriate effort
    /// </summary>
    Good,

    /// <summary>
    /// Effortless recall - card was too easy
    /// </summary>
    Easy
}

public static class GradeMapper
{
    public static FSRS.Core.Enums.Rating ToFsrs(Grade grade)
    {
        return grade switch
        {
            Grade.Again => FSRS.Core.Enums.Rating.Again,
            Grade.Hard => FSRS.Core.Enums.Rating.Hard,
            Grade.Good => FSRS.Core.Enums.Rating.Good,
            Grade.Easy => FSRS.Core.Enums.Rating.Easy,
            _ => FSRS.Core.Enums.Rating.Again
        };
    }

    public static Grade FromFsrs(FSRS.Core.Enums.Rating rating)
    {
        return rating switch
        {
            FSRS.Core.Enums.Rating.Again => Grade.Again,
            FSRS.Core.Enums.Rating.Hard => Grade.Hard,
            FSRS.Core.Enums.Rating.Good => Grade.Good,
            FSRS.Core.Enums.Rating.Easy => Grade.Easy,
            _ => Grade.Again
        };
    }
}