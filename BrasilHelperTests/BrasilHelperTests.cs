namespace BrasilHelperTests;

public class BrasilHelper : IBrasilHelperForTest
{
    public bool EhCpfValido(string? input)
    {
        return Marrari.Common.BrasilHelper.EhCpfValido(input);
    }
    public bool EhCpfValido(long? input)
    {
        return Marrari.Common.BrasilHelper.EhCpfValido(input);
    }
    public bool EhCnpjValido(string? input)
    {
        return Marrari.Common.BrasilHelper.EhCnpjValido(input);
    }
    public bool EhCnpjValido(long? input)
    {
        return Marrari.Common.BrasilHelper.EhCnpjValido(input);
    }
}

public class BrasilHelperTests : BrasilHelperTestsBase<BrasilHelper>
{
    public BrasilHelperTests(BrasilHelper brasilHelper) 
        : base(brasilHelper)
    {
    }
}
