public static class Enums
{
    public enum Direction
    {
        Forward,
        Right,
        Backwards,
        Left,
    }
    public enum Axis
    {
        x,
        y,
        z,
    }
    public enum InteractType
    {
        Trigger,
        Collision,
        Click,
    }
    public enum CharacterExpression
    {
        Default,
        None,
        Angry,
        Confused,
        Excited,
        Happy,
        Nervous,
        Sad,
        Shocked,
        Sick,
    }
    public enum AnimationActions
    {
        None,
        Idle,
        IdleSpice,
        Walk,
        Run,
    }
    public enum Inequalities
    {
        None,
        GreaterThan,
        LessThan,
        Equal,
    }
    public enum EffectFlag
    {
        None,
        FadeToBlack,
        FadeFromBlack,
        JumpToLevel1,
        ShowImage,
        HideImage,
        PlayCredits,
    }
}
