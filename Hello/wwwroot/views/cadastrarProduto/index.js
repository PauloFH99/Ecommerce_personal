var index = {

    btnCadastrar: function () {

        var descricao = document.getElementById("descricao").value;
        var categoria = document.querySelector("#selCategoria").value;
        var valor = document.querySelector("#valor").value;
        var quantidade = document.querySelector("#quantidade").value;
        if (descricao.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Informe a descrição.";
            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").style.marginTop = "15px";
        } else if (quantidade.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Informe a quantidade.";
            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").style.marginTop = "15px";
        }
        else if (categoria.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Informe a categoria.";
            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").style.marginTop = "15px";
        }
        else if (valor.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Informe o valor.";
            document.getElementById("divMsg").className = "alert alert-danger";
            document.getElementById("divMsg").style.marginTop = "15px";
        }
        else {

            var dados = {
                descricao,
                categoria,
                quantidade,
                valor
            }

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(dados)  //serializa
            };

            fetch("/CadastrarProduto/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (dadosObj.operacao) {


                        /*document.getElementById("divMsg").innerHTML = dadosObj.msg;
                        document.getElementById("divMsg").className = "alert alert-success";
                        document.getElementById("divMsg").style.marginTop = "15px";*/

                        //enviando a imagem
                        var id = dadosObj.id;

                        var fd = new FormData();
                        var msg = dadosObj.msg;
                        for (var i = 0; i < document.getElementById("foto").files.length; i++) {
                            fd.append("foto" + "i", document.getElementById("foto").files[i]);
                        }


                        fd.append("id", id);

                        var configFD = {
                            method: "POST",
                            headers: {
                                "Accept": "application/json"
                            },
                            body: fd
                        }

                        fetch("/CadastrarProduto/Foto?msg=" + msg, configFD)
                            .then(function (dadosJson) {
                                var obj = dadosJson.json(); //deserializando
                                return obj;
                            })
                            .then(function (dadosObj) {
                                alert(dadosObj.msg);
                                window.location.href = "Index";
                            })
                            .catch(function () {

                                document.getElementById("divMsg").innerHTML = "deu erro na foto";
                                document.getElementById("divMsg").className = "alert alert-danger";
                                document.getElementById("divMsg").style.marginTop = "15px";
                            })

                    }
                    else {
                        document.getElementById("divMsg").innerHTML = dadosObj.msg;
                        document.getElementById("divMsg").className = "alert alert-danger";
                        document.getElementById("divMsg").style.marginTop = "15px";

                    }

                })
                .catch(function () {
                    document.getElementById("divMsg").innerHTML = dadosObj.msg;
                    document.getElementById("divMsg").className = "alert alert-danger";
                    document.getElementById("divMsg").style.marginTop = "15px";
                })

        }

    },

    buscarCategorias: function (codigo) {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/CategoriaProduto/ObterCategorias?codigo=" + codigo, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCategoria = document.getElementById("selCategoria");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                    //opts += "<option value='" + dadosObj[i] + "'>" + dadosObj[i] + "</option>"
                }

                selCategoria.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },
    buscarProduto: function (pd) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/CategoriaProduto/ObterProdutos?pd=" + pd, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                var display = document.getElementById("minhaDiv").style.display;
                var selProduto = document.getElementById("selProduto");

                if (display == "none" && selProduto.innerHTML != "")
                    document.getElementById("minhaDiv").style.display = 'block';
                else
                    document.getElementById("minhaDiv").style.display = 'none';

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descricao}</option>`;
                }

                selProduto.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },
    buscarProdutoporCategoria: function (pd) {
        var minhaDivL = document.getElementById("minhaDiv").style.display = 'block';
        minhaDivL = document.getElementById("minhaDiv");
        minhaDivL.innerHTML = `<img src=\"/imgs/ajax-loader.gif"\ />`;
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/CategoriaProduto/ObterProdutosPorCategoria?pd=" + pd, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                document.getElementById("minhaDiv").style.display = 'block';
                var display = document.getElementById("minhaDiv");
                var selProduto = document.getElementById("selProduto");


                var opts = "<option value=''></option>";

                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descricao}</option>`;
                }
                var optsDiv = "<option value=''></option>";

                for (var i = 0; i < dadosObj.length; i++) {

                    optsDiv += ` 
                    <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                    <div class="card mb-4">
                           <div id="carouselExampleControlsid${dadosObj[i].id}" class="carousel slide" data-ride="carousel">
                             <ol class="carousel-indicators">`;

                    for (var j = 0; j < dadosObj[i].qtdeFoto; j++) {
                        if (j == 0)
                            optsDiv += `<li data-target="#carouselExampleControlsid${dadosObj[i].id}" data-slide-to="0" class="active"></li>`;
                        else {
                            optsDiv += `<li data-target="#carouselExampleControlsid${dadosObj[i].id}" data-slide-to=`;
                            optsDiv += `"${j}"></li>`;
                        }
                    }
                    optsDiv += ` 
                              </ol>
                              <div class="carousel-inner">
                                    `;
                    for (var j = 0; j < dadosObj[i].qtdeFoto; j++) {
                        if (j == 0) {
                            optsDiv += `<div class="carousel-item active">
                                               <img class="d-block w-100" src="/CadastrarProduto/ObterFotoS?id=${dadosObj[i].id}&pos=${j}" alt="`;
                            optsDiv += `${j}" slide">
                                                </div>`;
                        }
                        else {
                            optsDiv += `<div class="carousel-item">
                                               <img class="d-block w-100" src="/CadastrarProduto/ObterFotoS?id=${dadosObj[i].id}&pos=${j}" alt="`;
                            optsDiv += `${j}" slide">
                                                </div>`;
                        }
                    }

                    optsDiv += `
                              </div>
                              <a class="carousel-control-prev" href="#carouselExampleControlsid${dadosObj[i].id}" role="button" data-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="sr-only">Previous</span>
                              </a>
                              <a class="carousel-control-next" href="#carouselExampleControlsid${dadosObj[i].id}" role="button" data-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="sr-only">Next</span>
                              </a>
                            </div>
                        
                            <div class="card-body">
                                <h4 class="card-title">
                                    <a href="#">${dadosObj[i].descricao}</a>
                                </h4>
                                <h5>R$: ${dadosObj[i].valor}</h5>
                                <p class="card-text"> </p>
                            </div>
                    </div>
                </div>`;
                }

                display.innerHTML = optsDiv;
                selProduto.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },
    obterProduto: function (codigo) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/CadastrarProduto/ObterProduto?codigo=" + codigo, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                var display = document.getElementById("minhaDiv").style.display = 'block';
                var selProduto = document.getElementById("selProduto");



                var opts = "<option value=''></option>";
                for (var i = 0; i <= 1; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descricao}</option>`;
                }
                var optsDiv = "<option value=''></option>";

                for (var i = 0; i <= 1; i++) {

                    optsDiv += ` <div class="col-lg-4 col-md-6 mb-4">
                    <div class="card h-100">
                        <a href="#"><img class="card-img-top" src="/imgs/pc.png" alt=""></a>
                            <div class="card-body">
                                <h4 class="card-title">
                                    <a href="#">item 1</a>
                                </h4>
                                <h5>R$: ${dadosObj[i].valor}</h5>
                                <p class="card-text"> ${dadosObj[i].descricao}</p>
                            </div>
                    </div>
                </div>`;
                }

                minhaDiv.innerHTML = optsDiv;
                selProduto.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}
index.buscarCategorias();