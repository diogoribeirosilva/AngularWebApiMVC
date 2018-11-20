app.controller("crudCtrl", function($scope, crudService) {

    $scope.divCelular = false;

    obterCelulares();

    function obterCelulares() {

        // debugger;

        var celularesData = crudService.ObterCelulares();

        celularesData.then(function (celular) {

            $scope.celurares = celular.data;
        }, function () {

            toastr["error"]("Erro ao obter os celulares", "Diogo Ribeiro - Testando toastr");
        });

    }

    //obtem por id
    $scope.obterPorId = function (celular) {

        var celularData = crudService.ObterCelularPorId(celular.Id);

        celularData.then(function (_celular) {
            $scope.celular = _celular.data;
            $scope.celularId = celular.Id;
            $scope.Marca = celular.Marca;
            $scope.Modelo = celular.Modelo;
            $scope.Cor = celular.Cor;
            $scope.TipoChip = celular.TipoChip;
            $scope.MemoriaInterna = celular.MemoriaInterna;
            $scope.Action = "Atualizar";
            $scope.divCelular = true;
        }, function () {
            toastr["error"]("Erro ao obter celular por id!", "Diogo Ribeiro - Testando toastr");
        });
    }

    $scope.excluirCelular = function(celular) {

        var celularData = crudService.ExcluirCelular(celular.Id);

        celularData.then(function(data) {

            if (data.status == 200) {

                toastr["success"]("Celular excluído com sucesso!", "Diogo Ribeiro - Testando toastr");
            }
            obterCelulares();
        },function() {
            toastr["error"]("Erro ao excluir", "Diogo Ribeiro - Testando toastr");

            });
        limparCampos();

    }

    $scope.AdicionarAtualizarCelular = function () {

        var celular = {
            Marca: $scope.Marca,
            Modelo: $scope.Modelo,
            Cor: $scope.Cor,
            TipoChip: $scope.TipoChip,
            MemoriaInterna: $scope.MemoriaInterna
        };
        var valorAcao = $scope.Action;

        if (valorAcao == "Atualizar") {

            celular.Id = $scope.celularId;
            var celularData = crudService.AtualizarCelular(celular);
            celularData.then(function (data) {
                obterCelulares();
                $scope.divCelular = false;
                if (data.status == 200) {
                    toastr["success"]("Celular alterado com sucesso!", "Diogo Ribeiro - Testando toastr");
                }
            }, function () {
                toastr["error"]("Erro ao atualizar!", "Diogo Ribeiro - Testando toastr");
            });
        } else {

            var celularData = crudService.AdicionarCelular(celular);
            celularData.then(function (data) {
                obterCelulares();

                if (data.status == 200) {
                    toastr["success"]("Celular cadastrado com sucesso!", "Diogo Ribeiro - Testando toastr");
                }
                $scope.divCelular = false;
            }, function () {
                toastr["error"]("Erro ao incluir!", "Diogo Ribeiro - Testando toastr");
            });
        }
        limparCampos();
    }

    $scope.incluirCelularDiv = function () {

        limparCampos();
        $scope.Action = "Adicionar";
        $scope.divCelular = true;
    }

    $scope.Cancelar = function () {
        $scope.divCelular = false;
    };

    

    function limparCampos() {
        $scope.Cor = "";
        $scope.Id = "";
        $scope.Modelo = "";
        $scope.Marca = "";
        $scope.TipoChip = "";
        $scope.MemoriaInterna = "";
    }


  

    

});
