using Xunit;

namespace BrasilHelperTests;

public interface IBrasilHelperForTest
{
    bool EhCpfValido(string? input);
    bool EhCpfValido(long? input);
    bool EhCnpjValido(string? input);
    bool EhCnpjValido(long? input);
}

public abstract class BrasilHelperTestsBase<TBrasilHelper> : IClassFixture<TBrasilHelper> where TBrasilHelper : class, IBrasilHelperForTest
{
    private readonly TBrasilHelper _instance;

    public BrasilHelperTestsBase(TBrasilHelper brasilHelper)
    {
        _instance = brasilHelper;
    }

    [Fact]
    public void EhCpf_InvalidoSeNull()
    {
        long? arg = null;
        string? args = null;
        Assert.False(_instance.EhCpfValido(arg));
        Assert.False(_instance.EhCpfValido(args));
    }

    [Fact]
    public void EhCpf_InvalidoSeEmpty()
    {
        Assert.False(_instance.EhCpfValido(string.Empty));
        Assert.False(_instance.EhCpfValido(""));
        Assert.False(_instance.EhCpfValido("           "));
    }

    [Fact]
    public void EhCpf_RepeticaoNaoPermitida()
    {
        Assert.False(_instance.EhCpfValido(00000000000));
        Assert.False(_instance.EhCpfValido(11111111111));
        Assert.False(_instance.EhCpfValido(22222222222));
        Assert.False(_instance.EhCpfValido(33333333333));
        Assert.False(_instance.EhCpfValido(44444444444));
        Assert.False(_instance.EhCpfValido(55555555555));
        Assert.False(_instance.EhCpfValido(66666666666));
        Assert.False(_instance.EhCpfValido(77777777777));
        Assert.False(_instance.EhCpfValido(88888888888));
        Assert.False(_instance.EhCpfValido(99999999999));
        Assert.False(_instance.EhCpfValido("00000000000"));
        Assert.False(_instance.EhCpfValido("111111111-11"));
        Assert.False(_instance.EhCpfValido("22222222222"));
        Assert.False(_instance.EhCpfValido("333333333-33"));
        Assert.False(_instance.EhCpfValido("444444444 44"));
        Assert.False(_instance.EhCpfValido("555555555-55"));
        Assert.False(_instance.EhCpfValido("   66666666666"));
        Assert.False(_instance.EhCpfValido("77777777777    "));
        Assert.False(_instance.EhCpfValido("888.888.888-88"));
        Assert.False(_instance.EhCpfValido(" 999.999.999-99 "));
    }

    [Fact]
    public void EhCpf_QtdeDigitosInvalida()
    {
        Assert.False(_instance.EhCpfValido(1243346809));
        Assert.False(_instance.EhCpfValido(124334680940));
        Assert.False(_instance.EhCpfValido("1243346809"));
        Assert.False(_instance.EhCpfValido("124334680940"));

    }


    [Fact]
    public void EhCpf_DigitoVerificadorInvalido()
    {
        Assert.False(_instance.EhCpfValido(12433468090));
        Assert.False(_instance.EhCpfValido(12433468084));
        Assert.False(_instance.EhCpfValido("12433468090"));
        Assert.False(_instance.EhCpfValido("12433468084"));

    }

    [Fact]
    public void EhCpf_IgnorarEspacosExtras()
    {
        Assert.True(_instance.EhCpfValido("  12433468094"));
        Assert.True(_instance.EhCpfValido("12433468094   "));
        Assert.True(_instance.EhCpfValido("124 33468094"));
        Assert.True(_instance.EhCpfValido("124334680 94"));
        Assert.True(_instance.EhCpfValido("124334680- 94"));
        Assert.True(_instance.EhCpfValido("124334680 -94"));
        Assert.True(_instance.EhCpfValido("124334680 -94"));
        Assert.True(_instance.EhCpfValido("124 334 680 94"));
        Assert.True(_instance.EhCpfValido("124 334 680 - 94"));
        Assert.True(_instance.EhCpfValido("124 334 68094"));
        Assert.True(_instance.EhCpfValido("  124 334 680 - 94   "));
    }

