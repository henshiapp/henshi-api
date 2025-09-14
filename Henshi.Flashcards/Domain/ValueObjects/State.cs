using Npgsql.Replication;

namespace Henshi.Flashcards.Domain.ValueObjects;

public enum State
{
    /// <summary>
    /// Card is being learned for the first time
    /// </summary>
    Learning,

    /// <summary>
    /// Card has graduated to regular review schedule
    /// </summary>
    Review,

    /// <summary>
    /// Card was forgotten and needs to be relearned
    /// </summary>
    Relearning,
}

public static class StateMapper
{
    public static FSRS.Core.Enums.State ToFsrs(State state)
    {
        return state switch
        {
            State.Learning => FSRS.Core.Enums.State.Learning,
            State.Review => FSRS.Core.Enums.State.Review,
            State.Relearning => FSRS.Core.Enums.State.Relearning,
            _ => FSRS.Core.Enums.State.Learning
        };
    }

    public static State FromFsrs(FSRS.Core.Enums.State state)
    {
        return state switch
        {
            FSRS.Core.Enums.State.Learning => State.Learning,
            FSRS.Core.Enums.State.Review => State.Review,
            FSRS.Core.Enums.State.Relearning => State.Relearning,
            _ => State.Learning
        };
    }
}