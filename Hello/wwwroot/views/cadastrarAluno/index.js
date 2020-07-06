
var index = {

    btnCadastrar: function () {

        var nomeUsuario = document.getElementById("Nome").value;
        var senha = document.querySelector("#Senha").value;
        var senhaConf = document.querySelector("#SenhaConf").value;

        if (nomeUsuario.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Informe o nome.";
        }
        else if (senha.trim() == "" || senhaConf.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "As senhas não informadas.";
        }
        else if (senha != senhaConf) {

            document.getElementById("divMsg").innerHTML =
                "As senhas não conferem.";
        }
        else {

            var dados = {
                nomeUsuario,
                senha,
                senhaConf
            }

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(dados)  //serializa
            };

            fetch("/CadastrarAluno/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj.operacao == "true") {
                        window.location.href = "Default";
                    }
                    else {
                        document.getElementById("divMsg").innerHTML = dadosObj.msg;
                        document.getElementById("divMsg").className = "alert alert-danger";
                        document.getElementById("divMsg").style.marginTop = "15px";
                    }


                })
                .catch(function () {

                    document.getElementById("divMsg").innerHTML = "deu erro";
                })


        }

    },
    buscarEstados: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/ObterEstados", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selUF = document.getElementById("selUF");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i]}">
                            ${dadosObj[i]}</option>`;
                    //opts += "<option value='" + dadosObj[i] + "'>" + dadosObj[i] + "</option>"
                }

                selUF.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    buscarCidades: function (uf) {


        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/Cidade/ObterCidades?uf=" + uf, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCidade = document.getElementById("selCidade");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selCidade.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },

    obterDadosEditar: function (id) {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Accept": "application/json",
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/CadastrarUsuario/Obter?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                document.getElementById("usuario").value = dadosObj.nomeUsuario;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },

    selecionarPerfil(id, nome) {
        document.getElementById("hfPerfilId").value = id;
        document.getElementById("divPerfilNome").innerHTML = nome;

        $.fancybox.close();

    },

    abrirPerfil() {

        $.fancybox.open({
            src: 'IndexObterPerfis',
            type: 'iframe',
            opts: {
                afterShow: function (instance, current) {
                    console.info('done!');
                },
                beforeClose: function () {
                    alert("fechou");
                }
            }
        });
    }
}

//iniciar estados
//iniciando a página;
index.buscarEstados();
if (document.getElementById("hfIdEditar") != null) {
    if (document.getElementById("hfIdEditar").value != "") {

        index.obterDadosEditar(document.getElementById("hfIdEditar").value)

    }
}