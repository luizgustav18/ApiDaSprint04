using System.Text.RegularExpressions;
using ApiDaSprint04.Models;

namespace ApiDaSprint04.Services
{
    public class PessoaService
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        public IEnumerable<Pessoa> ObterTodas() => pessoas;

        public string Adicionar(Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.Nome) || pessoa.Nome.Trim().Length < 3)
                return "Nome inválido. Deve conter pelo menos 3 caracteres.";

            if (pessoa.Idade < 16)
                return "Idade inválida.";

            if (!ValidadorCpf.Validar(pessoa.Cpf))
                return "CPF inválido.";

            if (!ValidarEmail(pessoa.Email))
                return "E-mail inválido.";

            if (!ValidarSenha(pessoa.Senha))
                return "Senha inválida. Ela deve conter pelo menos 6 caracteres, incluindo uma letra maiúscula, uma minúscula e um número.";

            if (pessoas.Any(p => p.Email == pessoa.Email))
                return "Já existe uma conta com esse e-mail.";

            pessoas.Add(pessoa);
            return "Pessoa adicionada com sucesso!";
        }


        public string Autenticar(string email, string senha)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Email == email && p.Senha == senha);
            if (pessoa == null)
                return "E-mail ou senha incorretos.";
            return "Login realizado com sucesso!";
        }

        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private bool ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");
            return regex.IsMatch(senha);
        }
    }
}