    [Fact]
    public void EhCpf_IgnorarCaracteresExtras()
    {
        Assert.True(_instance.EhCpfValido("=12433468094"));
        Assert.True(_instance.EhCpfValido("12433468094."));
        Assert.True(_instance.EhCpfValido("124,334,680-94"));
        Assert.True(_instance.EhCpfValido("124334680/94"));
        Assert.True(_instance.EhCpfValido(@"124334680\94"));
        Assert.True(_instance.EhCpfValido("124334680–94"));
        Assert.True(_instance.EhCpfValido("124-334-680-94"));
        Assert.True(_instance.EhCpfValido("124334680_94"));
        Assert.True(_instance.EhCpfValido("124,334.680-94"));
        Assert.True(_instance.EhCpfValido("124 . 334 . 680 - 94"));
        Assert.True(_instance.EhCpfValido("124334680--94"));
        Assert.True(_instance.EhCpfValido("CPF: 124.334.680-94 "));
    }

    [Fact]
    public void EhCpf_TodosValidos()
    {
        // https://www.4devs.com.br/gerador_de_cpf
        Assert.True(_instance.EhCpfValido(01799819000));
        Assert.True(_instance.EhCpfValido(46427295004));
        Assert.True(_instance.EhCpfValido(36422626002));
        Assert.True(_instance.EhCpfValido(11180935004));
        Assert.True(_instance.EhCpfValido(28007679014));
        Assert.True(_instance.EhCpfValido(34636409027));
        Assert.True(_instance.EhCpfValido(03876101034));
        Assert.True(_instance.EhCpfValido(25227728054));
        Assert.True(_instance.EhCpfValido(01198693061));
        Assert.True(_instance.EhCpfValido(11039233074));
        Assert.True(_instance.EhCpfValido(00366515080));
        Assert.True(_instance.EhCpfValido(76416046091));
        Assert.True(_instance.EhCpfValido(59944975095));
        Assert.True(_instance.EhCpfValido("017.998.190-00"));
        Assert.True(_instance.EhCpfValido("464.272.950-04"));
        Assert.True(_instance.EhCpfValido("364.226.260-02"));
        Assert.True(_instance.EhCpfValido("111.809.350-04"));
        Assert.True(_instance.EhCpfValido("280.076.790-14"));
        Assert.True(_instance.EhCpfValido("346.364.090-27"));
        Assert.True(_instance.EhCpfValido("038.761.010-34"));
        Assert.True(_instance.EhCpfValido("25227728054"));
        Assert.True(_instance.EhCpfValido("01198693061"));
        Assert.True(_instance.EhCpfValido("11039233074"));
        Assert.True(_instance.EhCpfValido("00366515080"));
        Assert.True(_instance.EhCpfValido("76416046091"));
        Assert.True(_instance.EhCpfValido("59944975095"));
    }

    [Fact]
    public void EhCnpj_InvalidoSeNull()
    {
        long? arg = null;
        string? args = null;
        Assert.False(_instance.EhCnpjValido(arg));
        Assert.False(_instance.EhCnpjValido(args));
    }

    [Fact]
    public void EhCnpj_InvalidoSeEmpty()
    {
        Assert.False(_instance.EhCnpjValido(string.Empty));
        Assert.False(_instance.EhCnpjValido(""));
        Assert.False(_instance.EhCnpjValido("           "));
    }
    [Fact]
    public void EhCnpj_RepeticaoNaoPermitida()
    {
        Assert.False(_instance.EhCpfValido(00000000000000));
        Assert.False(_instance.EhCpfValido(11111111111111));
        Assert.False(_instance.EhCpfValido(22222222222222));
        Assert.False(_instance.EhCpfValido(33333333333333));
        Assert.False(_instance.EhCpfValido(44444444444444));
        Assert.False(_instance.EhCpfValido(55555555555555));
        Assert.False(_instance.EhCpfValido(66666666666666));
        Assert.False(_instance.EhCpfValido(77777777777777));
        Assert.False(_instance.EhCpfValido(88888888888888));
        Assert.False(_instance.EhCpfValido(99999999999999));
        Assert.False(_instance.EhCpfValido("00000000000000"));
        Assert.False(_instance.EhCpfValido("111111111-11111"));
        Assert.False(_instance.EhCpfValido("22222222222222"));
        Assert.False(_instance.EhCpfValido("333333333-33333"));
        Assert.False(_instance.EhCpfValido("444444444 44444"));
        Assert.False(_instance.EhCpfValido("555555555-55555"));
        Assert.False(_instance.EhCpfValido("   66666666666666"));
        Assert.False(_instance.EhCpfValido("77777777777777    "));
        Assert.False(_instance.EhCpfValido("888.888.888-88888"));
        Assert.False(_instance.EhCpfValido(" 999.999.999-99999 "));
    }
    [Fact]
    public void EhCnpj_QtdeDigitosInvalida()
    {
        Assert.False(_instance.EhCnpjValido(98712000123));
        Assert.False(_instance.EhCnpjValido(098765400014321));
       Assert.False(_instance.EhCnpjValido("98712000123"));
       Assert.False(_instance.EhCnpjValido("098765400014321"));
    }

