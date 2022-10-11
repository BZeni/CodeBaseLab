namespace Marrari.Common
{
    /// <summary>
    /// Classe Validadora de CPF e CNPJ em string/long
    /// </summary>
    public class BrasilHelper
    {
        /// <summary>
        /// Faz a validação do CPF utilizando o algoritmo de validação
        /// </summary>
        /// <param name="input">CPF em formato string para a validação</param>
        /// <returns>Falso caso o CPF seja inválido, true caso o CPF seja Válido </returns>
        public static bool EhCpfValido(string? input)
        {         
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (CalculaNumeroDeDigitos(input) != 11)
            {
                return false;
            }
            if (VerficarRepeticao(input) is true)
            {
                return false;
            }            
            var digitoVerificador1 = 0;
            var digitoVerificador2 = 0;
            for (int posicao = 0; posicao < 9; posicao++)
            {
                var digito = ObterDigito(input, posicao);
                digitoVerificador1 += digito * (10 - posicao);
                digitoVerificador2 += digito * (11 - posicao);
            }
            var resultado1 = digitoVerificador1 % 11;
            resultado1 = GetCalculo(resultado1);
            if (ObterDigito(input, 9) != resultado1)
            {
                return false;
            }
            digitoVerificador2 += resultado1 * 2;
            var resultado2 = digitoVerificador2 % 11;
            resultado2 = GetCalculo(resultado2);
            if (ObterDigito(input, 10) != resultado2)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Validador de CPF em formato long utilizando o algoritmo validador
        /// </summary>
        /// <param name="input">CPF a ser validado</param>
        /// <returns>Falso caso o CPF seja inválido, true caso o CPF seja Válido </returns>
        public static bool EhCpfValido(long? input)
        {
            if (input is null)
            {
                return false;
            }
            if (VerificarDigitos(input) > 11)
            {
                return false;
            }
            if (VerificarRep(input) is true)
            {
                return false;
            }

            long? cpf = input / 10;
            long? primeiroDigito = CalcularDigito(cpf);
            long? segundoDigito = CalcularDigito(input);

            primeiroDigito = GetCalculo(primeiroDigito);
            segundoDigito = GetCalculo(segundoDigito);

            long? digito = input / 10;
            long? digitoVerificador1 = digito % 10;
            long? digitoVerificador2 = input % 10;
            if (primeiroDigito != digitoVerificador1 || segundoDigito != digitoVerificador2)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///  Validador de CNPJ em formato string utilizando o algoritmo validador
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool EhCnpjValido(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (CalculaNumeroDeDigitos(input) != 14)
            {
                return false;
            }
            if (VerficarRepeticao(input) is true)
            {
                return false;
            }

            int[] Multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao < 12)
                    {
                        totalDigito1 += digito * Multiplicador1[posicao];
                        totalDigito2 += digito * Multiplicador2[posicao];
                    }
                    else if (posicao == 12)
                    {
                        var dv1 = (totalDigito1 % 11);
                        dv1 = dv1 < 2
                            ? 0
                            : 11 - dv1;

                        if (digito != dv1)
                        {
                            return false;
                        }

                        totalDigito2 += dv1 * Multiplicador2[12];
                    }
                    else if (posicao == 13)
                    {
                        var dv2 = (totalDigito2 % 11);

                        dv2 = dv2 < 2
                            ? 0
                            : 11 - dv2;

                        if (digito != dv2)
                        {
                            return false;
                        }
                    }
                    posicao++;
                }
            }
            return true;
        }
        /// <summary>
        /// Validador de CNPJ em formato long utilizando o algoritmo validador
        /// </summary>
        /// <param name="input">CNPJ a ser validado</param>
        /// <returns>Falso caso o CNPJ seja inválido, true caso o CNPJ seja Válido </returns>
        public static bool EhCnpjValido(long? input)
        {
            if (input is null)
            {
                return false;
            }
            if (VerificarDigitos(input) > 14)
            {
                return false;
            }
            if (VerificarRep(input) is true)
            {
                return false;
            }
            long? primeiroCalculo = input / 10;
            long? segundoCalculo = input / 1;
            long? primeiroDigito = CalcularDigitoCnpj(primeiroCalculo);
            long? segundoDigito = CalcularDigitoCnpj(segundoCalculo);
            primeiroDigito = GetCalculo(primeiroDigito);
            segundoDigito = GetCalculo(segundoDigito);
            long? digito = input / 10;
            long? digitoVerificador1 = digito % 10;
            long? digitoVerificador2 = input % 10;
            if (primeiroDigito != digitoVerificador1 || segundoDigito != digitoVerificador2)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Verificar a quantidade de digítos
        /// </summary>
        /// <param name="input">CPF a ser validado</param>
        /// <returns>Retornar a quantidade de digitos no input</returns>
        private static int CalculaNumeroDeDigitos(string input)
        {
            var result = 0;
            foreach (char c in input.ToString())
            {
                if (char.IsDigit(c))
                {
                    result++;
                }
            }
            return result;
        }
        /// <summary>
        /// Verificar se há repetição de números iguais em sequência
        /// </summary>
        /// <param name="input">CPF a ser validado</param>
        /// <returns>Verdadeiro caso haja repetição e falso caso contrário</returns>
        private static bool VerficarRepeticao(string input)
        {
            var previous = -1;
            foreach (char c in input.ToString())
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (previous == -1)
                    {
                        previous = digito;
                    }
                    else
                    {
                        if (previous != digito)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Obter o digito Verificador 
        /// </summary>
        /// <param name="input">CPF a ser validado</param>
        /// <param name="posicao">Posição do número desejado</param>
        /// <returns>Retornar o número escolhido</returns>
        private static int ObterDigito(string input, int posicao)
        {
            int count = 0;
            foreach (char c in input)
                if (char.IsDigit(c))
                {
                    if (count == posicao)
                    {
                        return c - '0';
                    }
                    count++;
                }
            return 0;
        }
        /// <summary>
        /// Obter o digito verificador após o calculo da divisão da soma por 11
        /// </summary>
        /// <param name="resultado">Digito verificador</param>
        /// <returns> 0 caso o resultado for menor que 2, caso o resultado for maior que 2 retornará 11 - resultado </returns>
        private static int GetCalculo(int resultado)
        {
            resultado = resultado < 2
                ? 0
                : 11 - resultado;
            return resultado;
        }
        /// <summary>
        /// Obter o digito verificador após o calculo da divisão da soma por 11
        /// </summary>
        /// <param name="resultado">Digito verificador</param>
        /// <returns> 0 caso o resultado for menor que 2, caso o resultado for maior que 2 retornará 11 - resultado </returns>
        public static long? GetCalculo(long? resultado)
        {
            resultado = resultado < 2
                ? 0
                : 11 - resultado;
            return resultado;
        }
        /// <summary>
        /// Verificar a quantidade de digítos
        /// </summary>
        /// <param name="input">CPF/CNPJ a ser validado</param>
        /// <returns>Retornar a quantidade de digitos no input</returns>
        public static long VerificarDigitos(long? input)
        {
            long count = 0;
            while (input > 0)
            {
                input = input / 10;
                ++count;
            }
            return count;
        }
        /// <summary>
        /// Verificar se há repetição de números iguais em sequência
        /// </summary>
        /// <param name="input">CPF/CNPJ a ser validado</param>
        /// <returns>Verdadeiro caso haja repetição e falso caso contrário</returns>
        public static bool VerificarRep(long? input)
        {
            long? digito = input % 10;
            while (input > 0)
            {
                long? ultimoDigito = input % 10;
                input = input / 10;
                if (ultimoDigito != digito)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Calculo para obter o digito verificador 
        /// </summary>
        /// <param name="input">CPF a ser validado</param>
        /// <returns>Retornará o Primeiro digito verificador</returns>
        private static long? CalcularDigito(long? input)
        {
            long mult = 2;
            long? soma = 0;
            while (input > 1)
            {
                input = input / 10;
                long? ultimoDigito = input % 10;
                soma += ultimoDigito * mult;
                mult++;
            }
            long? resultado1 = soma % 11;
            return resultado1;
        }
        /// <summary>
        /// Calculo para obter o digito verificador
        /// </summary>
        /// <param name="input">CNPJ a ser validado </param>
        /// <returns>Retornará o resultado do calculo do digito verificador</returns>
        private static long? CalcularDigitoCnpj(long? input)
        {
            long mult1 = 2;
            long mult2 = 2;

            long? segundaParte = input / 100000000;

            input = input % 1000000000;

            long? soma2 = 0;
            long? soma1 = 0;
            while (input > 1)
            {
                input = input / 10;
                long? ultimoDigito = input % 10;
                soma1 += ultimoDigito * mult1;
                mult1++;
            }
            while (segundaParte > 1)
            {
                segundaParte = segundaParte / 10;
                long? ultimoDigito2 = segundaParte % 10;
                soma2 += ultimoDigito2 * mult2;
                mult2++;
            }
            long? resultado = soma1 + soma2;
            return resultado % 11;
        }
    }
}