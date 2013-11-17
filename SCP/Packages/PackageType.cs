namespace TVA.Scarabaeus.SCP.Packages
{
    public enum PackageType : byte
    {
        AnswerOk,
        AnswerIncorrectCommand,
        AnswerReSendLast,
        SetupConnection,
        SetLegPosition,
    }
}