    [Fact]
    public void EhCnpj_DigitoVerificadorInvalido()
    {
        Assert.False(_instance.EhCnpjValido(77821894000112));
        Assert.False(_instance.EhCnpjValido(12088799000124));
        Assert.False(_instance.EhCnpjValido("77.821.894/0001-12"));
        Assert.False(_instance.EhCnpjValido("12.088.799/0001-24"));
    }
    
    [Fact]
    public void EhCnpj_IgnorarEspacosExtras()
    {
        Assert.True(_instance.EhCnpjValido("  12088799000107"));
        Assert.True(_instance.EhCnpjValido("12088799000107   "));
        Assert.True(_instance.EhCnpjValido("120  88799000107"));
        Assert.True(_instance.EhCnpjValido("120887990001   07"));
        Assert.True(_instance.EhCnpjValido("120887990001- 07"));
        Assert.True(_instance.EhCnpjValido("120887990001 -07"));
        Assert.True(_instance.EhCnpjValido("120887990001  -07"));
        Assert.True(_instance.EhCnpjValido("12 088 79 90001 07"));
        Assert.True(_instance.EhCnpjValido("12 088 799 0001 - 07"));
        Assert.True(_instance.EhCnpjValido("12 088 799 000107"));
        Assert.True(_instance.EhCnpjValido("  12 088 799 0001 - 07  "));
    }

    [Fact]
    public void EhCnpj_IgnorarCaracteresExtras()
    {
        Assert.True(_instance.EhCnpjValido("=12088799000107"));
        Assert.True(_instance.EhCnpjValido("12088799000107."));
        Assert.True(_instance.EhCnpjValido("12,088,799/0001-07"));
        Assert.True(_instance.EhCnpjValido("120887990001/07"));
        Assert.True(_instance.EhCnpjValido(@"120887990001\07"));
        Assert.True(_instance.EhCnpjValido("120887990001-07"));
        Assert.True(_instance.EhCnpjValido("12-088-799-0001-07"));
        Assert.True(_instance.EhCnpjValido("120887990001_07"));
        Assert.True(_instance.EhCnpjValido("12088.799/0001-07"));
        Assert.True(_instance.EhCnpjValido("12 . 088 . 799 / 0001 - 07"));
        Assert.True(_instance.EhCnpjValido("120887990001--07"));
        Assert.True(_instance.EhCnpjValido("CNPJ: 12.088.799/0001-07 "));
    }
  
    [Fact]
    public void EhCnpj_TodosValidos()
    {
        // https://www.4devs.com.br/gerador_de_cpf
        Assert.True(_instance.EhCnpjValido(61406832000105));
        Assert.True(_instance.EhCnpjValido(90174036000184));
        Assert.True(_instance.EhCnpjValido(08193572000146));
        Assert.True(_instance.EhCnpjValido(07256215000117));
        Assert.True(_instance.EhCnpjValido(74409787000186));
        Assert.True(_instance.EhCnpjValido(01839583000101));
        Assert.True(_instance.EhCnpjValido(21857384000133));
        Assert.True(_instance.EhCnpjValido(21857384000133));
        Assert.True(_instance.EhCnpjValido(21857384000133));
        Assert.True(_instance.EhCnpjValido(21857384000133));
        Assert.True(_instance.EhCnpjValido(21857384000133));       
        Assert.True(_instance.EhCnpjValido("61.406.832/0001-05"));
        Assert.True(_instance.EhCnpjValido("90.174.036/0001-84"));
        Assert.True(_instance.EhCnpjValido("08.193.572/0001-46"));
        Assert.True(_instance.EhCnpjValido("07.256.215/0001-17"));
        Assert.True(_instance.EhCnpjValido("74.409.787/0001-86"));
        Assert.True(_instance.EhCnpjValido("01839583000101"));
        Assert.True(_instance.EhCnpjValido("21857384000133"));
        Assert.True(_instance.EhCnpjValido("21857384000133"));
        Assert.True(_instance.EhCnpjValido("21857384000133"));
        Assert.True(_instance.EhCnpjValido("21857384000133"));
        Assert.True(_instance.EhCnpjValido("21857384000133"));     
    }
}

