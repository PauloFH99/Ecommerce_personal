using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Hello.CamadaNegocio
{
    public class UsuarioCamadaNegocio
    {
        public (bool, string) Criar(Models.Usuario usuario)
        {
            string msg = "";
            bool operacao = false;
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
            //obrigatorio nome de usuario unico
            // senha com min 6 caracteres
            if (usuario.Senha.ToString().Length < 6)
            {
                msg = "Senha muito pequena";
            }
            else if (!usuarioBD.validarLogin(usuario.NomeUsuario))
            {
                msg = "Login já cadastrado.";
            }
            else if (usuarioBD.Criar(usuario))
            {
                msg = "Usuário cadastrado com sucesso.";
                operacao = true;
            }
            else
            {
                msg = "Erro ao cadastrar usuário";
            }
            return (operacao, msg);
        }
        public (bool, Models.Usuario) Validar(string usuarioNome, string senha)
        {
            CamadaAcessoDados.UsuarioBD usuarioBD = new CamadaAcessoDados.UsuarioBD();
            return usuarioBD.Validar(usuarioNome, senha);
        }
        public List<Models.Perfil> ObterPerfis(string descricao)
        {
            CamadaAcessoDados.PerfilBD pbd = new CamadaAcessoDados.PerfilBD();
            return pbd.Pesquisar(descricao);
        }
    }
}
