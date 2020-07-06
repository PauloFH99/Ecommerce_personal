var indexListar = {
    excluir: function (id) {

        if (!confirm("Deseja excluír?")) {
            return;
        }

        var config = {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/CadastrarProduto/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tr = document.querySelector("tr[data-id='" + id + "']");
                    tr.parentNode.removeChild(tr);
                    alert("Produto excluído com sucesso");
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbUsuarios").style.display = "table";

        var tbodyUsuarios = document.getElementById("tbodyUsuarios");
        tbodyUsuarios.innerHTML = `<tr><td colspan="3"><img src=\"/imgs/ajax-loader.gif"\ /></td></tr>`
        document.getElementById("btnPesquisar").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var descricao = document.getElementById("descricao").value;

        fetch("/CadastrarProduto/Pesquisar?descricao=" + descricao, config)
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
                              <td> <div class="dropdown">
                              <button class="btn btn-light" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                               <i class="fas fa-ellipsis-v"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item"href="javascript:void" onclick="indexListar.excluir(${dadosObj[i].id})"><i class="far fa-trash-alt"  style="color:red" ></i> Excluir</a>
                                <a class="dropdown-item" href="#">Another action</a>
                                <a class="dropdown-item" href="#">Something else here</a>
                              </div>
                            </div>
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


    }

}