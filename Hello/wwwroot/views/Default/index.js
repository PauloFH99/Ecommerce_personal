let index = {

    usuario: "",
    btnEntrarOnClick: function () {

        let nomeUsuario = document.getElementById("name").value;
        let senhaUsuario = document.getElementById("senha").value;

        let dados = {

            nome: nomeUsuario,
            senha: senhaUsuario
        }



        var config = {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include',
            body: JSON.stringify(dados)  //serializa
        };

        fetch("Default/Logar", config)
            .then(function (dadosJson) {
                //console.log(dadosJson);
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                console.log(dadosObj);
                document.getElementById("divMsg").innerHTML = dadosObj.msg;
            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })



        // alert("oi 333..." + v);
    }
}



