var indexObterPerfil = {
    
    btnPesquisarOnClick: function () {

        document.getElementById("tbPerfils").style.display = "table";

        var tbodyUsuarios = document.getElementById("tbodyPerfis");
        tbodyUsuarios.innerHTML = `<tr><td colspan="3"><img src=\"/imgs/ajax-loader.gif"\ /></td></tr>`
        document.getElementById("btnPesquisar").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var descricao = encodeURIComponent(document.getElementById("descricao").value);

        fetch("/CadastrarAluno/ObterPerfis?descricao=" + descricao, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `<tr data-id="${dadosObj[i].id}">
                            <td>${dadosObj[i].id}</td>
                            <td>${dadosObj[i].descricao}</td>
                              <td>         
                                <a href="javascript:;" onclick="indexObterPerfil.selecionar(${dadosObj[i].id},'${dadosObj[i].descricao}')"> Selecionar</a>
                            </td>
                         </tr>`
                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }

                tbodyUsuarios.innerHTML = linhas;
            })
            .catch(function () {
                tbodyUsuarios.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnPesquisar").disabled = "";
            });


    },
    selecionar: function (id,nome) {
        window.parent.index.selecionarPerfil(id, nome);
    }

}